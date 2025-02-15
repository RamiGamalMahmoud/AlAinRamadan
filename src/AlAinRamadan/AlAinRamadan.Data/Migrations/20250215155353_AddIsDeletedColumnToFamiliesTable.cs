using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlAinRamadan.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddIsDeletedColumnToFamiliesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Families",
                type: "INTEGER",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Families");
        }
    }
}
