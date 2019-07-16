using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using VehicleTracking.Data.CommonData;
using VehicleTracking.Data.Exceptions;
using VehicleTracking.Data.Helper;
using VehicleTracking.Data.Model;
using VehicleTracking.Data.Vehicle;
using VehicleTracking.Service.Interface;

namespace VehicleTracking.Service
{
    public class AccountService : IAccountService
    {
        private readonly IConfiguration _configuration;
        private readonly SignInManager<VehicleTrackingUser> _signinManager;
        private readonly UserManager<VehicleTrackingUser> _userManager;

        public AccountService(IConfiguration configuration,
            UserManager<VehicleTrackingUser> userManager,
            SignInManager<VehicleTrackingUser> signinManager)
        {
            _configuration = configuration;
            _userManager = userManager;
            _signinManager = signinManager;
        }

        public async Task<string> LogIn(LoginRequest model)
        {
            var user = await _userManager.FindByNameAsync(model.Email);

            if (user == null) throw new UserNotFoundException(ErrorCode.E001);

            var result = await _signinManager.CheckPasswordSignInAsync(user, model.Password, false);

            if (!result.Succeeded) throw new WrongPasswordException(ErrorCode.E002);

            var token = await GenerateJWT(user);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<string> Register(RegisterRequest resigerInfo)
        {
            var user = new VehicleTrackingUser()
            {
                UserName = resigerInfo.UserName,
                Email = resigerInfo.Email
            };

            var createResult = await _userManager.CreateAsync(user, resigerInfo.Password);

            if (!createResult.Succeeded) throw new CreateUserFailException(ErrorCode.E003);

            //Set default Role is vehicleTrackingUser
            await _userManager.AddToRoleAsync(user, "VehicleTrackingUser");

            var token = await GenerateJWT(user);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task<JwtSecurityToken> GenerateJWT(VehicleTrackingUser vehicleTrackingUser)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddMinutes(double.Parse(_configuration["Tokens:ExpiryMinutes"])).ToUniversalTime();

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, vehicleTrackingUser.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, vehicleTrackingUser.Id.ToString())
            };

            //Assign Roles
            var roles = await _userManager.GetRolesAsync(vehicleTrackingUser);
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
