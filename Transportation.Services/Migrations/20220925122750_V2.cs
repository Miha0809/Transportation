using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Transportation.Services.Migrations
{
    public partial class V2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Cargo_CargoId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Cargo_Customer_CargoId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_HistoryCargo_HistoryCargoId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Users_CargoCompanyId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_Customer_CargoId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CargoCompany_Description",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CargoCompany_Name",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CargoCompany_Password",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Customer_CargoId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Customer_Name",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Customer_Password",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "Drivers");

            migrationBuilder.RenameIndex(
                name: "IX_Users_RoleId",
                table: "Drivers",
                newName: "IX_Drivers_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_HistoryCargoId",
                table: "Drivers",
                newName: "IX_Drivers_HistoryCargoId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_CargoId",
                table: "Drivers",
                newName: "IX_Drivers_CargoId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_CargoCompanyId",
                table: "Drivers",
                newName: "IX_Drivers_CargoCompanyId");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Drivers",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Drivers",
                type: "nvarchar(32)",
                maxLength: 32,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(32)",
                oldMaxLength: 32,
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsFree",
                table: "Drivers",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "HistoryCargoId",
                table: "Drivers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CargoId",
                table: "Drivers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Drivers",
                table: "Drivers",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "CargoCompanies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", maxLength: 2147483647, nullable: false),
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
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", maxLength: 2147483647, nullable: false),
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

            migrationBuilder.AddForeignKey(
                name: "FK_Drivers_Cargo_CargoId",
                table: "Drivers",
                column: "CargoId",
                principalTable: "Cargo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Drivers_CargoCompanies_CargoCompanyId",
                table: "Drivers",
                column: "CargoCompanyId",
                principalTable: "CargoCompanies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Drivers_HistoryCargo_HistoryCargoId",
                table: "Drivers",
                column: "HistoryCargoId",
                principalTable: "HistoryCargo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Drivers_Roles_RoleId",
                table: "Drivers",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Drivers_Cargo_CargoId",
                table: "Drivers");

            migrationBuilder.DropForeignKey(
                name: "FK_Drivers_CargoCompanies_CargoCompanyId",
                table: "Drivers");

            migrationBuilder.DropForeignKey(
                name: "FK_Drivers_HistoryCargo_HistoryCargoId",
                table: "Drivers");

            migrationBuilder.DropForeignKey(
                name: "FK_Drivers_Roles_RoleId",
                table: "Drivers");

            migrationBuilder.DropTable(
                name: "CargoCompanies");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Drivers",
                table: "Drivers");

            migrationBuilder.RenameTable(
                name: "Drivers",
                newName: "Users");

            migrationBuilder.RenameIndex(
                name: "IX_Drivers_RoleId",
                table: "Users",
                newName: "IX_Users_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_Drivers_HistoryCargoId",
                table: "Users",
                newName: "IX_Users_HistoryCargoId");

            migrationBuilder.RenameIndex(
                name: "IX_Drivers_CargoId",
                table: "Users",
                newName: "IX_Users_CargoId");

            migrationBuilder.RenameIndex(
                name: "IX_Drivers_CargoCompanyId",
                table: "Users",
                newName: "IX_Users_CargoCompanyId");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Users",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Users",
                type: "nvarchar(32)",
                maxLength: 32,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(32)",
                oldMaxLength: 32);

            migrationBuilder.AlterColumn<bool>(
                name: "IsFree",
                table: "Users",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<int>(
                name: "HistoryCargoId",
                table: "Users",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CargoId",
                table: "Users",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "CargoCompany_Description",
                table: "Users",
                type: "nvarchar(max)",
                maxLength: 2147483647,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CargoCompany_Name",
                table: "Users",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CargoCompany_Password",
                table: "Users",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Customer_CargoId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Customer_Name",
                table: "Users",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Customer_Password",
                table: "Users",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Users",
                type: "nvarchar(max)",
                maxLength: 2147483647,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Customer_CargoId",
                table: "Users",
                column: "Customer_CargoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Cargo_CargoId",
                table: "Users",
                column: "CargoId",
                principalTable: "Cargo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Cargo_Customer_CargoId",
                table: "Users",
                column: "Customer_CargoId",
                principalTable: "Cargo",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_HistoryCargo_HistoryCargoId",
                table: "Users",
                column: "HistoryCargoId",
                principalTable: "HistoryCargo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Users_CargoCompanyId",
                table: "Users",
                column: "CargoCompanyId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
