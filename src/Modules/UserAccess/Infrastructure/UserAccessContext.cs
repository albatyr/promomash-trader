using Microsoft.EntityFrameworkCore;
using Promomash.Trader.UserAccess.Application.Users.RegisterUser;
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
            Country.Create("CAN", "Canada"));

        modelBuilder.Entity<Province>().HasData(
            Province.Create("CA", "California", "USA"),
            Province.Create("TX", "Texas", "USA"),
            Province.Create("NY", "New York", "USA"),
            Province.Create("FL", "Florida", "USA"),
            Province.Create("ON", "Ontario", "CAN"),
            Province.Create("QC", "Quebec", "CAN"),
            Province.Create("BC", "British Columbia", "CAN"),
            Province.Create("AB", "Alberta", "CAN"));

        modelBuilder.Entity<User>(
            entity =>
            {
                entity.HasData(
                    new
                    {
                        Id = new UserId(Guid.Parse("018f9b8e-95e2-7a40-b43a-7057c1c5d4e0")),
                        Login = "admin@example.com",
                        Email = "admin@example.com",
                        Password = "AGYjn9TRYyKoR8TlVqo6jdD64ObqSYwIqLsXXA4iOAukGYSl3Zizeqatrsq+pdqHLQ==",
                        IsAgreedToWorkForFood = true,
                        ProvinceId = ProvinceId.Create("CA", "USA")
                    });
            });
    }
}