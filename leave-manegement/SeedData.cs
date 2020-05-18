using System;
using leave_manegement.Data;
using Microsoft.AspNetCore.Identity;

namespace leave_manegement
{
    public static class SeedData
    {
        public static void Seed(
            UserManager<Employee> userManager,
            RoleManager<IdentityRole> roleManager
        )
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }

        public static void SeedUsers(UserManager<Employee> userManager)
        {
            if (userManager.FindByNameAsync("admin").Result == null)
            {
                var user = new Employee {
                    UserName = "Administrator",
                    Email = "admin@email.com"
                };

                var result = userManager.CreateAsync(user, "Qwerty!23456").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Administrator").Wait();
                }

            }
        }

        public static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if(!roleManager.RoleExistsAsync("Administrator").Result)
            {
                var role = new IdentityRole{ Name = "Administrator" };
                roleManager.CreateAsync(role).Wait();
            }

            if (!roleManager.RoleExistsAsync("Employee").Result)
            {
                var role = new IdentityRole { Name = "Employee" };
                roleManager.CreateAsync(role).Wait();
            }
        }
    }
}
