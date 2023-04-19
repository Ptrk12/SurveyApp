﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SurveyEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SurveyQuestionAnswerEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Answer = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyQuestionAnswerEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SurveyQuestions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Question = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyQuestions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SurveyEntitySurveyQuestionEntity",
                columns: table => new
                {
                    SurveyQuestionsId = table.Column<int>(type: "int", nullable: false),
                    SurveysId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyEntitySurveyQuestionEntity", x => new { x.SurveyQuestionsId, x.SurveysId });
                    table.ForeignKey(
                        name: "FK_SurveyEntitySurveyQuestionEntity_SurveyEntity_SurveysId",
                        column: x => x.SurveysId,
                        principalTable: "SurveyEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SurveyEntitySurveyQuestionEntity_SurveyQuestions_SurveyQuestionsId",
                        column: x => x.SurveyQuestionsId,
                        principalTable: "SurveyQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SurveyQuestionAnswerEntitySurveyQuestionEntity",
                columns: table => new
                {
                    SurveyQuestionAnswersId = table.Column<int>(type: "int", nullable: false),
                    SurveyQuestionsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyQuestionAnswerEntitySurveyQuestionEntity", x => new { x.SurveyQuestionAnswersId, x.SurveyQuestionsId });
                    table.ForeignKey(
                        name: "FK_SurveyQuestionAnswerEntitySurveyQuestionEntity_SurveyQuestionAnswerEntity_SurveyQuestionAnswersId",
                        column: x => x.SurveyQuestionAnswersId,
                        principalTable: "SurveyQuestionAnswerEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SurveyQuestionAnswerEntitySurveyQuestionEntity_SurveyQuestions_SurveyQuestionsId",
                        column: x => x.SurveyQuestionsId,
                        principalTable: "SurveyQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserAnswers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SurveyId = table.Column<int>(type: "int", nullable: false),
                    SurveyQuestionId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Answer = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserAnswers_SurveyQuestions_SurveyQuestionId",
                        column: x => x.SurveyQuestionId,
                        principalTable: "SurveyQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "SurveyEntity",
                columns: new[] { "Id", "Status", "Title" },
                values: new object[] { 1, "public", "My first survey" });

            migrationBuilder.InsertData(
                table: "SurveyQuestionAnswerEntity",
                columns: new[] { "Id", "Answer" },
                values: new object[,]
                {
                    { 1, "4" },
                    { 2, "7" },
                    { 3, "9" },
                    { 4, "Park,Forest" },
                    { 5, "Cycling,Running" },
                    { 6, "Pizza,Burger" },
                    { 7, "Patryk" },
                    { 8, "300km" },
                    { 9, "12" }
                });

            migrationBuilder.InsertData(
                table: "SurveyQuestions",
                columns: new[] { "Id", "Question", "Type" },
                values: new object[,]
                {
                    { 1, "How many people live in this house ?", "numeric" },
                    { 2, "How do you rate healthcare in our country ?", "numeric" },
                    { 3, "How do you rate today's weather ?", "numeric" },
                    { 4, "Favourite places to spend free time ?", "multiple" },
                    { 5, "Favourite sports ?", "multiple" },
                    { 6, "Favourite food ?", "multiple" },
                    { 7, "What's your name ?", "open" },
                    { 8, "4 x 3 ?", "open" },
                    { 9, "How many people live in this house ?", "open" }
                });

            migrationBuilder.InsertData(
                table: "SurveyEntitySurveyQuestionEntity",
                columns: new[] { "SurveyQuestionsId", "SurveysId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 3, 1 },
                    { 4, 1 },
                    { 5, 1 },
                    { 6, 1 },
                    { 7, 1 },
                    { 8, 1 },
                    { 9, 1 }
                });

            migrationBuilder.InsertData(
                table: "SurveyQuestionAnswerEntitySurveyQuestionEntity",
                columns: new[] { "SurveyQuestionAnswersId", "SurveyQuestionsId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 3, 3 },
                    { 4, 4 },
                    { 5, 5 },
                    { 6, 6 },
                    { 7, 7 },
                    { 8, 8 },
                    { 9, 9 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_SurveyEntitySurveyQuestionEntity_SurveysId",
                table: "SurveyEntitySurveyQuestionEntity",
                column: "SurveysId");

            migrationBuilder.CreateIndex(
                name: "IX_SurveyQuestionAnswerEntitySurveyQuestionEntity_SurveyQuestionsId",
                table: "SurveyQuestionAnswerEntitySurveyQuestionEntity",
                column: "SurveyQuestionsId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAnswers_SurveyQuestionId",
                table: "UserAnswers",
                column: "SurveyQuestionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SurveyEntitySurveyQuestionEntity");

            migrationBuilder.DropTable(
                name: "SurveyQuestionAnswerEntitySurveyQuestionEntity");

            migrationBuilder.DropTable(
                name: "UserAnswers");

            migrationBuilder.DropTable(
                name: "SurveyEntity");

            migrationBuilder.DropTable(
                name: "SurveyQuestionAnswerEntity");

            migrationBuilder.DropTable(
                name: "SurveyQuestions");
        }
    }
}
