using Microsoft.AspNetCore.Identity;

namespace WebProjectG.Server.domain.GebruikerFiles.RoleFiles
{
    public class SeedRoles
    {
        public static async Task Initialize(IServiceProvider serviceProvider, UserManager<Gebruiker> userManager)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string[] roleNames = { "Particulier", "ZakelijkeHuurder", "WagenparkBeheerder" };
            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
        }
    }
}
