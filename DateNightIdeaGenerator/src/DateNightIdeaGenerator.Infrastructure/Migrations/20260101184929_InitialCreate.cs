using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DateNightIdeaGenerator.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DateIdeas",
                columns: table => new
                {
                    DateIdeaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    Category = table.Column<int>(type: "int", nullable: false),
                    BudgetRange = table.Column<int>(type: "int", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DurationMinutes = table.Column<int>(type: "int", nullable: true),
                    Season = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsFavorite = table.Column<bool>(type: "bit", nullable: false),
                    HasBeenTried = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DateIdeas", x => x.DateIdeaId);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Salt = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Experiences",
                columns: table => new
                {
                    ExperienceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateIdeaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExperienceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    ActualCost = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: true),
                    Photos = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    WasSuccessful = table.Column<bool>(type: "bit", nullable: false),
                    WouldRepeat = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Experiences", x => x.ExperienceId);
                    table.ForeignKey(
                        name: "FK_Experiences_DateIdeas_DateIdeaId",
                        column: x => x.DateIdeaId,
                        principalTable: "DateIdeas",
                        principalColumn: "DateIdeaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    RatingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateIdeaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ExperienceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false),
                    Review = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateIdeaId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ExperienceId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => x.RatingId);
                    table.ForeignKey(
                        name: "FK_Ratings_DateIdeas_DateIdeaId",
                        column: x => x.DateIdeaId,
                        principalTable: "DateIdeas",
                        principalColumn: "DateIdeaId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ratings_DateIdeas_DateIdeaId1",
                        column: x => x.DateIdeaId1,
                        principalTable: "DateIdeas",
                        principalColumn: "DateIdeaId");
                    table.ForeignKey(
                        name: "FK_Ratings_Experiences_ExperienceId",
                        column: x => x.ExperienceId,
                        principalTable: "Experiences",
                        principalColumn: "ExperienceId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ratings_Experiences_ExperienceId1",
                        column: x => x.ExperienceId1,
                        principalTable: "Experiences",
                        principalColumn: "ExperienceId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DateIdeas_BudgetRange",
                table: "DateIdeas",
                column: "BudgetRange");

            migrationBuilder.CreateIndex(
                name: "IX_DateIdeas_Category",
                table: "DateIdeas",
                column: "Category");

            migrationBuilder.CreateIndex(
                name: "IX_DateIdeas_HasBeenTried",
                table: "DateIdeas",
                column: "HasBeenTried");

            migrationBuilder.CreateIndex(
                name: "IX_DateIdeas_IsFavorite",
                table: "DateIdeas",
                column: "IsFavorite");

            migrationBuilder.CreateIndex(
                name: "IX_DateIdeas_UserId",
                table: "DateIdeas",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Experiences_DateIdeaId",
                table: "Experiences",
                column: "DateIdeaId");

            migrationBuilder.CreateIndex(
                name: "IX_Experiences_ExperienceDate",
                table: "Experiences",
                column: "ExperienceDate");

            migrationBuilder.CreateIndex(
                name: "IX_Experiences_UserId",
                table: "Experiences",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Experiences_WasSuccessful",
                table: "Experiences",
                column: "WasSuccessful");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_DateIdeaId",
                table: "Ratings",
                column: "DateIdeaId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_DateIdeaId1",
                table: "Ratings",
                column: "DateIdeaId1");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_ExperienceId",
                table: "Ratings",
                column: "ExperienceId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_ExperienceId1",
                table: "Ratings",
                column: "ExperienceId1");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_Score",
                table: "Ratings",
                column: "Score");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_UserId",
                table: "Ratings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_Name",
                table: "Roles",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_UserId",
                table: "UserRoles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserName",
                table: "Users",
                column: "UserName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Experiences");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "DateIdeas");
        }
    }
}
