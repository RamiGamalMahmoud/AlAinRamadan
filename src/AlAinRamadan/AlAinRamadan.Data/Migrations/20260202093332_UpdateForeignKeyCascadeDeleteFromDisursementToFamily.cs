using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlAinRamadan.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateForeignKeyCascadeDeleteFromDisursementToFamily : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Disbursements_Families_FamilyId",
                table: "Disbursements");

            migrationBuilder.AddForeignKey(
                name: "FK_Disbursements_Families_FamilyId",
                table: "Disbursements",
                column: "FamilyId",
                principalTable: "Families",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Disbursements_Families_FamilyId",
                table: "Disbursements");

            migrationBuilder.AddForeignKey(
                name: "FK_Disbursements_Families_FamilyId",
                table: "Disbursements",
                column: "FamilyId",
                principalTable: "Families",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
