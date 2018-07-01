using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SitemapWeb.Migrations
{
    public partial class AddSitemapSubmissionStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "SitemapSubmissions",
                maxLength: 512,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 512,
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "SitemapSubmissions",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "SitemapSubmissionStatusId",
                table: "SitemapSubmissions",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SitemapSubmissionStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SitemapSubmissionStatuses", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SitemapSubmissions_SitemapSubmissionStatusId",
                table: "SitemapSubmissions",
                column: "SitemapSubmissionStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_SitemapSubmissions_SitemapSubmissionStatuses_SitemapSubmissionStatusId",
                table: "SitemapSubmissions",
                column: "SitemapSubmissionStatusId",
                principalTable: "SitemapSubmissionStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SitemapSubmissions_SitemapSubmissionStatuses_SitemapSubmissionStatusId",
                table: "SitemapSubmissions");

            migrationBuilder.DropTable(
                name: "SitemapSubmissionStatuses");

            migrationBuilder.DropIndex(
                name: "IX_SitemapSubmissions_SitemapSubmissionStatusId",
                table: "SitemapSubmissions");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "SitemapSubmissions");

            migrationBuilder.DropColumn(
                name: "SitemapSubmissionStatusId",
                table: "SitemapSubmissions");

            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "SitemapSubmissions",
                maxLength: 512,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 512);
        }
    }
}
