using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebAPI.Domain.Models.Entities;

namespace WebAPI.Infrastructure.Database.RoomTypes;

public class RoomTypeConfiguration : IEntityTypeConfiguration<RoomType>
{
    public void Configure( EntityTypeBuilder<RoomType> builder )
    {
        builder.ToTable( nameof( RoomType ) )
            .HasKey( rt => rt.Id );

        builder.Property( rt => rt.Id )
            .HasColumnName( "id" )
            .ValueGeneratedOnAdd();

        builder.Property( rt => rt.Name )
            .IsRequired()
            .HasColumnName( "name" )
            .HasMaxLength( 30 );

        builder.Property( rt => rt.DailyPrice )
            .IsRequired()
            .HasColumnName( "daily_price" )
            .HasColumnType( "money" )
            .HasAnnotation( "MinValue", 0 );

        builder.Property( rt => rt.Currency )
            .HasColumnName( "currency" )
            .HasConversion<string>()
            .HasMaxLength( 3 );

        builder.Property( rt => rt.MinPersonCount )
            .IsRequired()
            .HasColumnName( "min_person_count" )
            .HasColumnType( "tinyint" )
            .HasAnnotation( "MinValue", 1 );

        builder.Property( rt => rt.MaxPersonCount )
            .IsRequired()
            .HasColumnName( "max_person_count" )
            .HasColumnType( "tinyint" )
            .HasAnnotation( "MinValue", 1 );

        builder.Property( rt => rt.TotalRoomsCount )
            .IsRequired()
            .HasColumnName( "total_rooms_count" )
            .HasColumnType( "tinyint" );

        builder.Property( rt => rt.PropertyId )
            .HasColumnName( "id_property" );

        builder.HasOne<Property>()
            .WithMany()
            .HasForeignKey( rt => rt.PropertyId )
            .OnDelete( DeleteBehavior.Cascade );

        builder.HasMany( rt => rt.RoomServices )
            .WithMany( s => s.RoomTypes )
            .UsingEntity<Dictionary<string, object>>(
                "RoomTypeServices",
                j => j.HasOne<RoomService>().WithMany().HasForeignKey( "id_service" ),
                j => j.HasOne<RoomType>().WithMany().HasForeignKey( "id_room_type" )
            );

        builder.HasMany( rt => rt.Amenities )
            .WithMany( a => a.RoomTypes )
            .UsingEntity<Dictionary<string, object>>(
                "RoomTypeAmenities",
                j => j.HasOne<Amenity>().WithMany().HasForeignKey( "id_amenity" ),
                j => j.HasOne<RoomType>().WithMany().HasForeignKey( "id_room_type" )
            );
    }
}
