using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FamilyCalendarEventPlanner.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddHouseholds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Households",
                columns: table => new
                {
                    HouseholdId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Street = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Province = table.Column<int>(type: "int", nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Households", x => x.HouseholdId);
                });

            migrationBuilder.AddColumn<Guid>(
                name: "HouseholdId",
                table: "FamilyMembers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FamilyMembers_HouseholdId",
                table: "FamilyMembers",
                column: "HouseholdId");

            migrationBuilder.CreateIndex(
                name: "IX_Households_Name",
                table: "Households",
                column: "Name");

            migrationBuilder.AddForeignKey(
                name: "FK_FamilyMembers_Households_HouseholdId",
                table: "FamilyMembers",
                column: "HouseholdId",
                principalTable: "Households",
                principalColumn: "HouseholdId",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FamilyMembers_Households_HouseholdId",
                table: "FamilyMembers");

            migrationBuilder.DropIndex(
                name: "IX_FamilyMembers_HouseholdId",
                table: "FamilyMembers");

            migrationBuilder.DropColumn(
                name: "HouseholdId",
                table: "FamilyMembers");

            migrationBuilder.DropTable(
                name: "Households");
        }
    }
}
