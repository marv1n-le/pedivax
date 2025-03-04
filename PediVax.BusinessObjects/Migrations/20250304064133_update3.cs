using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PediVax.BusinessObjects.Migrations
{
    /// <inheritdoc />
    public partial class update3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "AppointmentId",
                table: "VaccineProfile",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "AgeRange",
                table: "VaccineDisease",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "VaccinationRecord",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "User",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "VaccinePackageId",
                table: "Payment",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "VaccineId",
                table: "Payment",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "PaymentType",
                table: "Payment",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Disease",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "VaccinePackageId",
                table: "Appointment",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "VaccineId",
                table: "Appointment",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "PaymentId",
                table: "Appointment",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "CreatedDate", "Image", "ModifiedDate", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2025, 3, 4, 6, 41, 32, 784, DateTimeKind.Utc).AddTicks(8287), "https://pedivax.com/images/user.png", new DateTime(2025, 3, 4, 6, 41, 32, 784, DateTimeKind.Utc).AddTicks(8293), "6zl48GrmvUL6eue9YHlOOjXJ6q1wAk3aJS070bfejxo=", "mOY2GZuwnnTEusYrbxLIffKqAAembCTHCRnF2hRXd8o=" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AgeRange",
                table: "VaccineDisease");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "User");

            migrationBuilder.AlterColumn<int>(
                name: "AppointmentId",
                table: "VaccineProfile",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "VaccinationRecord",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "VaccinePackageId",
                table: "Payment",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "VaccineId",
                table: "Payment",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PaymentType",
                table: "Payment",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Disease",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "VaccinePackageId",
                table: "Appointment",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "VaccineId",
                table: "Appointment",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PaymentId",
                table: "Appointment",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ModifiedDate", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2025, 3, 4, 6, 39, 16, 934, DateTimeKind.Utc).AddTicks(4928), new DateTime(2025, 3, 4, 6, 39, 16, 934, DateTimeKind.Utc).AddTicks(4931), "RTkdkSPgaCNumhU010Jx/DwUIKHatxMAAmbiqKnceJk=", "Pp8VcNDqdeGcz2mgnWAfQBre/jiKnvHjJCAq8C4rvgA=" });
        }
    }
}
