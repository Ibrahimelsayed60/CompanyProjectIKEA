using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LinkDev.IKEA.DAL.Persistance.Data.Migrations
{
    /// <inheritdoc />
    public partial class ImageColumnForEmployeeMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Employees");
        }
    }
}
