using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TEXT_PASSAGES",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    DifficultyLevel = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    NativeSpeakerAudioUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TEXT_PASSAGES", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "USERS",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Username = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USERS", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ASSESSMENT_RESULTS",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    TextPassageId = table.Column<Guid>(type: "uuid", nullable: false),
                    AccuracyScore = table.Column<float>(type: "real", nullable: false),
                    FluencyScore = table.Column<float>(type: "real", nullable: false),
                    ProsodyScore = table.Column<float>(type: "real", nullable: false),
                    PronunciationScore = table.Column<float>(type: "real", nullable: false),
                    UserAudioUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    FullResultJson = table.Column<string>(type: "jsonb", nullable: false),
                    AssessedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ASSESSMENT_RESULTS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ASSESSMENT_RESULTS_TEXT_PASSAGES_TextPassageId",
                        column: x => x.TextPassageId,
                        principalTable: "TEXT_PASSAGES",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ASSESSMENT_RESULTS_USERS_UserId",
                        column: x => x.UserId,
                        principalTable: "USERS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WORD_SCORES",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AssessmentResultId = table.Column<Guid>(type: "uuid", nullable: false),
                    Word = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    AccuracyScore = table.Column<float>(type: "real", nullable: false),
                    ErrorType = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Offset = table.Column<int>(type: "integer", nullable: false),
                    Duration = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WORD_SCORES", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WORD_SCORES_ASSESSMENT_RESULTS_AssessmentResultId",
                        column: x => x.AssessmentResultId,
                        principalTable: "ASSESSMENT_RESULTS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PHONEME_SCORES",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    WordScoreId = table.Column<Guid>(type: "uuid", nullable: false),
                    Phoneme = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    AccuracyScore = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PHONEME_SCORES", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PHONEME_SCORES_WORD_SCORES_WordScoreId",
                        column: x => x.WordScoreId,
                        principalTable: "WORD_SCORES",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ASSESSMENT_RESULTS_TextPassageId",
                table: "ASSESSMENT_RESULTS",
                column: "TextPassageId");

            migrationBuilder.CreateIndex(
                name: "IX_ASSESSMENT_RESULTS_UserId",
                table: "ASSESSMENT_RESULTS",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PHONEME_SCORES_WordScoreId",
                table: "PHONEME_SCORES",
                column: "WordScoreId");

            migrationBuilder.CreateIndex(
                name: "IX_WORD_SCORES_AssessmentResultId",
                table: "WORD_SCORES",
                column: "AssessmentResultId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PHONEME_SCORES");

            migrationBuilder.DropTable(
                name: "WORD_SCORES");

            migrationBuilder.DropTable(
                name: "ASSESSMENT_RESULTS");

            migrationBuilder.DropTable(
                name: "TEXT_PASSAGES");

            migrationBuilder.DropTable(
                name: "USERS");
        }
    }
}
