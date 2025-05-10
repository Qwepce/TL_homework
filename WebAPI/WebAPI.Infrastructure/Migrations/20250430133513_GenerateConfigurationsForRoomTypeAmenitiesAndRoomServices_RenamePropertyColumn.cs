using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class GenerateConfigurationsForRoomTypeAmenitiesAndRoomServices_RenamePropertyColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up( MigrationBuilder migrationBuilder )
        {
            migrationBuilder.RenameColumn(
                name: "Longtitude",
                table: "Property",
                newName: "Longitude" );

            migrationBuilder.CreateTable(
                name: "Amenity",
                columns: table => new
                {
                    Id = table.Column<int>( type: "int", nullable: false )
                        .Annotation( "SqlServer:Identity", "1, 1" ),
                    Name = table.Column<string>( type: "nvarchar(50)", maxLength: 50, nullable: false )
                },
                constraints: table =>
                {
                    table.PrimaryKey( "PK_Amenity", x => x.Id );
                } );

            migrationBuilder.CreateTable(
                name: "RoomService",
                columns: table => new
                {
                    Id = table.Column<int>( type: "int", nullable: false )
                        .Annotation( "SqlServer:Identity", "1, 1" ),
                    Name = table.Column<string>( type: "nvarchar(50)", maxLength: 50, nullable: false ),
                    Description = table.Column<string>( type: "nvarchar(150)", maxLength: 150, nullable: false )
                },
                constraints: table =>
                {
                    table.PrimaryKey( "PK_RoomService", x => x.Id );
                } );

            migrationBuilder.CreateTable(
                name: "RoomType",
                columns: table => new
                {
                    Id = table.Column<int>( type: "int", nullable: false )
                        .Annotation( "SqlServer:Identity", "1, 1" ),
                    PropertyId = table.Column<int>( type: "int", nullable: false ),
                    Name = table.Column<string>( type: "nvarchar(30)", maxLength: 30, nullable: false ),
                    DailyPrice = table.Column<decimal>( type: "decimal(10,2)", nullable: false ),
                    Currency = table.Column<string>( type: "nvarchar(3)", maxLength: 3, nullable: false ),
                    MinPersonCount = table.Column<byte>( type: "tinyint", nullable: false ),
                    MaxPersonCount = table.Column<byte>( type: "tinyint", nullable: false )
                },
                constraints: table =>
                {
                    table.PrimaryKey( "PK_RoomType", x => x.Id );
                    table.ForeignKey(
                        name: "FK_RoomType_Property_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Property",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade );
                } );

            migrationBuilder.CreateTable(
                name: "RoomTypeAmenities",
                columns: table => new
                {
                    AmenitiesId = table.Column<int>( type: "int", nullable: false ),
                    RoomTypesId = table.Column<int>( type: "int", nullable: false )
                },
                constraints: table =>
                {
                    table.PrimaryKey( "PK_RoomTypeAmenities", x => new { x.AmenitiesId, x.RoomTypesId } );
                    table.ForeignKey(
                        name: "FK_RoomTypeAmenities_Amenity_AmenitiesId",
                        column: x => x.AmenitiesId,
                        principalTable: "Amenity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade );
                    table.ForeignKey(
                        name: "FK_RoomTypeAmenities_RoomType_RoomTypesId",
                        column: x => x.RoomTypesId,
                        principalTable: "RoomType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade );
                } );

            migrationBuilder.CreateTable(
                name: "RoomTypeServices",
                columns: table => new
                {
                    RoomTypesId = table.Column<int>( type: "int", nullable: false ),
                    ServicesId = table.Column<int>( type: "int", nullable: false )
                },
                constraints: table =>
                {
                    table.PrimaryKey( "PK_RoomTypeServices", x => new { x.RoomTypesId, x.ServicesId } );
                    table.ForeignKey(
                        name: "FK_RoomTypeServices_RoomService_ServicesId",
                        column: x => x.ServicesId,
                        principalTable: "RoomService",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade );
                    table.ForeignKey(
                        name: "FK_RoomTypeServices_RoomType_RoomTypesId",
                        column: x => x.RoomTypesId,
                        principalTable: "RoomType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade );
                } );

            migrationBuilder.CreateIndex(
                name: "IX_RoomType_PropertyId",
                table: "RoomType",
                column: "PropertyId" );

            migrationBuilder.CreateIndex(
                name: "IX_RoomTypeAmenities_RoomTypesId",
                table: "RoomTypeAmenities",
                column: "RoomTypesId" );

            migrationBuilder.CreateIndex(
                name: "IX_RoomTypeServices_ServicesId",
                table: "RoomTypeServices",
                column: "ServicesId" );
        }

        /// <inheritdoc />
        protected override void Down( MigrationBuilder migrationBuilder )
        {
            migrationBuilder.DropTable(
                name: "RoomTypeAmenities" );

            migrationBuilder.DropTable(
                name: "RoomTypeServices" );

            migrationBuilder.DropTable(
                name: "Amenity" );

            migrationBuilder.DropTable(
                name: "RoomService" );

            migrationBuilder.DropTable(
                name: "RoomType" );

            migrationBuilder.RenameColumn(
                name: "Longitude",
                table: "Property",
                newName: "Longtitude" );
        }
    }
}
