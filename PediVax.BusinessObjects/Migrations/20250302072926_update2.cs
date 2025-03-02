using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PediVax.BusinessObjects.Migrations
{
    /// <inheritdoc />
    public partial class update2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "ChildProfile");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "ChildProfile");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ModifiedDate", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2025, 3, 2, 7, 29, 25, 855, DateTimeKind.Utc).AddTicks(5962), new DateTime(2025, 3, 2, 7, 29, 25, 855, DateTimeKind.Utc).AddTicks(5966), "l0IY3N+40p/tGBRX6ZEfa8v42RlVNP+l+Xzcztg5Y7Y=", "lDSH2/Rss9Lf1AReUKiP4fBnzXacN51FKeDam+VXjRE=" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "ChildProfile",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "ChildProfile",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ModifiedDate", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2025, 3, 2, 5, 52, 59, 73, DateTimeKind.Utc).AddTicks(3710), new DateTime(2025, 3, 2, 5, 52, 59, 73, DateTimeKind.Utc).AddTicks(3715), "w9HyqTUO7+U8Z7VO72fbn/yenQMlkwb1uAg15bEnLMw=", "PTV8VwTgGbJpg3OxlR/atGk/jwCLUwn9zIruakqn45c=" });
        }
    }
}
