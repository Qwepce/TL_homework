using Microsoft.EntityFrameworkCore;
using WebAPI.Domain.Models.Entities;
using WebAPI.Infrastructure.Database.Configurations;

namespace WebAPI.Infrastructure.Database;
public class ApplicationDbContext : DbContext
{
    public DbSet<Property> Properties { get; set; }
    public DbSet<RoomType> RoomTypes { get; set; }
    public DbSet<Amenity> Amenities { get; set; }
    public DbSet<RoomService> RoomServices { get; set; }
    public DbSet<Reservation> Reservations { get; set; }

    public ApplicationDbContext() : base()
    {

    }

    protected override void OnConfiguring( DbContextOptionsBuilder optionsBuilder )
    {
        base.OnConfiguring( optionsBuilder );

        optionsBuilder.UseSqlServer( "Server=localhost;Database=WebAPI;Trusted_Connection=True;TrustServerCertificate=true;" );
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
