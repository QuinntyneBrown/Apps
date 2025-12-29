// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnniversaryBirthdayReminder.Infrastructure.Migrations;

/// <summary>
/// Initial database migration.
/// </summary>
public partial class InitialCreate : Migration
{
    /// <inheritdoc/>
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "ImportantDates",
            columns: table => new
            {
                ImportantDateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                PersonName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                DateType = table.Column<int>(type: "int", nullable: false),
                DateValue = table.Column<DateTime>(type: "datetime2", nullable: false),
                RecurrencePattern = table.Column<int>(type: "int", nullable: false),
                Relationship = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                Notes = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ImportantDates", x => x.ImportantDateId);
            });

        migrationBuilder.CreateTable(
            name: "Celebrations",
            columns: table => new
            {
                CelebrationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                ImportantDateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                CelebrationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                Notes = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                Photos = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Attendees = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Rating = table.Column<int>(type: "int", nullable: true),
                Status = table.Column<int>(type: "int", nullable: false),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Celebrations", x => x.CelebrationId);
                table.ForeignKey(
                    name: "FK_Celebrations_ImportantDates_ImportantDateId",
                    column: x => x.ImportantDateId,
                    principalTable: "ImportantDates",
                    principalColumn: "ImportantDateId",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Gifts",
            columns: table => new
            {
                GiftId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                ImportantDateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                EstimatedPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                ActualPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                PurchaseUrl = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                Status = table.Column<int>(type: "int", nullable: false),
                PurchasedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                DeliveredAt = table.Column<DateTime>(type: "datetime2", nullable: true),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Gifts", x => x.GiftId);
                table.ForeignKey(
                    name: "FK_Gifts_ImportantDates_ImportantDateId",
                    column: x => x.ImportantDateId,
                    principalTable: "ImportantDates",
                    principalColumn: "ImportantDateId",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Reminders",
            columns: table => new
            {
                ReminderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                ImportantDateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                ScheduledTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                AdvanceNoticeDays = table.Column<int>(type: "int", nullable: false),
                DeliveryChannel = table.Column<int>(type: "int", nullable: false),
                Status = table.Column<int>(type: "int", nullable: false),
                SentAt = table.Column<DateTime>(type: "datetime2", nullable: true),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Reminders", x => x.ReminderId);
                table.ForeignKey(
                    name: "FK_Reminders_ImportantDates_ImportantDateId",
                    column: x => x.ImportantDateId,
                    principalTable: "ImportantDates",
                    principalColumn: "ImportantDateId",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Celebrations_CelebrationDate",
            table: "Celebrations",
            column: "CelebrationDate");

        migrationBuilder.CreateIndex(
            name: "IX_Celebrations_ImportantDateId",
            table: "Celebrations",
            column: "ImportantDateId");

        migrationBuilder.CreateIndex(
            name: "IX_Celebrations_Status",
            table: "Celebrations",
            column: "Status");

        migrationBuilder.CreateIndex(
            name: "IX_Gifts_ImportantDateId",
            table: "Gifts",
            column: "ImportantDateId");

        migrationBuilder.CreateIndex(
            name: "IX_Gifts_Status",
            table: "Gifts",
            column: "Status");

        migrationBuilder.CreateIndex(
            name: "IX_ImportantDates_DateValue",
            table: "ImportantDates",
            column: "DateValue");

        migrationBuilder.CreateIndex(
            name: "IX_ImportantDates_UserId",
            table: "ImportantDates",
            column: "UserId");

        migrationBuilder.CreateIndex(
            name: "IX_ImportantDates_UserId_IsActive",
            table: "ImportantDates",
            columns: new[] { "UserId", "IsActive" });

        migrationBuilder.CreateIndex(
            name: "IX_Reminders_ImportantDateId",
            table: "Reminders",
            column: "ImportantDateId");

        migrationBuilder.CreateIndex(
            name: "IX_Reminders_ScheduledTime",
            table: "Reminders",
            column: "ScheduledTime");

        migrationBuilder.CreateIndex(
            name: "IX_Reminders_Status",
            table: "Reminders",
            column: "Status");
    }

    /// <inheritdoc/>
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(name: "Celebrations");
        migrationBuilder.DropTable(name: "Gifts");
        migrationBuilder.DropTable(name: "Reminders");
        migrationBuilder.DropTable(name: "ImportantDates");
    }
}
