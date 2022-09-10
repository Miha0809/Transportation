using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Transportation.Models;
using Transportation.Services;

namespace Transportation.Controllers.Customer;

public class AuthorizationCustomerController : Controller
{
    private readonly TransportationDbContext _context;
    private RefreshToken _refreshTokenDTO;

    public AuthorizationCustomerController(TransportationDbContext context)
    {
        _context = context;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(Models.Customer.Customer customer)
    {
        if (customer == null || !ModelState.IsValid)
        {
            return BadRequest();
        }

        customer.Password = Hash.GetHash(customer.Password);
        customer.Role = await _context.Roles.FirstOrDefaultAsync(role => role.Name.Equals("Customer"));

        await _context.Customers.AddAsync(customer);
        await _context.SaveChangesAsync();

        return Ok(customer);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] Login login)
    {
        var customer = await _context.Customers.FirstOrDefaultAsync(customer =>
            customer.Email.Equals(login.Email) && Hash.GetHash(login.Password).Equals(customer.Password));

        if (customer == null || !Hash.VerifyHash(login.Password, Hash.GetHash(login.Password)) || !ModelState.IsValid)
        {
            return BadRequest();
        }

        var accessToken = GenerateAccessToken(customer);
        var refreshToken = GenerateRefreshToken();
        var refreshTokenDTO = new RefreshToken()
        {
            Token = refreshToken,
            UserId = customer.Id
        };

        await _context.RefreshTokens.AddAsync(refreshTokenDTO);
        await _context.SaveChangesAsync();

        return Ok(new
        {
            access_token = accessToken,
            refresh_token = refreshToken
        });
    }

    [Authorize(Roles = "Customer")]
    [HttpDelete("logout")]
    public async Task<IActionResult> Logout()
    {
        int.TryParse(HttpContext.User.FindFirstValue("Id"), out var customerId);
        var refresh = await _context.RefreshTokens.FirstOrDefaultAsync(token => token.UserId.Equals(customerId));

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
        var refreshTokenOld = await _context.RefreshTokens.FirstOrDefaultAsync(token => token.Token.Equals(refresh.RefreshToken));
        var customer = await _context.Customers.FindAsync(refreshTokenOld.UserId);

        if (!isValid || refreshTokenOld == null || customer == null)
        {
            return BadRequest();
        }

        _context.RefreshTokens.Remove(refreshTokenOld);
        await _context.SaveChangesAsync();

        var accessToken = GenerateAccessToken(customer);
        var refreshTokenNew = GenerateRefreshToken();
        var refreshTokenDTO = new RefreshToken()
        {
            Token = refreshTokenNew,
            UserId = customer.Id
        };

        await _context.RefreshTokens.AddAsync(refreshTokenDTO);
        await _context.SaveChangesAsync();

        return Ok(new
        {
            access_token = accessToken,
            refresh_token = refreshTokenOld.Token
        });
    }

    private string GenerateAccessToken(Models.Customer.Customer customer)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("ACCESS_SECRET_KEY")));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
        var claims = new List<Claim>
        {
            new("Id", customer.Id.ToString()),
            new("Email", customer.Email),
            new("Role", customer.Role.Name)
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