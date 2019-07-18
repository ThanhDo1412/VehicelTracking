using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleTracking.Data.CommonData;
using VehicleTracking.Data.Exceptions;
using VehicleTracking.Data.Model;
using VehicleTracking.Data.Repository;
using VehicleTracking.Data.TrackingHistory;
using VehicleTracking.Data.Vehicle;
using VehicleTracking.GateWay.GoogleGateway;
using VehicleTracking.Service.Interface;

namespace VehicleTracking.Service
{
    public class TrackingService : ITrackingService
    {
        private readonly IReponsitory<TrackingHistory> _trackingHistoryRepository;
        private readonly IReponsitory<TrackingSession> _trackingSessionRepository;
        private readonly IReponsitory<Vehicle> _vehicleRepository;
        private readonly IGeoCodingService _geoCodingService;

        public TrackingService(IReponsitory<TrackingHistory> trackingHistoryRepository, IReponsitory<TrackingSession> trackingSessionRepository, IReponsitory<Vehicle> vehicleRepository, IGeoCodingService geoCodingService)
        {
            _trackingHistoryRepository = trackingHistoryRepository;
            _trackingSessionRepository = trackingSessionRepository;
            _vehicleRepository = vehicleRepository;
            _geoCodingService = geoCodingService;
        }

        public async Task UpdateLocation(VehicleLocationRequest model)
        {
            var vehicle = _vehicleRepository.FindOneByCondition(x => x.VehicleNumber == model.VehicleNumber && x.IsActive);
            if (vehicle == null) throw new VehicleNotFoundException(ErrorCode.E102, model.VehicleNumber);

            //Check session was created or not
            var session = _trackingSessionRepository.FindOneByCondition(x =>
                x.CreatedDate >= DateTime.UtcNow.Date
                && x.VehicleId == vehicle.Id);
            if (session == null)
            {
                session = new TrackingSession()
                {
                    VehicleId = vehicle.Id,
                    CreatedDate = DateTime.UtcNow,
                    TrackingRemark = DateTime.UtcNow.ToString("d")
                };
                _trackingSessionRepository.Create(session);
                await _trackingSessionRepository.SaveAsync();
            }

            //Update Location with this session
            var location = new TrackingHistory()
            {
                TrackingSessionId = session.Id,
                CreatedDate = DateTime.UtcNow,
                Lat = model.Latitude,
                Lon = model.Longitude
            };
            _trackingHistoryRepository.Create(location);
            await _trackingHistoryRepository.SaveAsync();
        }

        public async Task<VehicleLocationResponse> GetCurrentLocation(string vehicleNumber, bool isGetAddress)
        {
            var vehicle = _vehicleRepository.FindOneByCondition(x => x.VehicleNumber == vehicleNumber && x.IsActive);
            if (vehicle == null) throw new VehicleNotFoundException(ErrorCode.E102, vehicleNumber);

            //Get latest position depend on session
            var location = _trackingSessionRepository.FindByCondition(x => x.VehicleId == vehicle.Id)
                .OrderByDescending(x => x.CreatedDate).SelectMany(x => x.TrackingHistories)
                .OrderByDescending(x => x.CreatedDate).FirstOrDefault();
          
            if (location == null) throw new LocationNotFoundException(ErrorCode.E103, vehicleNumber);

            var address = isGetAddress ? await _geoCodingService.ReverseGeoCoding(location.Lon, location.Lat) : null;

            return new VehicleLocationResponse()
            {
                VehicleNumber = vehicleNumber,
                Latitude = location.Lat,
                Longitude = location.Lon,
                LatestUpdate = location.CreatedDate,
                Address = address
            };
        }

        public VehicleJourneyResponse GetVehicleJourney(VehicleJourneyRequest model)
        {
            var vehicle = _vehicleRepository.FindOneByCondition(x => x.VehicleNumber == model.VehicleNumber && x.IsActive);
            if (vehicle == null) throw new VehicleNotFoundException(ErrorCode.E102, model.VehicleNumber);

            //Get latest position depend on session
            var locations = _trackingSessionRepository.FindByCondition(x =>
                    x.VehicleId == vehicle.Id
                    && x.CreatedDate >= model.From
                    && x.CreatedDate <= model.To)
                .SelectMany(x => x.TrackingHistories)
                .Where(x => x.CreatedDate >= model.From && x.CreatedDate <= model.To)
                .Select(x => new LocationBase
                {
                    Latitude = x.Lat,
                    Longitude = x.Lon,
                    LatestUpdate = x.CreatedDate
                });

            if (!locations.Any()) throw new JourneyNotFoundException(ErrorCode.E104, model.VehicleNumber, model.From.ToString("G"), model.To.ToString("G"));

            return new VehicleJourneyResponse()
            {
                VehicleNumber = model.VehicleNumber,
                Locations = locations.OrderBy(x => x.LatestUpdate).ToList()
            };
        }
    }
}
