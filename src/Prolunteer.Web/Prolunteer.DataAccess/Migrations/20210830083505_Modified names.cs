using Microsoft.EntityFrameworkCore.Migrations;

namespace Prolunteer.DataAccess.Migrations
{
    public partial class Modifiednames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Location_Cities_CityId",
                table: "Locations");

            migrationBuilder.DropForeignKey(
                name: "FK_Event_Location",
                table: "Events");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Location",
                table: "Locations");

            //migrationBuilder.RenameTable(
            //    name: "Location",
            //    newName: "Locations");

            migrationBuilder.RenameColumn(
                name: "CityId",
                table: "Locations",
                newName: "CityID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Locations",
                newName: "ID");

            migrationBuilder.RenameIndex(
                name: "IX_Location_CityId",
                table: "Locations",
                newName: "IX_Locations_CityID");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Locations",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Locations",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Locations",
                table: "Locations",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Location_City",
                table: "Locations",
                column: "CityID",
                principalTable: "Cities",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_Event_Location",
                table: "Events",
                column: "LocationID",
                principalTable: "Locations",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Location_City",
                table: "Locations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Locations",
                table: "Locations");

            migrationBuilder.RenameTable(
                name: "Locations",
                newName: "Location");

            migrationBuilder.RenameColumn(
                name: "CityID",
                table: "Location",
                newName: "CityId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Location",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Locations_CityID",
                table: "Location",
                newName: "IX_Location_CityId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Location",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Location",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Location",
                table: "Location",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Location_Cities_CityId",
                table: "Location",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
