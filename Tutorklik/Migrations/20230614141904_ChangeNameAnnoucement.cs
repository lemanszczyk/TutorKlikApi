using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tutorklik.Migrations
{
    /// <inheritdoc />
    public partial class ChangeNameAnnoucement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Annoucements_AnnoucementId",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "AnnoucementId",
                table: "Comments",
                newName: "AnnouncementId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_AnnoucementId",
                table: "Comments",
                newName: "IX_Comments_AnnouncementId");

            migrationBuilder.RenameColumn(
                name: "AnnoucementName",
                table: "Annoucements",
                newName: "AnnouncementName");

            migrationBuilder.RenameColumn(
                name: "AnnoucementDescription",
                table: "Annoucements",
                newName: "AnnouncementDescription");

            migrationBuilder.RenameColumn(
                name: "AnnoucementId",
                table: "Annoucements",
                newName: "AnnouncementId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Annoucements_AnnouncementId",
                table: "Comments",
                column: "AnnouncementId",
                principalTable: "Annoucements",
                principalColumn: "AnnouncementId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Annoucements_AnnouncementId",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "AnnouncementId",
                table: "Comments",
                newName: "AnnoucementId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_AnnouncementId",
                table: "Comments",
                newName: "IX_Comments_AnnoucementId");

            migrationBuilder.RenameColumn(
                name: "AnnouncementName",
                table: "Annoucements",
                newName: "AnnoucementName");

            migrationBuilder.RenameColumn(
                name: "AnnouncementDescription",
                table: "Annoucements",
                newName: "AnnoucementDescription");

            migrationBuilder.RenameColumn(
                name: "AnnouncementId",
                table: "Annoucements",
                newName: "AnnoucementId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Annoucements_AnnoucementId",
                table: "Comments",
                column: "AnnoucementId",
                principalTable: "Annoucements",
                principalColumn: "AnnoucementId");
        }
    }
}
