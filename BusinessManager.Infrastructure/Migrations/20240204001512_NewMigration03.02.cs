using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NewMigration0302 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TypeTags");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Types");

            migrationBuilder.AlterColumn<bool>(
                name: "IsOvertime",
                table: "WorkSchedules",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "MedicalLeaves",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "MedicalLeaves",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "LeaveRequests",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "LeaveRequests",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "GraduationDate",
                table: "EmployeeEducations",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsOvertime",
                table: "WorkSchedules",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "MedicalLeaves",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "MedicalLeaves",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "LeaveRequests",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "LeaveRequests",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AlterColumn<DateTime>(
                name: "GraduationDate",
                table: "EmployeeEducations",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Types",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Types", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TypeTags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AttendanceId = table.Column<int>(type: "int", nullable: false),
                    CertificationId = table.Column<int>(type: "int", nullable: false),
                    ContractId = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    LanguageId = table.Column<int>(type: "int", nullable: false),
                    LeaveId = table.Column<int>(type: "int", nullable: false),
                    MedicalLeaveId = table.Column<int>(type: "int", nullable: false),
                    SkillId = table.Column<int>(type: "int", nullable: false),
                    TagId = table.Column<int>(type: "int", nullable: false),
                    TypeId = table.Column<int>(type: "int", nullable: false),
                    WorkScheduleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeTags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TypeTags_EmployeeAttendances_AttendanceId",
                        column: x => x.AttendanceId,
                        principalTable: "EmployeeAttendances",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TypeTags_EmployeeCertifications_CertificationId",
                        column: x => x.CertificationId,
                        principalTable: "EmployeeCertifications",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TypeTags_EmployeeLanguages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "EmployeeLanguages",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TypeTags_EmployeeSkills_SkillId",
                        column: x => x.SkillId,
                        principalTable: "EmployeeSkills",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TypeTags_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TypeTags_EmploymentContracts_ContractId",
                        column: x => x.ContractId,
                        principalTable: "EmploymentContracts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TypeTags_LeaveRequests_LeaveId",
                        column: x => x.LeaveId,
                        principalTable: "LeaveRequests",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TypeTags_MedicalLeaves_MedicalLeaveId",
                        column: x => x.MedicalLeaveId,
                        principalTable: "MedicalLeaves",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TypeTags_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TypeTags_Types_TypeId",
                        column: x => x.TypeId,
                        principalTable: "Types",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TypeTags_WorkSchedules_WorkScheduleId",
                        column: x => x.WorkScheduleId,
                        principalTable: "WorkSchedules",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TypeTags_AttendanceId",
                table: "TypeTags",
                column: "AttendanceId");

            migrationBuilder.CreateIndex(
                name: "IX_TypeTags_CertificationId",
                table: "TypeTags",
                column: "CertificationId");

            migrationBuilder.CreateIndex(
                name: "IX_TypeTags_ContractId",
                table: "TypeTags",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_TypeTags_EmployeeId",
                table: "TypeTags",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_TypeTags_LanguageId",
                table: "TypeTags",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_TypeTags_LeaveId",
                table: "TypeTags",
                column: "LeaveId");

            migrationBuilder.CreateIndex(
                name: "IX_TypeTags_MedicalLeaveId",
                table: "TypeTags",
                column: "MedicalLeaveId");

            migrationBuilder.CreateIndex(
                name: "IX_TypeTags_SkillId",
                table: "TypeTags",
                column: "SkillId");

            migrationBuilder.CreateIndex(
                name: "IX_TypeTags_TagId",
                table: "TypeTags",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_TypeTags_TypeId",
                table: "TypeTags",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TypeTags_WorkScheduleId",
                table: "TypeTags",
                column: "WorkScheduleId");
        }
    }
}
