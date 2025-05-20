using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangePricesColumnsForMoneyFormat : Migration
    {
        /// <inheritdoc />
        protected override void Up( MigrationBuilder migrationBuilder )
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "daily_price",
                table: "RoomType",
                type: "money",
                nullable: false,
                oldClrType: typeof( decimal ),
                oldType: "decimal(10,2)" );

            migrationBuilder.AlterColumn<decimal>(
                name: "total_price",
                table: "Reservation",
                type: "money",
                nullable: false,
                oldClrType: typeof( decimal ),
                oldType: "decimal(10,2)" );
        }

        /// <inheritdoc />
        protected override void Down( MigrationBuilder migrationBuilder )
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "daily_price",
                table: "RoomType",
                type: "decimal(10,2)",
                nullable: false,
                oldClrType: typeof( decimal ),
                oldType: "money" );

            migrationBuilder.AlterColumn<decimal>(
                name: "total_price",
                table: "Reservation",
                type: "decimal(10,2)",
                nullable: false,
                oldClrType: typeof( decimal ),
                oldType: "money" );
        }
    }
}
