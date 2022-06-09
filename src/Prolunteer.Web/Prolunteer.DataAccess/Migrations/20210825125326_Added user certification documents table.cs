using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Prolunteer.DataAccess.Migrations
{
    public partial class Addedusercertificationdocumentstable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserCertification_UserCertifications_UserCertificationUserId_UserCertificationCertificationId",
                table: "UserCertification");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserCertification",
                table: "UserCertification");

            migrationBuilder.DropIndex(
                name: "IX_UserCertification_UserCertificationUserId_UserCertificationCertificationId",
                table: "UserCertification");

            migrationBuilder.DropColumn(
                name: "UserCertificationCertificationId",
                table: "UserCertification");

            migrationBuilder.DropColumn(
                name: "UserCertificationUserId",
                table: "UserCertification");

            migrationBuilder.RenameTable(
                name: "UserCertification",
                newName: "UserCertificationDocuments");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "UserCertificationDocuments",
                newName: "UserID");

            migrationBuilder.RenameColumn(
                name: "CertificationId",
                table: "UserCertificationDocuments",
                newName: "CertificationID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "UserCertificationDocuments",
                newName: "ID");

            migrationBuilder.AlterColumn<byte[]>(
                name: "Document",
                table: "UserCertificationDocuments",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0],
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserCertificationDocuments",
                table: "UserCertificationDocuments",
                column: "ID");

            migrationBuilder.CreateIndex(
                name: "IX_UserCertificationDocuments_UserID_CertificationID",
                table: "UserCertificationDocuments",
                columns: new[] { "UserID", "CertificationID" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserCertificationDocument_UserCertification",
                table: "UserCertificationDocuments",
                columns: new[] { "UserID", "CertificationID" },
                principalTable: "UserCertifications",
                principalColumns: new[] { "UserID", "CertificationID" },
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserCertificationDocument_UserCertification",
                table: "UserCertificationDocuments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserCertificationDocuments",
                table: "UserCertificationDocuments");

            migrationBuilder.DropIndex(
                name: "IX_UserCertificationDocuments_UserID_CertificationID",
                table: "UserCertificationDocuments");

            migrationBuilder.RenameTable(
                name: "UserCertificationDocuments",
                newName: "UserCertification");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "UserCertification",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "CertificationID",
                table: "UserCertification",
                newName: "CertificationId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "UserCertification",
                newName: "Id");

            migrationBuilder.AlterColumn<byte[]>(
                name: "Document",
                table: "UserCertification",
                type: "varbinary(max)",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)");

            migrationBuilder.AddColumn<int>(
                name: "UserCertificationCertificationId",
                table: "UserCertification",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserCertificationUserId",
                table: "UserCertification",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserCertification",
                table: "UserCertification",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserCertification_UserCertificationUserId_UserCertificationCertificationId",
                table: "UserCertification",
                columns: new[] { "UserCertificationUserId", "UserCertificationCertificationId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserCertification_UserCertifications_UserCertificationUserId_UserCertificationCertificationId",
                table: "UserCertification",
                columns: new[] { "UserCertificationUserId", "UserCertificationCertificationId" },
                principalTable: "UserCertifications",
                principalColumns: new[] { "UserID", "CertificationID" },
                onDelete: ReferentialAction.Restrict);
        }
    }
}
