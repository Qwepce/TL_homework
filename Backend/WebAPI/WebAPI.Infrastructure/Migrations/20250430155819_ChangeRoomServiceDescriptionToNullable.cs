using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeRoomServiceDescriptionToNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up( MigrationBuilder migrationBuilder )
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "RoomService",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof( string ),
                oldType: "nvarchar(150)",
                oldMaxLength: 150 );
        }

        /// <inheritdoc />
        protected override void Down( MigrationBuilder migrationBuilder )
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "RoomService",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof( string ),
                oldType: "nvarchar(150)",
                oldMaxLength: 150,
                oldNullable: true );
        }
    }
}
