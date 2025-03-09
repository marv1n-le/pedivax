using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PediVax.BusinessObjects.Migrations
{
    /// <inheritdoc />
    public partial class bigupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VaccineProfile_ChildProfile_ChildId",
                table: "VaccineProfile");

            migrationBuilder.DropTable(
                name: "VaccineProfileDetail");

            migrationBuilder.DropColumn(
                name: "Reaction",
                table: "VaccineProfile");

            migrationBuilder.AlterColumn<DateTime>(
                name: "VaccinationDate",
                table: "VaccineProfile",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<int>(
                name: "IsCompleted",
                table: "VaccineProfile",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "DiseaseId",
                table: "VaccineProfile",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "VaccineSchedulePersonal",
                columns: table => new
                {
                    VaccineSchedulePersonalId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VaccineScheduleId = table.Column<int>(type: "int", nullable: false),
                    ChildId = table.Column<int>(type: "int", nullable: false),
                    DiseaseId = table.Column<int>(type: "int", nullable: false),
                    VaccineId = table.Column<int>(type: "int", nullable: false),
                    DoseNumber = table.Column<int>(type: "int", nullable: false),
                    ScheduledDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsCompleted = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VaccineSchedulePersonal", x => x.VaccineSchedulePersonalId);
                    table.ForeignKey(
                        name: "FK_VaccineSchedulePersonal_ChildProfile_ChildId",
                        column: x => x.ChildId,
                        principalTable: "ChildProfile",
                        principalColumn: "ChildId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VaccineSchedulePersonal_Disease_DiseaseId",
                        column: x => x.DiseaseId,
                        principalTable: "Disease",
                        principalColumn: "DiseaseId");
                    table.ForeignKey(
                        name: "FK_VaccineSchedulePersonal_VaccineSchedule_VaccineScheduleId",
                        column: x => x.VaccineScheduleId,
                        principalTable: "VaccineSchedule",
                        principalColumn: "VaccineScheduleId");
                    table.ForeignKey(
                        name: "FK_VaccineSchedulePersonal_Vaccine_VaccineId",
                        column: x => x.VaccineId,
                        principalTable: "Vaccine",
                        principalColumn: "VaccineId");
                });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ModifiedDate", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2025, 3, 9, 14, 25, 7, 431, DateTimeKind.Utc).AddTicks(9697), new DateTime(2025, 3, 9, 14, 25, 7, 431, DateTimeKind.Utc).AddTicks(9700), "FjOsRdXx75N4f8bEaPMaPFLSQ4rgEan4YrOOAzijXlE=", "vWYf1gVHRwFNJMcHamJes4RapKonP4BSN2AoLQACnw0=" });

            migrationBuilder.CreateIndex(
                name: "IX_VaccineProfile_DiseaseId",
                table: "VaccineProfile",
                column: "DiseaseId");

            migrationBuilder.CreateIndex(
                name: "IX_VaccineSchedulePersonal_ChildId",
                table: "VaccineSchedulePersonal",
                column: "ChildId");

            migrationBuilder.CreateIndex(
                name: "IX_VaccineSchedulePersonal_DiseaseId",
                table: "VaccineSchedulePersonal",
                column: "DiseaseId");

            migrationBuilder.CreateIndex(
                name: "IX_VaccineSchedulePersonal_VaccineId",
                table: "VaccineSchedulePersonal",
                column: "VaccineId");

            migrationBuilder.CreateIndex(
                name: "IX_VaccineSchedulePersonal_VaccineScheduleId",
                table: "VaccineSchedulePersonal",
                column: "VaccineScheduleId");

            migrationBuilder.AddForeignKey(
                name: "FK_VaccineProfile_ChildProfile_ChildId",
                table: "VaccineProfile",
                column: "ChildId",
                principalTable: "ChildProfile",
                principalColumn: "ChildId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VaccineProfile_Disease_DiseaseId",
                table: "VaccineProfile",
                column: "DiseaseId",
                principalTable: "Disease",
                principalColumn: "DiseaseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VaccineProfile_ChildProfile_ChildId",
                table: "VaccineProfile");

            migrationBuilder.DropForeignKey(
                name: "FK_VaccineProfile_Disease_DiseaseId",
                table: "VaccineProfile");

            migrationBuilder.DropTable(
                name: "VaccineSchedulePersonal");

            migrationBuilder.DropIndex(
                name: "IX_VaccineProfile_DiseaseId",
                table: "VaccineProfile");

            migrationBuilder.DropColumn(
                name: "DiseaseId",
                table: "VaccineProfile");

            migrationBuilder.AlterColumn<DateTime>(
                name: "VaccinationDate",
                table: "VaccineProfile",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IsCompleted",
                table: "VaccineProfile",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Reaction",
                table: "VaccineProfile",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "VaccineProfileDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VaccineId = table.Column<int>(type: "int", nullable: false),
                    VaccineProfileId = table.Column<int>(type: "int", nullable: false),
                    VaccineScheduleId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DoseNumber = table.Column<int>(type: "int", nullable: false),
                    IsCompleted = table.Column<int>(type: "int", nullable: false),
                    ScheduledDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VaccineProfileDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VaccineProfileDetail_VaccineProfile_VaccineProfileId",
                        column: x => x.VaccineProfileId,
                        principalTable: "VaccineProfile",
                        principalColumn: "VaccineProfileId");
                    table.ForeignKey(
                        name: "FK_VaccineProfileDetail_VaccineSchedule_VaccineScheduleId",
                        column: x => x.VaccineScheduleId,
                        principalTable: "VaccineSchedule",
                        principalColumn: "VaccineScheduleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VaccineProfileDetail_Vaccine_VaccineId",
                        column: x => x.VaccineId,
                        principalTable: "Vaccine",
                        principalColumn: "VaccineId");
                });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ModifiedDate", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2025, 3, 8, 16, 59, 49, 833, DateTimeKind.Utc).AddTicks(440), new DateTime(2025, 3, 8, 16, 59, 49, 833, DateTimeKind.Utc).AddTicks(443), "A37PrWWQBqZ6WCG8RxJgRKRnLg882nTCHTDETCvMoO0=", "NoKA25mUkR3alOYDMYTvCiW32Bx7xDRTiwun5s6gzEo=" });

            migrationBuilder.CreateIndex(
                name: "IX_VaccineProfileDetail_VaccineId",
                table: "VaccineProfileDetail",
                column: "VaccineId");

            migrationBuilder.CreateIndex(
                name: "IX_VaccineProfileDetail_VaccineProfileId",
                table: "VaccineProfileDetail",
                column: "VaccineProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_VaccineProfileDetail_VaccineScheduleId",
                table: "VaccineProfileDetail",
                column: "VaccineScheduleId");

            migrationBuilder.AddForeignKey(
                name: "FK_VaccineProfile_ChildProfile_ChildId",
                table: "VaccineProfile",
                column: "ChildId",
                principalTable: "ChildProfile",
                principalColumn: "ChildId");
        }
    }
}
