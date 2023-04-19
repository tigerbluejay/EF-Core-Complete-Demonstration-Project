using Microsoft.EntityFrameworkCore.Migrations;

namespace WizLib_DataAccess.Migrations
{
    public partial class ChangeNametoGenreNameinGenreTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Genres",
                newName: "GenreName");

            // to reverse it you could write sql
            // migrationBuilder.Sql("UPDATE dbo.genres SET GenreName=Name")
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GenreName",
                table: "Genres",
                newName: "Name");

            // migrationBuilder.Sql("UPDATE dbo.genres SET Name=GenreName")

        }
    }
}
