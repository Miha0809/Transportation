using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Transportation.Models;
using Transportation.Services;

namespace Transportation.Controllers.CargoCompany;

public class AuthorizationCargoCompanyController : Controller
{
    private readonly TransportationDbContext _context;
    private RefreshToken _refreshTokenDTO;

    public AuthorizationCargoCompanyController(TransportationDbContext context)
    {
        _context = context;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(Models.CargoCompany.CargoCompany cargoCompany)
    {
        if (cargoCompany == null || !ModelState.IsValid)
        {
            return BadRequest();
        }

        cargoCompany.Password = Hash.GetHash(cargoCompany.Password);
        cargoCompany.Role = await _context.Roles.FirstOrDefaultAsync(role => role.Name.Equals("CargoCompany"));

        await _context.CargoCompanies.AddAsync(cargoCompany);
        await _context.SaveChangesAsync();
        
        return Ok(cargoCompany);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] Login login)
    {
        var cargoCompany = await _context.CargoCompanies.FirstOrDefaultAsync(company =>
            company.Email.Equals(login.Email) && Hash.GetHash(login.Password).Equals(company.Password));

        if (cargoCompany == null || !Hash.VerifyHash(login.Password, Hash.GetHash(login.Password)) || !ModelState.IsValid)
        {
            return BadRequest();
        }

        var accessToken = GenerateAccessToken(cargoCompany);
        var refreshToken = GenerateRefreshToken();
        var refreshTokenDTO = new RefreshToken()
        {
            Token = refreshToken,
            UserId = cargoCompany.Id
        };

        await _context.RefreshTokens.AddAsync(refreshTokenDTO);
        await _context.SaveChangesAsync();

        return Ok(new
        {
            access_token = accessToken,
            refresh_token = refreshToken
        });
    }

    [Authorize(Roles = "CargoCompany")]
    [HttpDelete("logout")]
    public async Task<IActionResult> Logout()
    {
        int.TryParse(HttpContext.User.FindFirstValue("Id"), out var cargoCompanyId);
        var refresh = await _context.RefreshTokens.FirstOrDefaultAsync(token => token.UserId.Equals(cargoCompanyId));

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
        var refreshToken = await _context.RefreshTokens.FirstOrDefaultAsync(token => token.Token.Equals(refresh.RefreshToken));
        var cargoCompany = await _context.CargoCompanies.FindAsync(refreshToken.UserId);

        if (!isValid || refreshToken == null || cargoCompany == null)
        {
            return BadRequest();
        }

        _context.RefreshTokens.Remove(refreshToken);
        await _context.SaveChangesAsync();

        var accessToken = GenerateAccessToken(cargoCompany);
        var refreshToken2 = GenerateRefreshToken();
        var refreshTokenDTO = new RefreshToken()
        {
            Token = refreshToken2,
            UserId = cargoCompany.Id
        };

        await _context.RefreshTokens.AddAsync(refreshTokenDTO);
        await _context.SaveChangesAsync();

        return Ok(new
        {
            access_token = accessToken,
            refresh_token = refreshToken.Token
        });
    }

    private string GenerateAccessToken(Models.CargoCompany.CargoCompany company)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("ACCESS_SECRET_KEY")));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
        var claims = new List<Claim>
        {
            new("Id", company.Id.ToString()),
            new("Email", company.Email),
            new("Role", company.Role.Name)
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
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("REFRESH_SECRET_KEY")));
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