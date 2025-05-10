using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebAPI.Domain.Models.Entities;

namespace WebAPI.Infrastructure.Database.Configurations;

public class AmenityConfiguration : IEntityTypeConfiguration<Amenity>
{
    public void Configure( EntityTypeBuilder<Amenity> builder )
    {
        builder.ToTable( nameof( Amenity ) )
            .HasKey( a => a.Id );

        builder.Property( a => a.Id )
            .HasColumnName( "id" )
            .ValueGeneratedOnAdd();

        builder.Property( a => a.Name )
            .HasColumnName( "name" )
            .IsRequired()
            .HasMaxLength( 50 );
    }
}
