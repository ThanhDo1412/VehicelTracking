using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using VT.Data.CommonData;
using VT.Data.Helper;
using VT.Data.Vehicle;
using VT.Data.ViewModel;

namespace VT.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly SignInManager<User> _signinManager;
        private readonly UserManager<User> _userManager;

        public AccountController(IConfiguration configuration,
            UserManager<User> userManager,
            SignInManager<User> signinManager)
        {
            _configuration = configuration;
            _userManager = userManager;
            _signinManager = signinManager;
        }

        [AllowAnonymous]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel login)
        {
            var user = await _userManager.FindByNameAsync(login.Email);

            if (user == null) throw new CustomException(ErrorCode.E001);

            var result = await _signinManager.CheckPasswordSignInAsync(user, login.Password, false);

            if (!result.Succeeded) throw new CustomException(ErrorCode.E002);

            var token = await GenerateJWT(user);

            var response = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(response);
        }

        [AllowAnonymous]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel resigerInfo)
        {
            var user = new User()
            {
                UserName = resigerInfo.UserName,
                Email = resigerInfo.Email
            };

            var createResult = await _userManager.CreateAsync(user, resigerInfo.Password);

            if (!createResult.Succeeded) throw new CustomException(ErrorCode.E003);

            //Set default Role is user
            await _userManager.AddToRoleAsync(user, "User");

            var token = await GenerateJWT(user);

            var response = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(response);
        }

        private async Task<JwtSecurityToken> GenerateJWT(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddMinutes(double.Parse(_configuration["Tokens:ExpiryMinutes"])).ToUniversalTime();

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            //Assign Roles
            var roles = await _userManager.GetRolesAsync(user);
            foreach (var r in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, r));
            }

            return new JwtSecurityToken(
                _configuration["Tokens:Issuer"],
                _configuration["Tokens:Audience"],
                claims,
                expires: expires,
                signingCredentials: creds);
        }
    }
}