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
        private readonly TrackingHistoryRepository<TrackingHistory> _trackingHistoryRepository;
        private readonly TrackingHistoryRepository<TrackingSession> _trackingSessionRepository;
        private readonly VehicleRepository<Vehicle> _vehicleRepository;
        private readonly IGeoCodingService _geoCodingService;

        public TrackingService(TrackingHistoryRepository<TrackingHistory> trackingHistoryRepository, TrackingHistoryRepository<TrackingSession> trackingSessionRepository, VehicleRepository<Vehicle> vehicleRepository, IGeoCodingService geoCodingService)
        {
            _trackingHistoryRepository = trackingHistoryRepository;
            _trackingSessionRepository = trackingSessionRepository;
            _vehicleRepository = vehicleRepository;
            _geoCodingService = geoCodingService;
        }

        public async Task UpdateLocation(VehicleLocationRequest model)
        {
            var vehicle = await _vehicleRepository.FindOneByConditionAsync(x => x.VehicleNumber == model.VehicleNumber && x.IsActive);
            if (vehicle == null) throw new VehicleNotFoundException(ErrorCode.E102, model.VehicleNumber);

            //Check session was created or not
            var session = await _trackingSessionRepository.FindOneByConditionAsync(x =>
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
            var vehicle = await _vehicleRepository.FindOneByConditionAsync(x => x.VehicleNumber == vehicleNumber && x.IsActive);
            if (vehicle == null) throw new VehicleNotFoundException(ErrorCode.E102, vehicleNumber);

            //Get latest session
            var session = (await _trackingSessionRepository.FindByConditionAsync(x => x.VehicleId == vehicle.Id))
                .OrderByDescending(x => x.CreatedDate).FirstOrDefault();
            if (session == null) throw new LocationNotFoundException(ErrorCode.E103, vehicleNumber);

            //Get latest position depend on this session
            var location = session.TrackingHistories.OrderByDescending(x => x.CreatedDate).FirstOrDefault();
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

        public async Task<VehicleJourneyResponse> GetVehicleJourney(VehicleJourneyRequest model)
        {
            var vehicle = await _vehicleRepository.FindOneByConditionAsync(x => x.VehicleNumber == model.VehicleNumber && x.IsActive);
            if (vehicle == null) throw new VehicleNotFoundException(ErrorCode.E102, model.VehicleNumber);

            //Get sessions
            var sessions = await _trackingSessionRepository.FindByConditionAsync(x =>
                x.VehicleId == vehicle.Id
                && x.CreatedDate >= model.From
                && x.CreatedDate <= model.To);
            if (!sessions.Any()) throw new JourneyNotFoundException(ErrorCode.E104, model.VehicleNumber, model.From.ToString("G"), model.To.ToString("G"));

            //Get latest position depend on this session
            var locations = new List<LocationBase>();
            foreach (var session in sessions)
            {
                var listLocation = session.TrackingHistories
                    .Where(x => x.CreatedDate >= model.From && x.CreatedDate <= model.To)
                    .Select(x => new LocationBase
                    {
                        Latitude = x.Lat,
                        Longitude = x.Lon,
                        LatestUpdate = x.CreatedDate
                    });
                locations.AddRange(listLocation);
            }
            if (!locations.Any()) throw new JourneyNotFoundException(ErrorCode.E104, model.VehicleNumber, model.From.ToString("G"), model.To.ToString("G"));

            return new VehicleJourneyResponse()
            {
                VehicleNumber = model.VehicleNumber,
                Locations = locations.OrderBy(x => x.LatestUpdate).ToList()
            };
        }
    }
}
