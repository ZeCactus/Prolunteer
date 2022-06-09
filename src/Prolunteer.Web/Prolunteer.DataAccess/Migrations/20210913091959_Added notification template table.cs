using Microsoft.EntityFrameworkCore.Migrations;

namespace Prolunteer.DataAccess.Migrations
{
    public partial class Addednotificationtemplatetable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NotificationTemplates",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    NotificationName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Template = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationTemplates", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NotificationTemplates");
        }
    }
}
