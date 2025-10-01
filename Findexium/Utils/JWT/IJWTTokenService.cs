using Microsoft.AspNetCore.Identity;

public interface IJwtTokenService
{
    string Create(IdentityUser user, IEnumerable<string> roles);
}

