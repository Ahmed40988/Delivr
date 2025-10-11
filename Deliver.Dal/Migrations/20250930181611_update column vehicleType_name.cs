using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Deliver.Dal.Migrations
{
    /// <inheritdoc />
    public partial class updatecolumnvehicleType_name : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_vehicleTypes_vehicleTypes_vehicleTypeId",
                table: "vehicleTypes");

            migrationBuilder.DropIndex(
                name: "IX_vehicleTypes_vehicleTypeId",
                table: "vehicleTypes");

            migrationBuilder.DropColumn(
                name: "vehicleTypeId",
                table: "vehicleTypes");

            migrationBuilder.AddColumn<int>(
                name: "vehicleType_name",
                table: "vehicleTypes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "vehicleType_name",
                table: "vehicleTypes");

            migrationBuilder.AddColumn<int>(
                name: "vehicleTypeId",
                table: "vehicleTypes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_vehicleTypes_vehicleTypeId",
                table: "vehicleTypes",
                column: "vehicleTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_vehicleTypes_vehicleTypes_vehicleTypeId",
                table: "vehicleTypes",
                column: "vehicleTypeId",
                principalTable: "vehicleTypes",
                principalColumn: "Id");
        }
    }
}
