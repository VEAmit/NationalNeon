using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace NationalNeon.Repository.Migrations
{
    public partial class jobfileuploadchangesmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_JobFileUploads_JobId",
                table: "JobFileUploads");

            migrationBuilder.CreateIndex(
                name: "IX_JobFileUploads_JobId",
                table: "JobFileUploads",
                column: "JobId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_JobFileUploads_JobId",
                table: "JobFileUploads");

            migrationBuilder.CreateIndex(
                name: "IX_JobFileUploads_JobId",
                table: "JobFileUploads",
                column: "JobId",
                unique: true,
                filter: "[JobId] IS NOT NULL");
        }
    }
}
