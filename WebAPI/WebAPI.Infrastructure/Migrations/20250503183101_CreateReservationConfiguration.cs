using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateReservationConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up( MigrationBuilder migrationBuilder )
        {
            migrationBuilder.CreateTable(
                name: "Reservation",
                columns: table => new
                {
                    id = table.Column<int>( type: "int", nullable: false )
                        .Annotation( "SqlServer:Identity", "1, 1" ),
                    id_property = table.Column<int>( type: "int", nullable: false ),
                    id_room_type = table.Column<int>( type: "int", nullable: false ),
                    arrival_date = table.Column<DateTime>( type: "date", nullable: false ),
                    arrival_time = table.Column<DateTime>( type: "datetime2", nullable: false ),
                    departure_date = table.Column<DateTime>( type: "date", nullable: false ),
                    departure_time = table.Column<DateTime>( type: "datetime2", nullable: false ),
                    guest_name = table.Column<string>( type: "nvarchar(150)", maxLength: 150, nullable: false ),
                    guest_phone_number = table.Column<string>( type: "nvarchar(20)", maxLength: 20, nullable: false ),
                    currency = table.Column<string>( type: "nvarchar(3)", maxLength: 3, nullable: false ),
                    TotalPrice = table.Column<decimal>( type: "decimal(18,2)", nullable: false )
                },
                constraints: table =>
                {
                    table.PrimaryKey( "PK_Reservation", x => x.id );
                    table.ForeignKey(
                        name: "FK_Reservation_Property_id_property",
                        column: x => x.id_property,
                        principalTable: "Property",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict );
                    table.ForeignKey(
                        name: "FK_Reservation_RoomType_id_room_type",
                        column: x => x.id_room_type,
                        principalTable: "RoomType",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict );
                } );

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_id_property",
                table: "Reservation",
                column: "id_property" );

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_id_room_type",
                table: "Reservation",
                column: "id_room_type" );
        }

        /// <inheritdoc />
        protected override void Down( MigrationBuilder migrationBuilder )
        {
            migrationBuilder.DropTable(
                name: "Reservation" );
        }
    }
}
