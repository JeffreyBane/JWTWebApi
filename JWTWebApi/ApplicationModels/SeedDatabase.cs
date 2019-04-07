using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFXServices.ApplicationModels
{
    public class SeedDatabase
    {

        public static void Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<ApplicationDBContext>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            //context.Database.EnsureCreated();
            context.Database.Migrate();


            if (!context.Users.Any())
            {

                ApplicationUser user = new ApplicationUser()
                {
                    Email = "you@somewhere.com",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = "you"
                };

                userManager.CreateAsync(user, "1P@ssword123");
            }

        }
    }
}
