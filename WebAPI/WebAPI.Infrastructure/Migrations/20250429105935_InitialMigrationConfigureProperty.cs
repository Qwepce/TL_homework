using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigrationConfigureProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up( MigrationBuilder migrationBuilder )
        {
            migrationBuilder.CreateTable(
                name: "Property",
                columns: table => new
                {
                    Id = table.Column<int>( type: "int", nullable: false )
                        .Annotation( "SqlServer:Identity", "1, 1" ),
                    Name = table.Column<string>( type: "nvarchar(100)", maxLength: 100, nullable: false ),
                    Country = table.Column<string>( type: "nvarchar(100)", maxLength: 100, nullable: false ),
                    City = table.Column<string>( type: "nvarchar(100)", maxLength: 100, nullable: false ),
                    Address = table.Column<string>( type: "nvarchar(100)", maxLength: 100, nullable: false ),
                    Latitude = table.Column<decimal>( type: "decimal(18,2)", nullable: false ),
                    Longtitude = table.Column<decimal>( type: "decimal(18,2)", nullable: false )
                },
                constraints: table =>
                {
                    table.PrimaryKey( "PK_Property", x => x.Id );
                } );
        }

        /// <inheritdoc />
        protected override void Down( MigrationBuilder migrationBuilder )
        {
            migrationBuilder.DropTable(
                name: "Property" );
        }
    }
}
