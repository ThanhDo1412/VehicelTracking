using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VehicleTracking.Data.Vehicle;
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
            await _vehicleService.AddVehicle(model);
            return Ok();
        }

        [HttpDelete]
        [Route("api/vehicle")]
        public async Task<IActionResult> RemoveVehicle([FromBody] string vehicleNumber)
        {
            await _vehicleService.RemoveVehicle(vehicleNumber);
            return Ok();
        }
    }
}