using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PediVax.BusinessObjects.Migrations
{
    /// <inheritdoc />
    public partial class update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DoseNumber",
                table: "VaccineDisease",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Vaccine",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "ChildProfile",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "UserId", "Address", "CreatedBy", "CreatedDate", "DateOfBirth", "Email", "FullName", "IsActive", "ModifiedBy", "ModifiedDate", "PasswordHash", "PasswordSalt", "PhoneNumber", "Role" },
                values: new object[] { 1, "PediVax HCM", "System", new DateTime(2025, 3, 2, 5, 52, 59, 73, DateTimeKind.Utc).AddTicks(3710), new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@pedivax.com", "System Admin", 1, "System", new DateTime(2025, 3, 2, 5, 52, 59, 73, DateTimeKind.Utc).AddTicks(3715), "w9HyqTUO7+U8Z7VO72fbn/yenQMlkwb1uAg15bEnLMw=", "PTV8VwTgGbJpg3OxlR/atGk/jwCLUwn9zIruakqn45c=", "0123456789", 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "UserId",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "DoseNumber",
                table: "VaccineDisease");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Vaccine");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "ChildProfile");
        }
    }
}
