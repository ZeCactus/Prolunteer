using Microsoft.EntityFrameworkCore.Migrations;

namespace Prolunteer.DataAccess.Migrations
{
    public partial class AddedautoincrementtoEventTypeIDcolumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Event_EventType",
                table: "Events");
            migrationBuilder.DropPrimaryKey(
                name: "PK_EventTypes",
                table: "EventTypes");
            migrationBuilder.DropColumn(
                name: "ID",
                table: "EventTypes");
            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "EventTypes",
                type: "int",
                nullable: false)
                .Annotation("SqlServer:Identity", "1, 1");
            migrationBuilder.AddPrimaryKey(
                name: "PK_EventTypes",
                table: "EventTypes",
                column: "ID");
            migrationBuilder.AddForeignKey(
                name: "FK_Event_EventType",
                table: "Events",
                column: "EventTypeID",
                principalTable: "EventTypes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AlterColumn<int>(
            //    name: "ID",
            //    table: "EventTypes",
            //    type: "int",
            //    nullable: false,
            //    oldClrType: typeof(int),
            //    oldType: "int")
            //    .OldAnnotation("SqlServer:Identity", "1, 1");
        }
    }
}
