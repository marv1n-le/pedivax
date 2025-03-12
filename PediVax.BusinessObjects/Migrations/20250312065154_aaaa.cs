using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PediVax.BusinessObjects.Migrations
{
    /// <inheritdoc />
    public partial class aaaa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Disease",
                columns: table => new
                {
                    DiseaseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Disease", x => x.DiseaseId);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordSalt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Vaccine",
                columns: table => new
                {
                    VaccineId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Origin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Manufacturer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DateOfManufacture = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vaccine", x => x.VaccineId);
                });

            migrationBuilder.CreateTable(
                name: "VaccinePackage",
                columns: table => new
                {
                    PackageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalDoses = table.Column<int>(type: "int", nullable: false),
                    AgeInMonths = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VaccinePackage", x => x.PackageId);
                });

            migrationBuilder.CreateTable(
                name: "VaccineSchedule",
                columns: table => new
                {
                    VaccineScheduleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DiseaseId = table.Column<int>(type: "int", nullable: false),
                    AgeInMonths = table.Column<int>(type: "int", nullable: false),
                    DoseNumber = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VaccineSchedule", x => x.VaccineScheduleId);
                    table.ForeignKey(
                        name: "FK_VaccineSchedule_Disease_DiseaseId",
                        column: x => x.DiseaseId,
                        principalTable: "Disease",
                        principalColumn: "DiseaseId");
                });

            migrationBuilder.CreateTable(
                name: "ChildProfile",
                columns: table => new
                {
                    ChildId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    Relationship = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChildProfile", x => x.ChildId);
                    table.ForeignKey(
                        name: "FK_ChildProfile_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "VaccineDisease",
                columns: table => new
                {
                    VaccineDiseaseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VaccineId = table.Column<int>(type: "int", nullable: false),
                    DiseaseId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VaccineDisease", x => x.VaccineDiseaseId);
                    table.ForeignKey(
                        name: "FK_VaccineDisease_Disease_DiseaseId",
                        column: x => x.DiseaseId,
                        principalTable: "Disease",
                        principalColumn: "DiseaseId");
                    table.ForeignKey(
                        name: "FK_VaccineDisease_Vaccine_VaccineId",
                        column: x => x.VaccineId,
                        principalTable: "Vaccine",
                        principalColumn: "VaccineId");
                });

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    PaymentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VaccinePackageId = table.Column<int>(type: "int", nullable: true),
                    VaccineId = table.Column<int>(type: "int", nullable: true),
                    PaymentType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaymentStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.PaymentId);
                    table.ForeignKey(
                        name: "FK_Payment_VaccinePackage_VaccinePackageId",
                        column: x => x.VaccinePackageId,
                        principalTable: "VaccinePackage",
                        principalColumn: "PackageId");
                    table.ForeignKey(
                        name: "FK_Payment_Vaccine_VaccineId",
                        column: x => x.VaccineId,
                        principalTable: "Vaccine",
                        principalColumn: "VaccineId");
                });

            migrationBuilder.CreateTable(
                name: "VaccinePackageDetail",
                columns: table => new
                {
                    PackageDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PackageId = table.Column<int>(type: "int", nullable: false),
                    VaccineId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VaccinePackageDetail", x => x.PackageDetailId);
                    table.ForeignKey(
                        name: "FK_VaccinePackageDetail_VaccinePackage_PackageId",
                        column: x => x.PackageId,
                        principalTable: "VaccinePackage",
                        principalColumn: "PackageId");
                    table.ForeignKey(
                        name: "FK_VaccinePackageDetail_Vaccine_VaccineId",
                        column: x => x.VaccineId,
                        principalTable: "Vaccine",
                        principalColumn: "VaccineId");
                });

            migrationBuilder.CreateTable(
                name: "Appointment",
                columns: table => new
                {
                    AppointmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentId = table.Column<int>(type: "int", nullable: true),
                    ChildId = table.Column<int>(type: "int", nullable: false),
                    VaccineId = table.Column<int>(type: "int", nullable: true),
                    VaccinePackageId = table.Column<int>(type: "int", nullable: true),
                    AppointmentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Reaction = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AppointmentStatus = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointment", x => x.AppointmentId);
                    table.ForeignKey(
                        name: "FK_Appointment_ChildProfile_ChildId",
                        column: x => x.ChildId,
                        principalTable: "ChildProfile",
                        principalColumn: "ChildId");
                    table.ForeignKey(
                        name: "FK_Appointment_Payment_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "Payment",
                        principalColumn: "PaymentId");
                    table.ForeignKey(
                        name: "FK_Appointment_VaccinePackage_VaccinePackageId",
                        column: x => x.VaccinePackageId,
                        principalTable: "VaccinePackage",
                        principalColumn: "PackageId");
                    table.ForeignKey(
                        name: "FK_Appointment_Vaccine_VaccineId",
                        column: x => x.VaccineId,
                        principalTable: "Vaccine",
                        principalColumn: "VaccineId");
                });

            migrationBuilder.CreateTable(
                name: "PaymentDetail",
                columns: table => new
                {
                    PaymentDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentId = table.Column<int>(type: "int", nullable: false),
                    VaccinePackageId = table.Column<int>(type: "int", nullable: false),
                    IsCompleted = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdministeredDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentDetail", x => x.PaymentDetailId);
                    table.ForeignKey(
                        name: "FK_PaymentDetail_Payment_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "Payment",
                        principalColumn: "PaymentId");
                    table.ForeignKey(
                        name: "FK_PaymentDetail_VaccinePackage_VaccinePackageId",
                        column: x => x.VaccinePackageId,
                        principalTable: "VaccinePackage",
                        principalColumn: "PackageId");
                });

            migrationBuilder.CreateTable(
                name: "VaccineProfile",
                columns: table => new
                {
                    VaccineProfileId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VaccineScheduleId = table.Column<int>(type: "int", nullable: false),
                    AppointmentId = table.Column<int>(type: "int", nullable: true),
                    ChildId = table.Column<int>(type: "int", nullable: false),
                    DiseaseId = table.Column<int>(type: "int", nullable: false),
                    VaccinationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DoseNumber = table.Column<int>(type: "int", nullable: false),
                    ScheduledDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsCompleted = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VaccineProfile", x => x.VaccineProfileId);
                    table.ForeignKey(
                        name: "FK_VaccineProfile_Appointment_AppointmentId",
                        column: x => x.AppointmentId,
                        principalTable: "Appointment",
                        principalColumn: "AppointmentId");
                    table.ForeignKey(
                        name: "FK_VaccineProfile_ChildProfile_ChildId",
                        column: x => x.ChildId,
                        principalTable: "ChildProfile",
                        principalColumn: "ChildId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VaccineProfile_Disease_DiseaseId",
                        column: x => x.DiseaseId,
                        principalTable: "Disease",
                        principalColumn: "DiseaseId");
                    table.ForeignKey(
                        name: "FK_VaccineProfile_VaccineSchedule_ChildId",
                        column: x => x.ChildId,
                        principalTable: "VaccineSchedule",
                        principalColumn: "VaccineScheduleId");
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "UserId", "Address", "CreatedBy", "CreatedDate", "DateOfBirth", "Email", "FullName", "Image", "IsActive", "ModifiedBy", "ModifiedDate", "PasswordHash", "PasswordSalt", "PhoneNumber", "Role" },
                values: new object[] { 1, "PediVax HCM", "System", new DateTime(2025, 3, 12, 6, 51, 53, 987, DateTimeKind.Utc).AddTicks(5986), new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@pedivax.com", "System Admin", "https://pedivax.com/images/user.png", 1, "System", new DateTime(2025, 3, 12, 6, 51, 53, 987, DateTimeKind.Utc).AddTicks(5988), "jrsVfha6nJxWoUmqpMw2MXkDInU4yoY5L+AWMBu6rbo=", "sX+vRlXjp0xxkO/mo9Hx7YrPtx8mf/AuA/f3d7eQkAo=", "0123456789", 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_ChildId",
                table: "Appointment",
                column: "ChildId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_PaymentId",
                table: "Appointment",
                column: "PaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_VaccineId",
                table: "Appointment",
                column: "VaccineId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_VaccinePackageId",
                table: "Appointment",
                column: "VaccinePackageId");

            migrationBuilder.CreateIndex(
                name: "IX_ChildProfile_UserId",
                table: "ChildProfile",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_VaccineId",
                table: "Payment",
                column: "VaccineId");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_VaccinePackageId",
                table: "Payment",
                column: "VaccinePackageId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentDetail_PaymentId",
                table: "PaymentDetail",
                column: "PaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentDetail_VaccinePackageId",
                table: "PaymentDetail",
                column: "VaccinePackageId");

            migrationBuilder.CreateIndex(
                name: "IX_VaccineDisease_DiseaseId",
                table: "VaccineDisease",
                column: "DiseaseId");

            migrationBuilder.CreateIndex(
                name: "IX_VaccineDisease_VaccineId",
                table: "VaccineDisease",
                column: "VaccineId");

            migrationBuilder.CreateIndex(
                name: "IX_VaccinePackageDetail_PackageId",
                table: "VaccinePackageDetail",
                column: "PackageId");

            migrationBuilder.CreateIndex(
                name: "IX_VaccinePackageDetail_VaccineId",
                table: "VaccinePackageDetail",
                column: "VaccineId");

            migrationBuilder.CreateIndex(
                name: "IX_VaccineProfile_AppointmentId",
                table: "VaccineProfile",
                column: "AppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_VaccineProfile_ChildId",
                table: "VaccineProfile",
                column: "ChildId");

            migrationBuilder.CreateIndex(
                name: "IX_VaccineProfile_DiseaseId",
                table: "VaccineProfile",
                column: "DiseaseId");

            migrationBuilder.CreateIndex(
                name: "IX_VaccineSchedule_DiseaseId",
                table: "VaccineSchedule",
                column: "DiseaseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaymentDetail");

            migrationBuilder.DropTable(
                name: "VaccineDisease");

            migrationBuilder.DropTable(
                name: "VaccinePackageDetail");

            migrationBuilder.DropTable(
                name: "VaccineProfile");

            migrationBuilder.DropTable(
                name: "Appointment");

            migrationBuilder.DropTable(
                name: "VaccineSchedule");

            migrationBuilder.DropTable(
                name: "ChildProfile");

            migrationBuilder.DropTable(
                name: "Payment");

            migrationBuilder.DropTable(
                name: "Disease");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "VaccinePackage");

            migrationBuilder.DropTable(
                name: "Vaccine");
        }
    }
}
