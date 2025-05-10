using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameReservationTotalPriceColumnAndAddConstraint : Migration
    {
        /// <inheritdoc />
        protected override void Up( MigrationBuilder migrationBuilder )
        {
            migrationBuilder.RenameColumn(
                name: "TotalPrice",
                table: "Reservation",
                newName: "total_price" );

            migrationBuilder.AlterColumn<decimal>(
                name: "total_price",
                table: "Reservation",
                type: "decimal(10,2)",
                nullable: false,
                oldClrType: typeof( decimal ),
                oldType: "decimal(18,2)" );
        }

        /// <inheritdoc />
        protected override void Down( MigrationBuilder migrationBuilder )
        {
            migrationBuilder.RenameColumn(
                name: "total_price",
                table: "Reservation",
                newName: "TotalPrice" );

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalPrice",
                table: "Reservation",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof( decimal ),
                oldType: "decimal(10,2)" );
        }
    }
}
