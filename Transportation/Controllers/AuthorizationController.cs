using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using NuGet.Protocol;
using Transportation.Models;
using Transportation.Services;

namespace Transportation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthorizationController : Controller
{
    private readonly TransportationDbContext _context;
    private RefreshToken _refreshTokenDTO;

    public AuthorizationController(TransportationDbContext context)
    {
        _context = context;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] User user)
    {
        if (user == null || !ModelState.IsValid)
        {
            return BadRequest();
        }

        user.Password = Hash.GetHash(user.Password);

        if (user.GetType() == typeof(Models.CargoCompany.CargoCompany))
        {
            user.Role = await _context.Roles.FirstOrDefaultAsync(role => role.Name.Equals("CargoCompany"));
        }
        else if (user.GetType() == typeof(Models.Driver.Driver))
        {
            user.Role = await _context.Roles.FirstOrDefaultAsync(role => role.Name.Equals("Driver"));
        }
        else if (user.GetType() == typeof(Models.Customer.Customer))
        {
            user.Role = await _context.Roles.FirstOrDefaultAsync(role => role.Name.Equals("Customer"));
        }

        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        return Ok(user);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] Login login)
    {
        var users = await _context.Users.FirstOrDefaultAsync(company =>
            company.Email.Equals(login.Email) && Hash.GetHash(login.Password).Equals(company.Password));

        if (users == null || !Hash.VerifyHash(login.Password, Hash.GetHash(login.Password)) || !ModelState.IsValid)
        {
            return BadRequest();
        }

        var accessToken = GenerateAccessToken(users);
        var refreshToken = GenerateRefreshToken();
        var refreshTokenDTO = new RefreshToken()
        {
            Token = refreshToken,
            UserId = users.Id
        };

        await _context.RefreshTokens.AddAsync(refreshTokenDTO);
        await _context.SaveChangesAsync();

        return Ok(new
        {
            access_token = accessToken,
            refresh_token = refreshToken
        });
    }

    // [Authorize(Roles = "CargoCompany")] // TODO: freach all roles
    [Authorize]
    [HttpDelete("logout")]
    public async Task<IActionResult> Logout()
    {
        int.TryParse(HttpContext.User.FindFirstValue("Id"), out var userId);
        var refresh = await _context.RefreshTokens.FirstOrDefaultAsync(token => token.UserId.Equals(userId));

        if (refresh == null)
        {
            return BadRequest();
        }

        _context.RefreshTokens.Remove(refresh);
        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest refresh)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var isValid = Validate(refresh.RefreshToken);
        var refreshToken =
            await _context.RefreshTokens.FirstOrDefaultAsync(token => token.Token.Equals(refresh.RefreshToken));
        var user = await _context.Users.FirstOrDefaultAsync(user => user.Id.Equals(refreshToken.UserId));

        if (!isValid || refreshToken == null || user == null)
        {
            return BadRequest();
        }

        _context.RefreshTokens.Remove(refreshToken);
        await _context.SaveChangesAsync();

        var accessToken = GenerateAccessToken(user);
        var refreshToken2 = GenerateRefreshToken();
        var refreshTokenDTO = new RefreshToken()
        {
            Token = refreshToken2,
            UserId = user.Id
        };

        await _context.RefreshTokens.AddAsync(refreshTokenDTO);
        await _context.SaveChangesAsync();

        return Ok(new
        {
            access_token = accessToken,
            refresh_token = refreshToken.Token
        });
    }

    private string GenerateAccessToken(Models.User user)
    {
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("ACCESS_SECRET_KEY")));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
        var claims = new List<Claim>
        {
            new("Id", user.Id.ToString()),
            new("Email", user.Email),
            new("Role", user.Role.Name)
        };
        var issuer = Environment.GetEnvironmentVariable("ISSUER") ?? string.Empty;
        var audience = Environment.GetEnvironmentVariable("AUDIENCE") ?? string.Empty;
        int.TryParse(Environment.GetEnvironmentVariable("ACCESS_TIME_LIFE_KAY_MIN") ?? string.Empty, out var minutes);
        var token = new JwtSecurityToken(issuer: issuer,
            audience: audience,
            claims: claims,
            notBefore: DateTime.UtcNow,
            expires: DateTime.Now.AddMinutes(minutes),
            signingCredentials: credentials);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private string GenerateRefreshToken()
    {
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("REFRESH_SECRET_KEY")));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
        var issuer = Environment.GetEnvironmentVariable("ISSUER") ?? string.Empty;
        var audience = Environment.GetEnvironmentVariable("AUDIENCE") ?? string.Empty;
        int.TryParse(Environment.GetEnvironmentVariable("REFRESH_TIME_LIFE_KAY_MIN") ?? string.Empty, out var minutes);
        var token = new JwtSecurityToken(issuer: issuer,
            audience: audience,
            notBefore: DateTime.UtcNow,
            expires: DateTime.Now.AddMinutes(minutes),
            signingCredentials: credentials);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private bool Validate(string refresh)
    {
        var tokenValidationParameters = new TokenValidationParameters()
        {
            IssuerSigningKey =
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("REFRESH_SECRET_KEY"))),
            ValidIssuer = Environment.GetEnvironmentVariable("ISSUER"),
            ValidAudience = Environment.GetEnvironmentVariable("AUDIENCE"),
            ValidateIssuerSigningKey = true,
            ValidateIssuer = true,
            ValidateAudience = true,
            ClockSkew = TimeSpan.Zero
        };
        var token = new JwtSecurityTokenHandler();

        try
        {
            token.ValidateToken(refresh, tokenValidationParameters, out SecurityToken validated);
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
}