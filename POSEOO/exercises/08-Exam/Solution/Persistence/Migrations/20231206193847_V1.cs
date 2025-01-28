using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class V1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Exam",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exam", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Examinee",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Examinee", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExamQuestion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    Points = table.Column<int>(type: "int", nullable: false),
                    ExamId = table.Column<int>(type: "int", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamQuestion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExamQuestion_Exam_ExamId",
                        column: x => x.ExamId,
                        principalTable: "Exam",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExamineeExamQuestion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExamId = table.Column<int>(type: "int", nullable: false),
                    ExamineeId = table.Column<int>(type: "int", nullable: false),
                    ExamQuestionId = table.Column<int>(type: "int", nullable: false),
                    ScorePercentage = table.Column<double>(type: "float", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamineeExamQuestion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExamineeExamQuestion_ExamQuestion_ExamQuestionId",
                        column: x => x.ExamQuestionId,
                        principalTable: "ExamQuestion",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ExamineeExamQuestion_Exam_ExamId",
                        column: x => x.ExamId,
                        principalTable: "Exam",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExamineeExamQuestion_Examinee_ExamineeId",
                        column: x => x.ExamineeId,
                        principalTable: "Examinee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Exam_Name_Date",
                table: "Exam",
                columns: new[] { "Name", "Date" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Examinee_Name",
                table: "Examinee",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExamineeExamQuestion_ExamId_ExamineeId_ExamQuestionId",
                table: "ExamineeExamQuestion",
                columns: new[] { "ExamId", "ExamineeId", "ExamQuestionId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExamineeExamQuestion_ExamineeId",
                table: "ExamineeExamQuestion",
                column: "ExamineeId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamineeExamQuestion_ExamQuestionId",
                table: "ExamineeExamQuestion",
                column: "ExamQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamQuestion_ExamId_Number",
                table: "ExamQuestion",
                columns: new[] { "ExamId", "Number" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExamineeExamQuestion");

            migrationBuilder.DropTable(
                name: "ExamQuestion");

            migrationBuilder.DropTable(
                name: "Examinee");

            migrationBuilder.DropTable(
                name: "Exam");
        }
    }
}
