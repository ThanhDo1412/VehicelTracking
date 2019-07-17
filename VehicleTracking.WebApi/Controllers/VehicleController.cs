using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VehicleTracking.Data.CommonData;
using VehicleTracking.Data.Exceptions;
using VehicleTracking.Data.Model;
using VehicleTracking.Service.Interface;

namespace VehicleTracking.WebApi.Controllers
{
    [Authorize(Roles = "User")]
    public class VehicleController : Controller
    {
        private readonly IVehicleService _vehicleService;

        public VehicleController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }


        [HttpPost]
        [Route("api/vehicle")]
        public async Task<IActionResult> AddVehicle([FromBody] VehicleRequest model)
        {
            model.Validate();
            await _vehicleService.AddVehicle(model);
            return Ok();
        }

        [HttpDelete]
        [Route("api/vehicle")]
        public async Task<IActionResult> RemoveVehicle([FromBody] string vehicleNumber)
        {
            if (string.IsNullOrWhiteSpace(vehicleNumber))
            {
                throw new VehicleInvalidException(ErrorCode.E105);
            }
            await _vehicleService.RemoveVehicle(vehicleNumber);
            return Ok();
        }
    }
}