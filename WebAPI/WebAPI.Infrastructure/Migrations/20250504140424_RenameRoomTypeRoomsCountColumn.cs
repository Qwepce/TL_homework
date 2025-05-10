using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameRoomTypeRoomsCountColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up( MigrationBuilder migrationBuilder )
        {
            migrationBuilder.RenameColumn(
                name: "available_rooms_count",
                table: "RoomType",
                newName: "total_rooms_count" );
        }

        /// <inheritdoc />
        protected override void Down( MigrationBuilder migrationBuilder )
        {
            migrationBuilder.RenameColumn(
                name: "total_rooms_count",
                table: "RoomType",
                newName: "available_rooms_count" );
        }
    }
}
