using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PediVax.BusinessObjects.Migrations
{
    /// <inheritdoc />
    public partial class update1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ModifiedDate", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2025, 3, 8, 8, 33, 35, 959, DateTimeKind.Utc).AddTicks(6429), new DateTime(2025, 3, 8, 8, 33, 35, 959, DateTimeKind.Utc).AddTicks(6433), "r9s/XzgupEVO4k4kJ3atntiOhnfAfDuM5zHaDI5NdDs=", "nrVoLK+V3/h5X8vm6+2NWYSkQF4cmFNSM4TDFWJpGf8=" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ModifiedDate", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2025, 3, 7, 19, 53, 7, 92, DateTimeKind.Utc).AddTicks(6923), new DateTime(2025, 3, 7, 19, 53, 7, 92, DateTimeKind.Utc).AddTicks(6927), "GTEbVtS6u/IAfqFIjJfr+e2JuiPKjiufWxsCzln/nFA=", "05cuCM9rPFKBkgQRGLWWj+bFEfNEPi7fyJ4ZEWDhoyE=" });
        }
    }
}
