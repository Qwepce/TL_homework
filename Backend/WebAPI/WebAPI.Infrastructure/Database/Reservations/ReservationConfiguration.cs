using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebAPI.Domain.Models.Entities;

namespace WebAPI.Infrastructure.Database.Reservations;

public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
{
    public void Configure( EntityTypeBuilder<Reservation> builder )
    {
        builder.ToTable( nameof( Reservation ) )
            .HasKey( r => r.Id );

        builder.Property( r => r.Id )
            .HasColumnName( "id" )
            .ValueGeneratedOnAdd();

        builder.Property( r => r.PropertyId )
            .HasColumnName( "id_property" )
            .IsRequired();

        builder.Property( r => r.RoomTypeId )
            .HasColumnName( "id_room_type" )
            .IsRequired();

        builder.Property( r => r.ArrivalDate )
            .HasColumnName( "arrival_date" )
            .HasColumnType( "date" )
            .IsRequired();

        builder.Property( r => r.ArrivalTime )
            .HasColumnName( "arrival_time" )
            .HasColumnType( "datetime2" )
            .IsRequired();

        builder.Property( r => r.DepartureDate )
            .HasColumnName( "departure_date" )
            .HasColumnType( "date" )
            .IsRequired();

        builder.Property( r => r.DepartureTime )
            .HasColumnName( "departure_time" )
            .HasColumnType( "datetime2" )
            .IsRequired();

        builder.Property( r => r.GuestName )
            .HasColumnName( "guest_name" )
            .IsRequired()
            .HasMaxLength( 150 );

        builder.Property( r => r.GuestPhoneNumber )
            .HasColumnName( "guest_phone_number" )
            .IsRequired()
            .HasMaxLength( 20 );

        builder.Property( r => r.ReservationCurrency )
            .HasColumnName( "currency" )
            .HasConversion<string>()
            .IsRequired()
            .HasMaxLength( 3 );

        builder.Property( r => r.TotalPrice )
            .HasColumnName( "total_price" )
            .HasColumnType( "money" )
            .HasAnnotation( "MinValue", 0 );

        builder.HasOne<Property>()
            .WithMany()
            .HasForeignKey( r => r.PropertyId )
            .OnDelete( DeleteBehavior.Restrict );

        builder.HasOne<RoomType>()
            .WithMany()
            .HasForeignKey( r => r.RoomTypeId )
            .OnDelete( DeleteBehavior.Restrict );
    }
}
