using Microsoft.EntityFrameworkCore;
using Promomash.Trader.UserAccess.Domain.Countries;
using Promomash.Trader.UserAccess.Domain.Users;
using Promomash.Trader.UserAccess.Infrastructure.Domain.Countries;
using Promomash.Trader.UserAccess.Infrastructure.Domain.Provinces;
using Promomash.Trader.UserAccess.Infrastructure.Domain.Users;

namespace Promomash.Trader.UserAccess.Infrastructure;

public class UserAccessContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<Province> Provinces { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new CountryEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ProvinceEntityTypeConfiguration());
        
        // Seed data
        modelBuilder.Entity<Country>().HasData(
            Country.Create("USA", "United States"),
            Country.Create("CAN", "Canada")
        );

        modelBuilder.Entity<Province>().HasData(
            // USA States
            Province.Create("CA", "California", "USA"),
            Province.Create("TX", "Texas", "USA"),
            Province.Create("NY", "New York", "USA"),
            Province.Create("FL", "Florida", "USA"),
    
            // Canadian Provinces
            Province.Create("ON", "Ontario", "CAN"),
            Province.Create("QC", "Quebec", "CAN"),
            Province.Create("BC", "British Columbia", "CAN"),
            Province.Create("AB", "Alberta", "CAN")
        );

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasData(
                new
                {
                    Id = new UserId(Guid.Parse("b1e9f865-c0b1-4c3d-9fbc-080c5c67af9a")),
                    Login = "admin@example.com",
                    Email = "admin@example.com",
                    Password = "adminpass",
                    IsAgreedToWorkForFood = true,
                    ProvinceId = ProvinceId.Create("CA", "USA")
                }
            );
        });
    }
}