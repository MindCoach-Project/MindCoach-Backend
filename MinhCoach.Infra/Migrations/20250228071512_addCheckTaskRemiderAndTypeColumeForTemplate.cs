using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MinhCoach.Infra.Migrations
{
    /// <inheritdoc />
    public partial class addCheckTaskRemiderAndTypeColumeForTemplate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Templates_TemplateId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_TemplateId",
                table: "Tasks");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Templates",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<bool>(
                name: "IsReminderSent",
                table: "Tasks",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsReminderSent",
                table: "SubTasks",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Templates");

            migrationBuilder.DropColumn(
                name: "IsReminderSent",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "IsReminderSent",
                table: "SubTasks");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_TemplateId",
                table: "Tasks",
                column: "TemplateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Templates_TemplateId",
                table: "Tasks",
                column: "TemplateId",
                principalTable: "Templates",
                principalColumn: "Id");
        }
    }
}
