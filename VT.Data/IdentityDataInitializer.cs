using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using VT.Data.Vehicle;

namespace VT.Data
{
    public static class IdentityDataInitializer
    {
        public static async Task SeedData(UserManager<User> userManager, RoleManager<IdentityRole<Guid>> roleManager)
        {
            //Add role
            var initRole = new string [] {"User", "Admin"};
            foreach (var role in initRole)
            {
                var isExisted = await roleManager.RoleExistsAsync(role);
                if (isExisted)
                {
                    await roleManager.CreateAsync(new IdentityRole<Guid>(role));
                }
            }

            //Add User
            var user = await userManager.FindByEmailAsync("vehicle1@test.com");
            if (user == null)
            {
                await userManager.CreateAsync(new User()
                {
                    UserName = "Vehicle1",
                    Email = "vehicle1@test.com"
                }, "123");
            }

            await userManager.AddToRoleAsync(user, "User");

            //Add Admin
            var adim = await userManager.FindByEmailAsync("admin@test.com");
            if (adim == null)
            {
                await userManager.CreateAsync(new User()
                {
                    UserName = "Vehicle Admin",
                    Email = "admin@test.com"
                }, "12345abc");
            }

            await userManager.AddToRoleAsync(adim, "Admin");
        }
    }
}
