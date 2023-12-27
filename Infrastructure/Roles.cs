using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public enum Roles
    {
        Admin,
        Manager,
        User
    }
    public class SeedRoles
    {
        public async static Task seed(RoleManager<IdentityRole> roleManager)
        {
            foreach(Roles role  in Enum.GetValues(typeof(Roles)))
            {
                var identityRole = new IdentityRole { Name = role.ToString() };
                await roleManager.CreateAsync(identityRole);
            }
        }
    }
}
