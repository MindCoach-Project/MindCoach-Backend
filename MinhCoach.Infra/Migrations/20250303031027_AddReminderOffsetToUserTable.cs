using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MinhCoach.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AddReminderOffsetToUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "reminderOffset",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 5);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "reminderOffset",
                table: "Users");
        }
    }
}
