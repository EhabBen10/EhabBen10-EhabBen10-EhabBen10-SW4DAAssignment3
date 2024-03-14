using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SW4DAAssignment3.Migrations
{
    /// <inheritdoc />
    public partial class Migration3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GPScoordinates",
                table: "Supermarkets",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GPScoordinates",
                table: "Supermarkets");
        }
    }
}
