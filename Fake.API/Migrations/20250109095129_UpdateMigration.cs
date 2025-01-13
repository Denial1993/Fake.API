using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fake.API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "TouristRoutes",
                columns: new[] { "Id", "CreateTime", "DepartureTime", "Description", "DiscountPersent", "Features", "Fees", "Notes", "OriginalPrice", "Title", "UpdateTime" },
                values: new object[] { new Guid("b0be8db8-3513-4142-ae0e-0595543dba1f"), new DateTime(2025, 1, 9, 8, 46, 53, 0, DateTimeKind.Unspecified), new DateTime(2025, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "This is a description for the sample tourist route.", null, "Sample features", "Sample fees", "Sample notes", 1000m, "Sample Tourist Route", null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TouristRoutes",
                keyColumn: "Id",
                keyValue: new Guid("b0be8db8-3513-4142-ae0e-0595543dba1f"));
        }
    }
}
