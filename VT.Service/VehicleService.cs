using System;
using System.Linq;
using System.Threading.Tasks;
using VT.Data.CommonData;
using VT.Data.Helper;
using VT.Data.Model;
using VT.Data.Repository;
using VT.Data.Vehicle;
using VT.Service.Interface;

namespace VT.Service
{
    public class VehicleService : IVehicleService
    {
        private readonly VehicleRepository<Vehicle> _vehicleRepository;

        public VehicleService(VehicleRepository<Vehicle> vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public async Task AddVehicle(VehicleRequest model)
        {
            var vehicle = await _vehicleRepository.FindOneByConditionAsync(x => x.VehicleNumber == model.VehicleNumber);

            //Vehicle already existed
            if (vehicle != null)
            {
                if (vehicle.IsActive) throw new CustomException(ErrorCode.E004, model.VehicleNumber);

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

        public async Task RemoveVehicel(string vehicleNumber)
        {
            var vehicle = await GetVehicleByNumber(vehicleNumber);
            if (vehicle == null) throw new CustomException(ErrorCode.E005, vehicleNumber);

            vehicle.IsActive = false;
            vehicle.UpdatedDate = DateTime.UtcNow;

            _vehicleRepository.Update(vehicle);
            await _vehicleRepository.SaveAsync();
        }

        public async Task<Vehicle> GetVehicleByNumber(string vehicleNumber)
        {
            return await _vehicleRepository.FindOneByConditionAsync(x => x.VehicleNumber == vehicleNumber && x.IsActive);
        }
    }
}
