using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/Auth")]
public class AuthController : ControllerBase
{
    private readonly UserManager<IdentityUser> _users;
    private readonly IJwtTokenService _tokens;

    public AuthController(UserManager<IdentityUser> users, IJwtTokenService tokens)
    { _users = users; _tokens = tokens; }

    public record RegisterDto(string Email, string Password);
    public record LoginDto(string Email, string Password);

    [HttpPost("Register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        var user = new IdentityUser
        {
            UserName = dto.Email,
            Email = dto.Email,
            EmailConfirmed = true
        };
        var res = await _users.CreateAsync(user, dto.Password);
        if (!res.Succeeded) return BadRequest(res.Errors.Select(e => e.Description));

        await _users.AddToRoleAsync(user, "User");

        return Ok(new { user.Id, user.Email });
    }

    [HttpPost("Login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var user = await _users.FindByEmailAsync(dto.Email);
        if (user is null) return Unauthorized("Identifiants invalides.");

        var ok = await _users.CheckPasswordAsync(user, dto.Password);
        if (!ok) return Unauthorized("Identifiants invalides.");

        var roles = await _users.GetRolesAsync(user);
        var token = _tokens.Create(user, roles);
        return Ok(new { token });
    }
}
