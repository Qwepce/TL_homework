using Microsoft.EntityFrameworkCore;
using WebAPI.Domain.Models.Entities;
using WebAPI.Infrastructure.Database.Amenities;
using WebAPI.Infrastructure.Database.Properties;
using WebAPI.Infrastructure.Database.Reservations;
using WebAPI.Infrastructure.Database.RoomServices;
using WebAPI.Infrastructure.Database.RoomTypes;

namespace WebAPI.Infrastructure.Database;
public class ApplicationDbContext : DbContext
{
    public DbSet<Property> Properties { get; set; }
    public DbSet<RoomType> RoomTypes { get; set; }
    public DbSet<Amenity> Amenities { get; set; }
    public DbSet<RoomService> RoomServices { get; set; }
    public DbSet<Reservation> Reservations { get; set; }

    public ApplicationDbContext( DbContextOptions options )
        : base( options )
    {

    }

    protected override void OnModelCreating( ModelBuilder modelBuilder )
    {
        base.OnModelCreating( modelBuilder );

        modelBuilder.ApplyConfiguration( new PropertyConfiguration() );
        modelBuilder.ApplyConfiguration( new RoomTypeConfiguration() );
        modelBuilder.ApplyConfiguration( new AmenityConfiguration() );
        modelBuilder.ApplyConfiguration( new RoomServiceConfiguration() );
        modelBuilder.ApplyConfiguration( new ReservationConfiguration() );
    }
}
