using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoList.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedTaskHistoryTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskHistory_Task_TaskTableTaskId",
                table: "TaskHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskHistory_User_UserId",
                table: "TaskHistory");

            migrationBuilder.DropIndex(
                name: "IX_TaskHistory_TaskTableTaskId",
                table: "TaskHistory");

            migrationBuilder.DropColumn(
                name: "TaskTableTaskId",
                table: "TaskHistory");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "TaskHistory",
                newName: "FKTaskAssignedByUser");

            migrationBuilder.RenameColumn(
                name: "UpdatedDateTime",
                table: "TaskHistory",
                newName: "AssignedDateTime");

            migrationBuilder.RenameIndex(
                name: "IX_TaskHistory_UserId",
                table: "TaskHistory",
                newName: "IX_TaskHistory_FKTaskAssignedByUser");

            migrationBuilder.CreateIndex(
                name: "IX_TaskHistory_FKTaskId",
                table: "TaskHistory",
                column: "FKTaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskHistory_Task_FKTaskId",
                table: "TaskHistory",
                column: "FKTaskId",
                principalTable: "Task",
                principalColumn: "TaskId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskHistory_User_FKTaskAssignedByUser",
                table: "TaskHistory",
                column: "FKTaskAssignedByUser",
                principalTable: "User",
                principalColumn: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskHistory_Task_FKTaskId",
                table: "TaskHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskHistory_User_FKTaskAssignedByUser",
                table: "TaskHistory");

            migrationBuilder.DropIndex(
                name: "IX_TaskHistory_FKTaskId",
                table: "TaskHistory");

            migrationBuilder.RenameColumn(
                name: "FKTaskAssignedByUser",
                table: "TaskHistory",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "AssignedDateTime",
                table: "TaskHistory",
                newName: "UpdatedDateTime");

            migrationBuilder.RenameIndex(
                name: "IX_TaskHistory_FKTaskAssignedByUser",
                table: "TaskHistory",
                newName: "IX_TaskHistory_UserId");

            migrationBuilder.AddColumn<int>(
                name: "TaskTableTaskId",
                table: "TaskHistory",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaskHistory_TaskTableTaskId",
                table: "TaskHistory",
                column: "TaskTableTaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskHistory_Task_TaskTableTaskId",
                table: "TaskHistory",
                column: "TaskTableTaskId",
                principalTable: "Task",
                principalColumn: "TaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskHistory_User_UserId",
                table: "TaskHistory",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId");
        }
    }
}
