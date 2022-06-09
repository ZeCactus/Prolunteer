using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Prolunteer.DataAccess.Migrations
{
    public partial class Addedlocationstable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Event_City",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "CityID",
                table: "Events",
                newName: "LocationID");

            migrationBuilder.RenameIndex(
                name: "IX_Events_CityID",
                table: "Events",
                newName: "IX_Events_LocationID");

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Location_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Location_CityId",
                table: "Location",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Event_Location",
                table: "Events",
                column: "LocationID",
                principalTable: "Location",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Event_Location",
                table: "Events");

            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.RenameColumn(
                name: "LocationID",
                table: "Events",
                newName: "CityID");

            migrationBuilder.RenameIndex(
                name: "IX_Events_LocationID",
                table: "Events",
                newName: "IX_Events_CityID");

            migrationBuilder.AddForeignKey(
                name: "FK_Event_City",
                table: "Events",
                column: "CityID",
                principalTable: "Cities",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
