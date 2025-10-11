using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Deliver.Dal.Migrations
{
    /// <inheritdoc />
    public partial class updatedeliveryEnitiyaddcolumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhotoUrl",
                table: "Deliveries",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "city",
                table: "Deliveries",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoUrl",
                table: "Deliveries");

            migrationBuilder.DropColumn(
                name: "city",
                table: "Deliveries");
        }
    }
}
