using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Deliver.Dal.Migrations
{
    /// <inheritdoc />
    public partial class updatevehicle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "vehicleTypes",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
