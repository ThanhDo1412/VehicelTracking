using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VehicleTracking.Data.Model;
using VehicleTracking.Service.Interface;

namespace VehicleTracking.WebApi.Controllers
{
    public class TrackingController : Controller
    {
        private readonly ITrackingService _trackingService;

        public TrackingController(ITrackingService trackingService)
        {
            _trackingService = trackingService;
        }


        [HttpPost]
        [Authorize(Roles = "User")]
        [Route("api/tracking/current")]
        public async Task<IActionResult> UpdateLocation([FromBody] VehicleLocationRequest model)
        {
            await _trackingService.UpdateLocation(model);
            return Ok();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("api/tracking/current/{vehicleNumber}")]
        public async Task<IActionResult> GetLocation(string vehicleNumber)
        {
            var result = await _trackingService.GetCurrentLocation(vehicleNumber);
            return Ok(result);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("api/tracking/journey/{vehicleNumber}")]
        public async Task<IActionResult> GetJourney([FromBody] VehicleJourneyRequest model)
        {
            var result = await _trackingService.GetVehicleJourney(model);
            return Ok(result);
        }
    }
}