using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VT.Data.CommonData;
using VT.Data.Helper;
using VT.Data.Model;
using VT.Data.Repository;
using VT.Data.TrackingHistory;
using VT.Service.Interface;

namespace VT.Service
{
    public class TrackingService : ITrackingService
    {
        private readonly TrackingHistoryRepository<TrackingHistory> _trackingHistoryRepository;
        private readonly TrackingHistoryRepository<TrackingSession> _trackingSessionRepository;
        private readonly VehicleService _vehicleService;

        public TrackingService(TrackingHistoryRepository<TrackingHistory> trackingHistoryRepository, TrackingHistoryRepository<TrackingSession> trackingSessionRepository, VehicleService vehicleService)
        {
            _trackingHistoryRepository = trackingHistoryRepository;
            _trackingSessionRepository = trackingSessionRepository;
            _vehicleService = vehicleService;
        }

        public async Task UpdateLocation(VehicleLocationRequest model)
        {
            var vehicle = await _vehicleService.GetVehicleByNumber(model.VehicleNumber);
            if (vehicle == null) throw new CustomException(ErrorCode.E005, model.VehicleNumber);

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

        public async Task<VehicleLocationResponse> GetCurrentLocation(string vehicleNumber)
        {
            var vehicle = await _vehicleService.GetVehicleByNumber(vehicleNumber);
            if (vehicle == null) throw new CustomException(ErrorCode.E005, vehicleNumber);

            //Get latest session
            var session = (await _trackingSessionRepository.FindByConditionAsync(x => x.VehicleId == vehicle.Id))
                .OrderByDescending(x => x.CreatedDate).FirstOrDefault();
            if (session == null) throw new CustomException(ErrorCode.E006, vehicleNumber);

            //Get latest position depend on this session
            var location = session.TrackingHistories.OrderByDescending(x => x.CreatedDate).FirstOrDefault();
            if (location == null) throw new CustomException(ErrorCode.E006, vehicleNumber);

            return new VehicleLocationResponse()
            {
                VehicleNumber = vehicleNumber,
                Latitude = location.Lat,
                Longitude = location.Lon,
                LatestUpdate = location.CreatedDate
            };
        }

        public async Task<VehicleJourneyResponse> GetVehicelJourney(VehicleJourneyRequest model)
        {
            var vehicle = await _vehicleService.GetVehicleByNumber(model.VehicleNumber);
            if (vehicle == null) throw new CustomException(ErrorCode.E005, model.VehicleNumber);

            //Get sessions
            var sessions = await _trackingSessionRepository.FindByConditionAsync(x =>
                x.VehicleId == vehicle.Id
                && x.CreatedDate >= model.From
                && x.CreatedDate <= model.To);
            if (!sessions.Any()) throw new CustomException(ErrorCode.E007, model.VehicleNumber, model.From.ToString("G"), model.To.ToString("G"));

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
            if (!locations.Any()) throw new CustomException(ErrorCode.E007, model.VehicleNumber, model.From.ToString("G"), model.To.ToString("G"));

            return new VehicleJourneyResponse()
            {
                VehicleNumber = model.VehicleNumber,
                Locations = locations.OrderBy(x => x.LatestUpdate).ToList()
            };
        }
    }
}
