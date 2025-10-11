using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Deliver.Dal.Migrations
{
    /// <inheritdoc />
    public partial class Addfluentapiconfigrationadressandrelatedtables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_addresses_UserId",
                table: "addresses");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "zones",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "streets",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "governments",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "cities",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_zones_Name",
                table: "zones",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_zones_Name_CityId",
                table: "zones",
                columns: new[] { "Name", "CityId" },
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_streets_Name",
                table: "streets",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_streets_Name_ZoneId",
                table: "streets",
                columns: new[] { "Name", "ZoneId" },
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_governments_Name",
                table: "governments",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_governments_Name_Id",
                table: "governments",
                columns: new[] { "Name", "Id" },
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_cities_Name",
                table: "cities",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_cities_Name_GovernmentId",
                table: "cities",
                columns: new[] { "Name", "GovernmentId" },
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_addresses_UserId_StreetId",
                table: "addresses",
                columns: new[] { "UserId", "StreetId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_zones_Name",
                table: "zones");

            migrationBuilder.DropIndex(
                name: "IX_zones_Name_CityId",
                table: "zones");

            migrationBuilder.DropIndex(
                name: "IX_streets_Name",
                table: "streets");

            migrationBuilder.DropIndex(
                name: "IX_streets_Name_ZoneId",
                table: "streets");

            migrationBuilder.DropIndex(
                name: "IX_governments_Name",
                table: "governments");

            migrationBuilder.DropIndex(
                name: "IX_governments_Name_Id",
                table: "governments");

            migrationBuilder.DropIndex(
                name: "IX_cities_Name",
                table: "cities");

            migrationBuilder.DropIndex(
                name: "IX_cities_Name_GovernmentId",
                table: "cities");

            migrationBuilder.DropIndex(
                name: "IX_addresses_UserId_StreetId",
                table: "addresses");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "zones",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "streets",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "governments",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "cities",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_addresses_UserId",
                table: "addresses",
                column: "UserId");
        }
    }
}
