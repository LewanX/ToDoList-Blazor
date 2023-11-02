using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoList.Server.Data.Migrations
{
    /// <inheritdoc />
    public partial class fixNoteId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubNotes_Notes_NoteId",
                table: "SubNotes");

            migrationBuilder.AlterColumn<int>(
                name: "NoteId",
                table: "SubNotes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SubNotes_Notes_NoteId",
                table: "SubNotes",
                column: "NoteId",
                principalTable: "Notes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubNotes_Notes_NoteId",
                table: "SubNotes");

            migrationBuilder.AlterColumn<int>(
                name: "NoteId",
                table: "SubNotes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_SubNotes_Notes_NoteId",
                table: "SubNotes",
                column: "NoteId",
                principalTable: "Notes",
                principalColumn: "Id");
        }
    }
}
