using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace apitoken.BLL
{
    public class LoginBLL
    {
        private readonly IServiceProvider _serviceProvider;

        public LoginBLL(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task CreateAdminRole()
        {
            var roleManager = _serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            if (!await roleManager.RoleExistsAsync("admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("admin"));
            }
        }

        public async Task AddUserToAdminRole(string userId)
        {
            var userManager = _serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            var user = await userManager.FindByIdAsync(userId);

            var isInRole = await userManager.IsInRoleAsync(user, "admin");
            if (!isInRole)
            {
                await userManager.AddToRoleAsync(user, "admin");
            }
        }


    }
}