using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VT.Data.Vehicle;
using VT.Data.Model;
using VT.Service.Interface;

namespace VT.WebApi.Controllers
{
    [Authorize(Roles = "User")]
    public class VehicelController : Controller
    {
        private readonly IVehicleService _vehicleService;

        public VehicelController(IVehicleService vehicleService)
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
            await _vehicleService.RemoveVehicel(vehicleNumber);
            return Ok();
        }
    }
}