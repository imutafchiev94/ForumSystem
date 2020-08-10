using ForumSystem.App.Common;
using ForumSystem.Data;
using ForumSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumSystem.App.Seeder
{
    public class RoleSeeder : ISeeder
    {


        public async Task SeedAsync(ForumSystemDbContext dbContext, IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

            await SeedRolesAsync(roleManager, GlobalConstants.AdminstrationRoleName);
            await SeedRolesAsync(roleManager, GlobalConstants.UserRoleName);

            await SeedUserWithRoleAsync(userManager);
        }

        private static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager, string roleName)
        {
            var role = await roleManager.FindByNameAsync(roleName);
            if(role == null)
            {
                var result = await roleManager.CreateAsync(new IdentityRole(roleName));
                if(!result.Succeeded)
                {
                    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                }
            }
        }

        private static async Task SeedUserWithRoleAsync(UserManager<User> userManager)
        {
            var user = await userManager.FindByNameAsync("Administrator");
            if(user == null)
            {
                var result = await userManager.CreateAsync(new User {
                    UserName = "Administrator",
                    Email = "admin@forumsystem.com",
                    EmailConfirmed = true,
                }, "Admin123");

                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                }
                else
                {
                    user = await userManager.FindByNameAsync("Administrator");

                    await userManager.AddToRoleAsync(user, GlobalConstants.AdminstrationRoleName);
                }

            }
        }


    }
}
