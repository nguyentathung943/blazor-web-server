using Common;
using DataAccess.Data;
using HiddenVilla_Server.Service.IService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HiddenVilla_Server.Service;

public class DbInitializer
{
    public static void Initialize(IApplicationBuilder applicationBuilder)
    {
        using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
        {
            var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
            var _roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
            var _userManager =  serviceScope.ServiceProvider.GetService<UserManager<IdentityUser>>();
            
            context.Database.EnsureCreated();
            try
            {
                if (context.Database.GetPendingMigrations().Count() > 0)
                {
                    context.Database.Migrate();
                }
            }
            catch (Exception ex)
            {
            
            }
            
            if (context.Roles.Any(x => x.Name == SD.Role_Admin)) return;
        
            _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(SD.Role_Customer)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(SD.Role_Employee)).GetAwaiter().GetResult();
        
            _userManager.CreateAsync(new IdentityUser
            {
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                EmailConfirmed = true
            }, "Admin123*").GetAwaiter().GetResult();

            IdentityUser user = context.Users.FirstOrDefault(u => u.Email == "admin@gmail.com");
            _userManager.AddToRoleAsync(user, SD.Role_Admin).GetAwaiter().GetResult();
        }
    }
}