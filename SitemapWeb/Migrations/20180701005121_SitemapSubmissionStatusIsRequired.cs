using Microsoft.EntityFrameworkCore.Migrations;

namespace SitemapWeb.Migrations
{
    public partial class SitemapSubmissionStatusIsRequired : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SitemapSubmissions_SitemapSubmissionStatuses_SitemapSubmissionStatusId",
                table: "SitemapSubmissions");

            migrationBuilder.AlterColumn<int>(
                name: "SitemapSubmissionStatusId",
                table: "SitemapSubmissions",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SitemapSubmissions_SitemapSubmissionStatuses_SitemapSubmissionStatusId",
                table: "SitemapSubmissions",
                column: "SitemapSubmissionStatusId",
                principalTable: "SitemapSubmissionStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SitemapSubmissions_SitemapSubmissionStatuses_SitemapSubmissionStatusId",
                table: "SitemapSubmissions");

            migrationBuilder.AlterColumn<int>(
                name: "SitemapSubmissionStatusId",
                table: "SitemapSubmissions",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_SitemapSubmissions_SitemapSubmissionStatuses_SitemapSubmissionStatusId",
                table: "SitemapSubmissions",
                column: "SitemapSubmissionStatusId",
                principalTable: "SitemapSubmissionStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
