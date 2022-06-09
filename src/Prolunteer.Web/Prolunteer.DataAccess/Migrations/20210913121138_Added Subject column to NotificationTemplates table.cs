using Microsoft.EntityFrameworkCore.Migrations;

namespace Prolunteer.DataAccess.Migrations
{
    public partial class AddedSubjectcolumntoNotificationTemplatestable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Subject",
                table: "NotificationTemplates",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Subject",
                table: "NotificationTemplates");
        }
    }
}
