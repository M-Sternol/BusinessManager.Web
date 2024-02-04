using BusinessManager.Application.Interfaces.Settings;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManager.Application.Services.Settings
{
    public class RoleInitializer : IRoleInitializer
    {
        public async Task InitializeAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            string[] roleNames = { "Admin", "Employee", "Manager" };
            foreach (var roleName in roleNames)
            {
                var roleExists = await roleManager.RoleExistsAsync(roleName);
                if (!roleExists)
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // Utwórz rolę "EMPLOYEE" jeśli nie istnieje
            var employeeRoleExists = await roleManager.RoleExistsAsync("EMPLOYEE");
            if (!employeeRoleExists)
            {
                await roleManager.CreateAsync(new IdentityRole("EMPLOYEE"));
            }
        }
        public async Task CreateEmployeeRoleIfNotExistAsync(RoleManager<IdentityRole> roleManager)
        {
            var roleExists = await roleManager.RoleExistsAsync("Employee");
            if (!roleExists)
            {
                await roleManager.CreateAsync(new IdentityRole("Employee"));
            }
        }
    }

}
