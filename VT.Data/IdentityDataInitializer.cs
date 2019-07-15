using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;
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
                if (!isExisted)
                {
                    await roleManager.CreateAsync(new IdentityRole<Guid>(role));
                }
            }

            //Add User
            var user = await userManager.FindByEmailAsync("vehicle1@test.com");
            if (user == null)
            {
                user = new User()
                {
                    UserName = "Vehicle1",
                    Email = "vehicle1@test.com"
                };
                await userManager.CreateAsync(user, "12345");
            }

            await userManager.AddToRoleAsync(user, "User");

            //Add Admin
            var admin = await userManager.FindByEmailAsync("admin@test.com");
            if (admin == null)
            {
                admin = new User()
                {
                    UserName = "Admin1",
                    Email = "admin@test.com"
                };
                await userManager.CreateAsync(admin, "12345abc");
            }

            await userManager.AddToRoleAsync(admin, "Admin");
        }
    }
}
