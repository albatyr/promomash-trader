using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Promomash.Trader.UserAccess.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "users");

            migrationBuilder.CreateTable(
                name: "Countries",
                schema: "users",
                columns: table => new
                {
                    Code = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Login = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    IsAgreedToWorkForFood = table.Column<bool>(type: "boolean", nullable: false),
                    ProvinceId = table.Column<string>(type: "character varying(6)", maxLength: 6, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Provinces",
                schema: "users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CountryCode = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provinces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Provinces_Countries_CountryCode",
                        column: x => x.CountryCode,
                        principalSchema: "users",
                        principalTable: "Countries",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "users",
                table: "Countries",
                columns: new[] { "Code", "Name" },
                values: new object[,]
                {
                    { "CAN", "Canada" },
                    { "USA", "United States" }
                });

            migrationBuilder.InsertData(
                schema: "users",
                table: "Users",
                columns: new[] { "Id", "Email", "IsAgreedToWorkForFood", "Login", "Password", "ProvinceId" },
                values: new object[] { new Guid("b1e9f865-c0b1-4c3d-9fbc-080c5c67af9a"), "admin@example.com", true, "admin@example.com", "adminpass", "USA:CA" });

            migrationBuilder.InsertData(
                schema: "users",
                table: "Provinces",
                columns: new[] { "Id", "CountryCode", "Name" },
                values: new object[,]
                {
                    { "CAN:AB", "CAN", "Alberta" },
                    { "CAN:BC", "CAN", "British Columbia" },
                    { "CAN:ON", "CAN", "Ontario" },
                    { "CAN:QC", "CAN", "Quebec" },
                    { "USA:CA", "USA", "California" },
                    { "USA:FL", "USA", "Florida" },
                    { "USA:NY", "USA", "New York" },
                    { "USA:TX", "USA", "Texas" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Provinces_CountryCode",
                schema: "users",
                table: "Provinces",
                column: "CountryCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Provinces",
                schema: "users");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "users");

            migrationBuilder.DropTable(
                name: "Countries",
                schema: "users");
        }
    }
}
