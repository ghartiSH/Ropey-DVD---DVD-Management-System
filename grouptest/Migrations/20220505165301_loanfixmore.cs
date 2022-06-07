using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace grouptest.Migrations
{
    public partial class loanfixmore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Loans_DvdCopies_DvdNumber",
                table: "Loans");

            migrationBuilder.DropForeignKey(
                name: "FK_Loans_Members_MemberId",
                table: "Loans");

            migrationBuilder.RenameColumn(
                name: "MemberId",
                table: "Loans",
                newName: "MemberNumber");

            migrationBuilder.RenameColumn(
                name: "DvdNumber",
                table: "Loans",
                newName: "CopyNumber");

            migrationBuilder.RenameIndex(
                name: "IX_Loans_MemberId",
                table: "Loans",
                newName: "IX_Loans_MemberNumber");

            migrationBuilder.RenameIndex(
                name: "IX_Loans_DvdNumber",
                table: "Loans",
                newName: "IX_Loans_CopyNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_DvdCopies_CopyNumber",
                table: "Loans",
                column: "CopyNumber",
                principalTable: "DvdCopies",
                principalColumn: "CopyNumber",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_Members_MemberNumber",
                table: "Loans",
                column: "MemberNumber",
                principalTable: "Members",
                principalColumn: "MemberNumber",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Loans_DvdCopies_CopyNumber",
                table: "Loans");

            migrationBuilder.DropForeignKey(
                name: "FK_Loans_Members_MemberNumber",
                table: "Loans");

            migrationBuilder.RenameColumn(
                name: "MemberNumber",
                table: "Loans",
                newName: "MemberId");

            migrationBuilder.RenameColumn(
                name: "CopyNumber",
                table: "Loans",
                newName: "DvdNumber");

            migrationBuilder.RenameIndex(
                name: "IX_Loans_MemberNumber",
                table: "Loans",
                newName: "IX_Loans_MemberId");

            migrationBuilder.RenameIndex(
                name: "IX_Loans_CopyNumber",
                table: "Loans",
                newName: "IX_Loans_DvdNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_DvdCopies_DvdNumber",
                table: "Loans",
                column: "DvdNumber",
                principalTable: "DvdCopies",
                principalColumn: "CopyNumber",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_Members_MemberId",
                table: "Loans",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "MemberNumber",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
