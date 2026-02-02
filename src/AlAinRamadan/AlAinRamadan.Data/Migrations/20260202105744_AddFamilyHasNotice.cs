using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlAinRamadan.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddFamilyHasNotice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasNotice",
                table: "Families",
                type: "INTEGER",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasNotice",
                table: "Families");
        }
    }
}
