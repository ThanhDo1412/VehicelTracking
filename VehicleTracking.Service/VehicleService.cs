using System;
using System.Threading.Tasks;
using VehicleTracking.Data.CommonData;
using VehicleTracking.Data.Exceptions;
using VehicleTracking.Data.Model;
using VehicleTracking.Data.Repository;
using VehicleTracking.Data.Vehicle;
using VehicleTracking.GateWay.GoogleGateway;
using VehicleTracking.Service.Interface;

namespace VehicleTracking.Service
{
    public class VehicleService : IVehicleService
    {
        private readonly IReponsitory<Vehicle> _vehicleRepository;

        public VehicleService(IReponsitory<Vehicle> vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public async Task AddVehicle(VehicleRequest model)
        {
            var vehicle = _vehicleRepository.FindOneByCondition(x => x.VehicleNumber == model.VehicleNumber);

            //Vehicle already existed
            if (vehicle != null)
            {
                if (vehicle.IsActive) throw new DuplicateVehicleException(ErrorCode.E101, model.VehicleNumber);

                vehicle.IsActive = true;
                vehicle.UpdatedDate = DateTime.UtcNow;

                _vehicleRepository.Update(vehicle);
                await _vehicleRepository.SaveAsync();
            }
            else
            {
                vehicle = new Vehicle()
                {
                    VehicleNumber = model.VehicleNumber,
                    IsActive = true,
                    CreatedDate = DateTime.UtcNow
                };

                _vehicleRepository.Create(vehicle);
                await _vehicleRepository.SaveAsync();
            }
        }

        public async Task RemoveVehicle(string vehicleNumber)
        {
            var vehicle = _vehicleRepository.FindOneByCondition(x => x.VehicleNumber == vehicleNumber && x.IsActive);
            if (vehicle == null) throw new VehicleNotFoundException(ErrorCode.E102, vehicleNumber);

            vehicle.IsActive = false;
            vehicle.UpdatedDate = DateTime.UtcNow;

            _vehicleRepository.Update(vehicle);
            await _vehicleRepository.SaveAsync();
        }
    }
}
