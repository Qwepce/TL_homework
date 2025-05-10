using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebAPI.Domain.Models.Entities;

namespace WebAPI.Infrastructure.Database.Configurations;

public class RoomServiceConfiguration : IEntityTypeConfiguration<RoomService>
{
    public void Configure( EntityTypeBuilder<RoomService> builder )
    {
        builder.ToTable( nameof( RoomService ) )
            .HasKey( rs => rs.Id );

        builder.Property( rs => rs.Id )
            .HasColumnName( "id" )
            .ValueGeneratedOnAdd();

        builder.Property( rs => rs.Name )
            .IsRequired()
            .HasColumnName( "name" )
            .HasMaxLength( 50 );
    }
}
