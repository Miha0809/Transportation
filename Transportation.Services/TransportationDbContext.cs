using Microsoft.EntityFrameworkCore;
using Transportation.Models;
using Transportation.Models.CargoCompany;
using Transportation.Models.Customer;
using Transportation.Models.Driver;

namespace Transportation.Services;

public class TransportationDbContext : DbContext
{
    public TransportationDbContext(DbContextOptions<TransportationDbContext> opinion) : base(opinion)
    {
    }

    // Project
    public virtual DbSet<Role> Roles { get; set; }
    public virtual DbSet<RefreshToken> RefreshTokens { get; set; }
    public virtual DbSet<User> Users { get; set; }

    // Customer
    public virtual DbSet<Customer> Customers { get; set; }

    // CargoCompany
    public virtual DbSet<CargoCompany> CargoCompanies { get; set; }

    // Driver
    public virtual DbSet<Driver> Drivers { get; set; }
}