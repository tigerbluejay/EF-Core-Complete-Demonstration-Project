using Microsoft.EntityFrameworkCore.Migrations;

namespace WizLib_DataAccess.Migrations
{
    public partial class AddManyToManybetweenFluentBookandFluentAuthorthroughFluentBookAuthor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FluentBookAuthor",
                columns: table => new
                {
                    FluentBook_Id = table.Column<int>(type: "int", nullable: false),
                    FluentAuthor_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FluentBookAuthor", x => new { x.FluentAuthor_Id, x.FluentBook_Id });
                    table.ForeignKey(
                        name: "FK_FluentBookAuthor_FluentAuthors_FluentAuthor_Id",
                        column: x => x.FluentAuthor_Id,
                        principalTable: "FluentAuthors",
                        principalColumn: "Author_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FluentBookAuthor_FluentBooks_FluentBook_Id",
                        column: x => x.FluentBook_Id,
                        principalTable: "FluentBooks",
                        principalColumn: "Book_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FluentBookAuthor_FluentBook_Id",
                table: "FluentBookAuthor",
                column: "FluentBook_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FluentBookAuthor");
        }
    }
}
