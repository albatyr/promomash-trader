using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Promomash.Trader.UserAccess.Domain.Countries;
using Promomash.Trader.UserAccess.Domain.Users;

namespace Promomash.Trader.UserAccess.Infrastructure.Domain.Users;

internal class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users", "users");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id)
            .HasConversion(id => id.Value, value => new UserId(value))
            .IsRequired();

        builder.Property(u => u.Login).IsRequired().HasMaxLength(100);
        builder.Property(u => u.Email).IsRequired().HasMaxLength(100);
        builder.Property(u => u.Password).IsRequired().HasMaxLength(100);
        builder.Property(u => u.IsAgreedToWorkForFood).IsRequired();

        builder.Property(e => e.ProvinceId)
            .HasConversion(
                v => v.ToString(),
                v => ProvinceId.FromString(v))
            .HasMaxLength(6)
            .IsRequired();
    }
}