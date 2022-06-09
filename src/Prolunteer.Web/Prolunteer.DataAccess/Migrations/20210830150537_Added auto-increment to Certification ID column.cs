using Microsoft.EntityFrameworkCore.Migrations;

namespace Prolunteer.DataAccess.Migrations
{
    public partial class AddedautoincrementtoCertificationIDcolumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserCertification_Certification",
                table: "UserCertifications");
            migrationBuilder.DropForeignKey(
                name: "FK_RequiredCertification_Certification",
                table: "RequiredCertifications");
            migrationBuilder.DropPrimaryKey(
                name: "PK_Certifications",
                table: "Certifications");
            migrationBuilder.DropColumn(
                name: "ID",
                table: "Certifications");
            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "Certifications",
                type: "int",
                nullable: false)
                .Annotation("SqlServer:Identity", "1, 1");
            migrationBuilder.AddPrimaryKey(
                name: "PK_Certifications",
                table: "Certifications",
                column: "ID");
            migrationBuilder.AddForeignKey(
                name: "FK_UserCertification_Certification",
                table: "UserCertifications",
                column: "CertificationID",
                principalTable: "Certifications",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_RequiredCertification_Certification",
                table: "RequiredCertifications",
                column: "CertificationID",
                principalTable: "Certifications",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AlterColumn<int>(
            //    name: "ID",
            //    table: "Certifications",
            //    type: "int",
            //    nullable: false,
            //    oldClrType: typeof(int),
            //    oldType: "int")
            //    .OldAnnotation("SqlServer:Identity", "1, 1");
        }
    }
}
