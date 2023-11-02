using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoList.Server.Data.Migrations
{
    /// <inheritdoc />
    public partial class moreRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SubNotes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    NoteId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubNotes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SubNotes_Notes_NoteId",
                        column: x => x.NoteId,
                        principalTable: "Notes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubNotes_NoteId",
                table: "SubNotes",
                column: "NoteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubNotes");
        }
    }
}
