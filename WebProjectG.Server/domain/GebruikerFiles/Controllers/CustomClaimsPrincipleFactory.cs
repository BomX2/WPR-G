using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using WebProjectG.Server.domain.GebruikerFiles;

public class CustomClaimsPrincipalFactory
    : UserClaimsPrincipalFactory<Gebruiker, IdentityRole>
{
    public CustomClaimsPrincipalFactory(
        UserManager<Gebruiker> userManager,
        RoleManager<IdentityRole> roleManager,
        IOptions<IdentityOptions> optionsAccessor)
        : base(userManager, roleManager, optionsAccessor)
    {
    }

    protected override async Task<ClaimsIdentity> GenerateClaimsAsync(Gebruiker user)
    {
        var identity = await base.GenerateClaimsAsync(user);

        // Add custom claims here
        if (!string.IsNullOrEmpty(user.Adres))
        {
            identity.AddClaim(new Claim(ClaimTypes.StreetAddress, user.Adres));
        }
        if (!string.IsNullOrEmpty(user.PhoneNumber))
        {
            identity.AddClaim(new Claim(ClaimTypes.MobilePhone, user.PhoneNumber));
        }
        identity.AddClaim(new Claim("TwoFactorEnabled", user.TwoFactorEnabled.ToString()));

        return identity;
    }
}