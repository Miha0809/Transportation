using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Transportation.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();
builder.Services.AddCors();
builder.Services.AddTransient<IAuthorizationHandler, RolesInDBAuthorizationHandler>();
builder.Services.AddDbContext<TransportationDbContext>(option =>
{
    option.UseLazyLoadingProxies()
        .UseNpgsql(builder.Configuration.GetConnectionString("Default"));
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(jwt =>
    {
        jwt.TokenValidationParameters = new TokenValidationParameters()
        {
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("ACCESS_SECRET_KEY") ?? String.Empty)),
            ValidIssuer = Environment.GetEnvironmentVariable("ISSUER"),
            ValidAudience = Environment.GetEnvironmentVariable("AUDIENCE"),
            ValidateIssuerSigningKey = true,
            ValidateIssuer = true,
            ValidateAudience = true,
            ClockSkew = TimeSpan.Zero
        };
    });

var app = builder.Build();

// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors(options =>
{
    options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
});
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    // endpoints.MapControllerRoute(
    //     name: "default",
    //     pattern: "{controller=Home}/{action=Index}/{id?}");
    endpoints.MapControllers();
});

app.Run();