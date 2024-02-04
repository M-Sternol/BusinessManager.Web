using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManager.Application.Interfaces.Settings
{
    public interface IRoleInitializer
    {
        Task InitializeAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager);
        Task CreateEmployeeRoleIfNotExistAsync(RoleManager<IdentityRole> roleManager);
    }
}
