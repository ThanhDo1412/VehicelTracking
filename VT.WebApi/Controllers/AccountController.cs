using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VT.Data.Model;
using VT.Data.Vehicle;
using VT.Service.Interface;

namespace VT.WebApi.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService,
            UserManager<User> userManager,
            SignInManager<User> signinManager)
        {
            _accountService = accountService;
        }

        [AllowAnonymous]
        [Route("api/Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest login)
        {
            var response = await _accountService.LogIn(login);
            return Ok(response);
        }

        [AllowAnonymous]
        [Route("api/Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest resigerInfo)
        {
            var response = await _accountService.Register(resigerInfo);
            return Ok(response);
        }
    }
}