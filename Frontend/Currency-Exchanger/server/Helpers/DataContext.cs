using Microsoft.EntityFrameworkCore;
using server.Entities;

namespace WebApi.Helpers
{
    public class DataContext : DbContext
    {
        protected readonly IConfiguration _configuration;
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<CurrencyPrice> CurrencyPrices { get; set; }

        public DataContext( IConfiguration configuration )
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring( DbContextOptionsBuilder options )
        {
            // Memory Databse for simplicity
            options.UseInMemoryDatabase( "TestDb" );
        }

        protected override void OnModelCreating( ModelBuilder modelBuilder )
        {
            modelBuilder.Entity<Currency>( b =>
            {
                b.HasKey( c => c.Code );
                b.Property( c => c.MaxPrice ).IsRequired();
                b.Property( c => c.MinPrice ).IsRequired();
            } );

            modelBuilder.Entity<CurrencyPrice>( b =>
            {
                b.HasKey( p => p.Id );
                b.Property( p => p.Id ).ValueGeneratedOnAdd();
                b.Property( p => p.Price ).IsRequired();
                b.Property( p => p.DateTime ).IsRequired();
                // b.OwnsOne(p => p.Currency);
                b.HasOne( p => p.Currency ).WithMany().HasForeignKey( p => p.CurrencyCode ).IsRequired();
            } );
        }
    }
}
