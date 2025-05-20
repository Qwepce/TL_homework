using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebAPI.Domain.Models.Entities;

namespace WebAPI.Infrastructure.Database.Properties;

public class PropertyConfiguration : IEntityTypeConfiguration<Property>
{
    public void Configure( EntityTypeBuilder<Property> builder )
    {
        builder.ToTable( nameof( Property ) )
            .HasKey( p => p.Id );

        builder.Property( p => p.Id )
            .HasColumnName( "id" )
            .ValueGeneratedOnAdd();

        builder.Property( p => p.Name )
            .HasMaxLength( 100 )
            .HasColumnName( "name" )
            .IsRequired();

        builder.Property( p => p.Country )
            .HasMaxLength( 100 )
            .HasColumnName( "country" )
            .IsRequired();

        builder.Property( p => p.City )
            .HasMaxLength( 100 )
            .HasColumnName( "city" )
            .IsRequired();

        builder.Property( p => p.Address )
            .HasMaxLength( 100 )
            .HasColumnName( "address" )
            .IsRequired();

        builder.Property( p => p.Latitude )
            .HasColumnType( "decimal(9,6)" )
            .HasColumnName( "latitude" )
            .IsRequired();

        builder.Property( p => p.Longitude )
            .HasColumnType( "decimal(9,6)" )
            .HasColumnName( "longitude" )
            .IsRequired();

        builder.HasMany<RoomType>()
            .WithOne()
            .HasForeignKey( rt => rt.PropertyId );
    }
}
