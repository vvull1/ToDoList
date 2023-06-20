using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoList.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedTaskHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskHistory_User_FKTaskAssignedByUser",
                table: "TaskHistory");

            migrationBuilder.DropIndex(
                name: "IX_TaskHistory_FKTaskAssignedByUser",
                table: "TaskHistory");

            migrationBuilder.DropColumn(
                name: "FKTaskAssignedByUser",
                table: "TaskHistory");

            migrationBuilder.CreateIndex(
                name: "IX_TaskHistory_FKTaskAssignedByUserId",
                table: "TaskHistory",
                column: "FKTaskAssignedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskHistory_User_FKTaskAssignedByUserId",
                table: "TaskHistory",
                column: "FKTaskAssignedByUserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskHistory_User_FKTaskAssignedByUserId",
                table: "TaskHistory");

            migrationBuilder.DropIndex(
                name: "IX_TaskHistory_FKTaskAssignedByUserId",
                table: "TaskHistory");

            migrationBuilder.AddColumn<int>(
                name: "FKTaskAssignedByUser",
                table: "TaskHistory",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaskHistory_FKTaskAssignedByUser",
                table: "TaskHistory",
                column: "FKTaskAssignedByUser");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskHistory_User_FKTaskAssignedByUser",
                table: "TaskHistory",
                column: "FKTaskAssignedByUser",
                principalTable: "User",
                principalColumn: "UserId");
        }
    }
}
