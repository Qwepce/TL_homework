using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameAllColumnsInSnakeCase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomType_Property_PropertyId",
                table: "RoomType");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomTypeAmenities_Amenity_AmenitiesId",
                table: "RoomTypeAmenities");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomTypeAmenities_RoomType_RoomTypesId",
                table: "RoomTypeAmenities");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomTypeServices_RoomService_ServicesId",
                table: "RoomTypeServices");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomTypeServices_RoomType_RoomTypesId",
                table: "RoomTypeServices");

            migrationBuilder.RenameColumn(
                name: "ServicesId",
                table: "RoomTypeServices",
                newName: "id_service");

            migrationBuilder.RenameColumn(
                name: "RoomTypesId",
                table: "RoomTypeServices",
                newName: "id_room_type");

            migrationBuilder.RenameIndex(
                name: "IX_RoomTypeServices_ServicesId",
                table: "RoomTypeServices",
                newName: "IX_RoomTypeServices_id_service");

            migrationBuilder.RenameColumn(
                name: "RoomTypesId",
                table: "RoomTypeAmenities",
                newName: "id_room_type");

            migrationBuilder.RenameColumn(
                name: "AmenitiesId",
                table: "RoomTypeAmenities",
                newName: "id_amenity");

            migrationBuilder.RenameIndex(
                name: "IX_RoomTypeAmenities_RoomTypesId",
                table: "RoomTypeAmenities",
                newName: "IX_RoomTypeAmenities_id_room_type");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "RoomType",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Currency",
                table: "RoomType",
                newName: "currency");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "RoomType",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "PropertyId",
                table: "RoomType",
                newName: "id_property");

            migrationBuilder.RenameColumn(
                name: "MinPersonCount",
                table: "RoomType",
                newName: "min_person_count");

            migrationBuilder.RenameColumn(
                name: "MaxPersonCount",
                table: "RoomType",
                newName: "max_person_count");

            migrationBuilder.RenameColumn(
                name: "DailyPrice",
                table: "RoomType",
                newName: "daily_price");

            migrationBuilder.RenameIndex(
                name: "IX_RoomType_PropertyId",
                table: "RoomType",
                newName: "IX_RoomType_id_property");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "RoomService",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "RoomService",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Property",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Longitude",
                table: "Property",
                newName: "longitude");

            migrationBuilder.RenameColumn(
                name: "Latitude",
                table: "Property",
                newName: "latitude");

            migrationBuilder.RenameColumn(
                name: "Country",
                table: "Property",
                newName: "country");

            migrationBuilder.RenameColumn(
                name: "City",
                table: "Property",
                newName: "city");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Property",
                newName: "address");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Property",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Amenity",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Amenity",
                newName: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomType_Property_id_property",
                table: "RoomType",
                column: "id_property",
                principalTable: "Property",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomTypeAmenities_Amenity_id_amenity",
                table: "RoomTypeAmenities",
                column: "id_amenity",
                principalTable: "Amenity",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomTypeAmenities_RoomType_id_room_type",
                table: "RoomTypeAmenities",
                column: "id_room_type",
                principalTable: "RoomType",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomTypeServices_RoomService_id_service",
                table: "RoomTypeServices",
                column: "id_service",
                principalTable: "RoomService",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomTypeServices_RoomType_id_room_type",
                table: "RoomTypeServices",
                column: "id_room_type",
                principalTable: "RoomType",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomType_Property_id_property",
                table: "RoomType");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomTypeAmenities_Amenity_id_amenity",
                table: "RoomTypeAmenities");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomTypeAmenities_RoomType_id_room_type",
                table: "RoomTypeAmenities");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomTypeServices_RoomService_id_service",
                table: "RoomTypeServices");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomTypeServices_RoomType_id_room_type",
                table: "RoomTypeServices");

            migrationBuilder.RenameColumn(
                name: "id_service",
                table: "RoomTypeServices",
                newName: "ServicesId");

            migrationBuilder.RenameColumn(
                name: "id_room_type",
                table: "RoomTypeServices",
                newName: "RoomTypesId");

            migrationBuilder.RenameIndex(
                name: "IX_RoomTypeServices_id_service",
                table: "RoomTypeServices",
                newName: "IX_RoomTypeServices_ServicesId");

            migrationBuilder.RenameColumn(
                name: "id_room_type",
                table: "RoomTypeAmenities",
                newName: "RoomTypesId");

            migrationBuilder.RenameColumn(
                name: "id_amenity",
                table: "RoomTypeAmenities",
                newName: "AmenitiesId");

            migrationBuilder.RenameIndex(
                name: "IX_RoomTypeAmenities_id_room_type",
                table: "RoomTypeAmenities",
                newName: "IX_RoomTypeAmenities_RoomTypesId");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "RoomType",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "currency",
                table: "RoomType",
                newName: "Currency");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "RoomType",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "min_person_count",
                table: "RoomType",
                newName: "MinPersonCount");

            migrationBuilder.RenameColumn(
                name: "max_person_count",
                table: "RoomType",
                newName: "MaxPersonCount");

            migrationBuilder.RenameColumn(
                name: "id_property",
                table: "RoomType",
                newName: "PropertyId");

            migrationBuilder.RenameColumn(
                name: "daily_price",
                table: "RoomType",
                newName: "DailyPrice");

            migrationBuilder.RenameIndex(
                name: "IX_RoomType_id_property",
                table: "RoomType",
                newName: "IX_RoomType_PropertyId");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "RoomService",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "RoomService",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Property",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "longitude",
                table: "Property",
                newName: "Longitude");

            migrationBuilder.RenameColumn(
                name: "latitude",
                table: "Property",
                newName: "Latitude");

            migrationBuilder.RenameColumn(
                name: "country",
                table: "Property",
                newName: "Country");

            migrationBuilder.RenameColumn(
                name: "city",
                table: "Property",
                newName: "City");

            migrationBuilder.RenameColumn(
                name: "address",
                table: "Property",
                newName: "Address");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Property",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Amenity",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Amenity",
                newName: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomType_Property_PropertyId",
                table: "RoomType",
                column: "PropertyId",
                principalTable: "Property",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomTypeAmenities_Amenity_AmenitiesId",
                table: "RoomTypeAmenities",
                column: "AmenitiesId",
                principalTable: "Amenity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomTypeAmenities_RoomType_RoomTypesId",
                table: "RoomTypeAmenities",
                column: "RoomTypesId",
                principalTable: "RoomType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomTypeServices_RoomService_ServicesId",
                table: "RoomTypeServices",
                column: "ServicesId",
                principalTable: "RoomService",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomTypeServices_RoomType_RoomTypesId",
                table: "RoomTypeServices",
                column: "RoomTypesId",
                principalTable: "RoomType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
