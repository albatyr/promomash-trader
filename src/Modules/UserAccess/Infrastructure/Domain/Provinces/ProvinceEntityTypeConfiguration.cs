using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Promomash.Trader.UserAccess.Domain.Countries;

namespace Promomash.Trader.UserAccess.Infrastructure.Domain.Provinces;

public class ProvinceEntityTypeConfiguration : IEntityTypeConfiguration<Province>
{
    public void Configure(EntityTypeBuilder<Province> builder)
    {
        builder.ToTable("Provinces", "users");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .HasConversion(id => id.ToString(), value => ProvinceId.FromString(value))
            .HasMaxLength(10)
            .IsRequired();

        builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
        builder.Property(p => p.CountryCode).IsRequired().HasMaxLength(3);

        builder.HasOne<Country>()
            .WithMany()
            .HasForeignKey(p => p.CountryCode);
    }
}