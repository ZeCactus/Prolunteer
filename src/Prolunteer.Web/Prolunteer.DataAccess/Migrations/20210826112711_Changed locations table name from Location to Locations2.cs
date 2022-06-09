using Microsoft.EntityFrameworkCore.Migrations;

namespace Prolunteer.DataAccess.Migrations
{
    public partial class ChangedlocationstablenamefromLocationtoLocations2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable("Location", null, "Locations");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
