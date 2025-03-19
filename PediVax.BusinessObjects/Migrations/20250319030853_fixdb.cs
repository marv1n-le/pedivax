using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PediVax.BusinessObjects.Migrations
{
    /// <inheritdoc />
    public partial class fixdb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Vaccine",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ModifiedDate", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2025, 3, 19, 3, 8, 52, 660, DateTimeKind.Utc).AddTicks(6476), new DateTime(2025, 3, 19, 3, 8, 52, 660, DateTimeKind.Utc).AddTicks(6481), "BYCe27mYnGvCFrjfqSnDtvKdoGs7ojK5bNLMRL+Xdgw=", "Tr+ygRhDOm9l6xGzkVJTuPHX3mR5XNIIICB3kPUHevk=" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Vaccine");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ModifiedDate", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2025, 3, 16, 12, 14, 2, 877, DateTimeKind.Utc).AddTicks(7881), new DateTime(2025, 3, 16, 12, 14, 2, 877, DateTimeKind.Utc).AddTicks(7886), "hBaK1TF0tQfjebs4fJwEcM2R/vrgV6NJk1tF+l9HUOs=", "y+AWufvnUIH6BTKmhJQ7SXki2lihYDzJ1yctAATaAco=" });
        }
    }
}
