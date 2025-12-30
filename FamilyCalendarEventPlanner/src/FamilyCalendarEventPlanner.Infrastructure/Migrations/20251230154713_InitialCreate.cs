using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FamilyCalendarEventPlanner.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AvailabilityBlocks",
                columns: table => new
                {
                    BlockId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MemberId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BlockType = table.Column<int>(type: "int", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AvailabilityBlocks", x => x.BlockId);
                });

            migrationBuilder.CreateTable(
                name: "CalendarEvents",
                columns: table => new
                {
                    EventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FamilyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    EventType = table.Column<int>(type: "int", nullable: false),
                    RecurrencePattern_Frequency = table.Column<int>(type: "int", nullable: false),
                    RecurrencePattern_Interval = table.Column<int>(type: "int", nullable: false),
                    RecurrencePattern_EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecurrencePattern_DaysOfWeek = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalendarEvents", x => x.EventId);
                });

            migrationBuilder.CreateTable(
                name: "EventAttendees",
                columns: table => new
                {
                    AttendeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FamilyMemberId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RSVPStatus = table.Column<int>(type: "int", nullable: false),
                    ResponseTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventAttendees", x => x.AttendeeId);
                });

            migrationBuilder.CreateTable(
                name: "EventReminders",
                columns: table => new
                {
                    ReminderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RecipientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReminderTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeliveryChannel = table.Column<int>(type: "int", nullable: false),
                    Sent = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventReminders", x => x.ReminderId);
                });

            migrationBuilder.CreateTable(
                name: "FamilyMembers",
                columns: table => new
                {
                    MemberId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FamilyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Color = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    IsImmediate = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    RelationType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FamilyMembers", x => x.MemberId);
                });

            migrationBuilder.CreateTable(
                name: "ScheduleConflicts",
                columns: table => new
                {
                    ConflictId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConflictingEventIds = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AffectedMemberIds = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConflictSeverity = table.Column<int>(type: "int", nullable: false),
                    IsResolved = table.Column<bool>(type: "bit", nullable: false),
                    ResolvedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleConflicts", x => x.ConflictId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AvailabilityBlocks_EndTime",
                table: "AvailabilityBlocks",
                column: "EndTime");

            migrationBuilder.CreateIndex(
                name: "IX_AvailabilityBlocks_MemberId",
                table: "AvailabilityBlocks",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_AvailabilityBlocks_StartTime",
                table: "AvailabilityBlocks",
                column: "StartTime");

            migrationBuilder.CreateIndex(
                name: "IX_CalendarEvents_CreatorId",
                table: "CalendarEvents",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_CalendarEvents_FamilyId",
                table: "CalendarEvents",
                column: "FamilyId");

            migrationBuilder.CreateIndex(
                name: "IX_CalendarEvents_StartTime",
                table: "CalendarEvents",
                column: "StartTime");

            migrationBuilder.CreateIndex(
                name: "IX_CalendarEvents_Status",
                table: "CalendarEvents",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_EventAttendees_EventId",
                table: "EventAttendees",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_EventAttendees_EventId_FamilyMemberId",
                table: "EventAttendees",
                columns: new[] { "EventId", "FamilyMemberId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EventAttendees_FamilyMemberId",
                table: "EventAttendees",
                column: "FamilyMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_EventReminders_EventId",
                table: "EventReminders",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_EventReminders_RecipientId",
                table: "EventReminders",
                column: "RecipientId");

            migrationBuilder.CreateIndex(
                name: "IX_EventReminders_ReminderTime",
                table: "EventReminders",
                column: "ReminderTime");

            migrationBuilder.CreateIndex(
                name: "IX_EventReminders_Sent",
                table: "EventReminders",
                column: "Sent");

            migrationBuilder.CreateIndex(
                name: "IX_FamilyMembers_Email",
                table: "FamilyMembers",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_FamilyMembers_FamilyId",
                table: "FamilyMembers",
                column: "FamilyId");

            migrationBuilder.CreateIndex(
                name: "IX_FamilyMembers_IsImmediate",
                table: "FamilyMembers",
                column: "IsImmediate");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleConflicts_IsResolved",
                table: "ScheduleConflicts",
                column: "IsResolved");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AvailabilityBlocks");

            migrationBuilder.DropTable(
                name: "CalendarEvents");

            migrationBuilder.DropTable(
                name: "EventAttendees");

            migrationBuilder.DropTable(
                name: "EventReminders");

            migrationBuilder.DropTable(
                name: "FamilyMembers");

            migrationBuilder.DropTable(
                name: "ScheduleConflicts");
        }
    }
}
