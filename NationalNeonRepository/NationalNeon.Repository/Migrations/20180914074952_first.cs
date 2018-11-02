using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace NationalNeon.Repository.Migrations
{
    public partial class first : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    departmentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    created_by = table.Column<string>(nullable: true),
                    created_on = table.Column<DateTime>(nullable: false),
                    departmentname = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true),
                    updated_by = table.Column<string>(nullable: true),
                    updated_on = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.departmentId);
                });

            migrationBuilder.CreateTable(
                name: "Jobs",
                columns: table => new
                {
                    jobId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    created_by = table.Column<string>(nullable: true),
                    created_on = table.Column<DateTime>(nullable: false),
                    description = table.Column<string>(nullable: true),
                    job_name = table.Column<string>(nullable: true),
                    job_number = table.Column<int>(nullable: false),
                    revenue = table.Column<decimal>(nullable: false),
                    sales_person = table.Column<string>(nullable: true),
                    scheduled_date = table.Column<DateTime>(nullable: false),
                    status = table.Column<string>(nullable: true),
                    target_completion_date = table.Column<DateTime>(nullable: false),
                    updated_by = table.Column<string>(nullable: true),
                    updated_on = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jobs", x => x.jobId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    userId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    created_by = table.Column<string>(nullable: true),
                    created_on = table.Column<DateTime>(nullable: false),
                    password = table.Column<string>(nullable: true),
                    reset_expiry_date = table.Column<DateTime>(nullable: false),
                    reset_me_guid = table.Column<Guid>(nullable: false),
                    role = table.Column<string>(nullable: true),
                    unsuccesful_login_attempt = table.Column<int>(nullable: false),
                    updated_by = table.Column<string>(nullable: true),
                    updated_on = table.Column<DateTime>(nullable: false),
                    username = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.userId);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    TaskId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BudgetedHours = table.Column<int>(nullable: false),
                    Completed = table.Column<int>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    Employee = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    TargetCompletionDate = table.Column<DateTime>(nullable: false),
                    TaskName = table.Column<string>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: false),
                    departmentId = table.Column<int>(nullable: false),
                    jobId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.TaskId);
                    table.ForeignKey(
                        name: "FK_Tasks_Departments_departmentId",
                        column: x => x.departmentId,
                        principalTable: "Departments",
                        principalColumn: "departmentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tasks_Jobs_jobId",
                        column: x => x.jobId,
                        principalTable: "Jobs",
                        principalColumn: "jobId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_departmentId",
                table: "Tasks",
                column: "departmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_jobId",
                table: "Tasks",
                column: "jobId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Jobs");
        }
    }
}
