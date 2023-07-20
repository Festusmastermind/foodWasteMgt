using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace foodWasteMgt.Migrations
{
    public partial class changeTableName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FoodItems_SysUsers_SysUserId",
                table: "FoodItems");

            migrationBuilder.DropTable(
                name: "SysUsers");

            migrationBuilder.RenameColumn(
                name: "SysUserId",
                table: "FoodItems",
                newName: "ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_FoodItems_SysUserId",
                table: "FoodItems",
                newName: "IX_FoodItems_ApplicationUserId");

            migrationBuilder.CreateTable(
                name: "ApplicationUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserType = table.Column<int>(type: "int", nullable: false),
                    Occupation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUsers", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_FoodItems_ApplicationUsers_ApplicationUserId",
                table: "FoodItems",
                column: "ApplicationUserId",
                principalTable: "ApplicationUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FoodItems_ApplicationUsers_ApplicationUserId",
                table: "FoodItems");

            migrationBuilder.DropTable(
                name: "ApplicationUsers");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "FoodItems",
                newName: "SysUserId");

            migrationBuilder.RenameIndex(
                name: "IX_FoodItems_ApplicationUserId",
                table: "FoodItems",
                newName: "IX_FoodItems_SysUserId");

            migrationBuilder.CreateTable(
                name: "SysUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Occupation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserType = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysUsers", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_FoodItems_SysUsers_SysUserId",
                table: "FoodItems",
                column: "SysUserId",
                principalTable: "SysUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
