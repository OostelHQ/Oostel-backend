using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Oostel.Domain.UserAuthentication.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Infrastructure.Data
{
    public static class Seed
    {

        public static async Task SeedIdentityRoles(ApplicationDbContext context, RoleManager<ApplicationRole> roleManager)
        {
            var roles = new string[] { "LandLord", "Student", "Agent"};
            foreach (var role in roles)
            {
                var roleStore = new RoleStore<ApplicationRole>(context);

                var roleExist = await roleManager.RoleExistsAsync(role);
                if (!roleExist)
                {
                    var roleToAdd = new ApplicationRole(role)
                    {
                        NormalizedName = role.ToUpper()
                    };
                    await roleManager.CreateAsync(roleToAdd);
                }
            }
        }
    }
}
