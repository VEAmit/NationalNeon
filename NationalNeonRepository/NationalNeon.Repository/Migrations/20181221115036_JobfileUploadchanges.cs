using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace NationalNeon.Repository.Migrations
{
    public partial class JobfileUploadchanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "fileName",
                table: "Jobs",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "JobFileUploads",
                columns: table => new
                {
                    JobFileUploadId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FilePath = table.Column<string>(nullable: true),
                    JobId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobFileUploads", x => x.JobFileUploadId);
                    table.ForeignKey(
                        name: "FK_JobFileUploads_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "jobId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobFileUploads_JobId",
                table: "JobFileUploads",
                column: "JobId",
                unique: true,
                filter: "[JobId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobFileUploads");

            migrationBuilder.DropColumn(
                name: "fileName",
                table: "Jobs");
        }
    }
}
