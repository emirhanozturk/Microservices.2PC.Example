using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Coordinator.Migrations
{
    /// <inheritdoc />
    public partial class mig2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Nodes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("9059ac17-bd52-4a02-bc33-e420cac33a0f"), "Order.API" },
                    { new Guid("dea17b95-2125-4a3f-900e-b4403869a513"), "Stock.API" },
                    { new Guid("fdbaf0e6-030f-4980-92fd-7baac05774c4"), "Payment.API" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Nodes",
                keyColumn: "Id",
                keyValue: new Guid("9059ac17-bd52-4a02-bc33-e420cac33a0f"));

            migrationBuilder.DeleteData(
                table: "Nodes",
                keyColumn: "Id",
                keyValue: new Guid("dea17b95-2125-4a3f-900e-b4403869a513"));

            migrationBuilder.DeleteData(
                table: "Nodes",
                keyColumn: "Id",
                keyValue: new Guid("fdbaf0e6-030f-4980-92fd-7baac05774c4"));
        }
    }
}
