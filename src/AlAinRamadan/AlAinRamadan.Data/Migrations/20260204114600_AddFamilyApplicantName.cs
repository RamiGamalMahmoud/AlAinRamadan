using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlAinRamadan.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddFamilyApplicantName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicantName",
                table: "Families",
                type: "nvarchar(100)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApplicantName",
                table: "Families");
        }
    }
}
