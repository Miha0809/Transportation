using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Transportation.Services.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    From = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    To = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HistoryCargo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryCargo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SizeCargo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Width = table.Column<long>(type: "bigint", nullable: false),
                    Height = table.Column<long>(type: "bigint", nullable: false),
                    Length = table.Column<long>(type: "bigint", nullable: false),
                    Weight = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SizeCargo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CargoCompanies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", maxLength: 2147483647, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CargoCompanies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CargoCompanies_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Cargo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    IsFree = table.Column<bool>(type: "bit", nullable: false),
                    AddressId = table.Column<int>(type: "int", nullable: true),
                    SizeCargoId = table.Column<int>(type: "int", nullable: true),
                    HistoryCargoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cargo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cargo_Address_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Address",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Cargo_HistoryCargo_HistoryCargoId",
                        column: x => x.HistoryCargoId,
                        principalTable: "HistoryCargo",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Cargo_SizeCargo_SizeCargoId",
                        column: x => x.SizeCargoId,
                        principalTable: "SizeCargo",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", maxLength: 2147483647, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    CargoId = table.Column<int>(type: "int", nullable: true),
                    RoleId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customers_Cargo_CargoId",
                        column: x => x.CargoId,
                        principalTable: "Cargo",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Customers_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Drivers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    IsFree = table.Column<bool>(type: "bit", nullable: false),
                    CargoId = table.Column<int>(type: "int", nullable: false),
                    HistoryCargoId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: true),
                    CargoCompanyId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drivers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Drivers_Cargo_CargoId",
                        column: x => x.CargoId,
                        principalTable: "Cargo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Drivers_CargoCompanies_CargoCompanyId",
                        column: x => x.CargoCompanyId,
                        principalTable: "CargoCompanies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Drivers_HistoryCargo_HistoryCargoId",
                        column: x => x.HistoryCargoId,
                        principalTable: "HistoryCargo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Drivers_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cargo_AddressId",
                table: "Cargo",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Cargo_HistoryCargoId",
                table: "Cargo",
                column: "HistoryCargoId");

            migrationBuilder.CreateIndex(
                name: "IX_Cargo_SizeCargoId",
                table: "Cargo",
                column: "SizeCargoId");

            migrationBuilder.CreateIndex(
                name: "IX_CargoCompanies_RoleId",
                table: "CargoCompanies",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CargoId",
                table: "Customers",
                column: "CargoId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_RoleId",
                table: "Customers",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_CargoCompanyId",
                table: "Drivers",
                column: "CargoCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_CargoId",
                table: "Drivers",
                column: "CargoId");

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_HistoryCargoId",
                table: "Drivers",
                column: "HistoryCargoId");

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_RoleId",
                table: "Drivers",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Drivers");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "Cargo");

            migrationBuilder.DropTable(
                name: "CargoCompanies");

            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "HistoryCargo");

            migrationBuilder.DropTable(
                name: "SizeCargo");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
