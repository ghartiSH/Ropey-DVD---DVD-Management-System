using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace grouptest.Migrations
{
    public partial class databasefixed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DvdCopies_DvdTitles_DvdTitleDvdNumber",
                table: "DvdCopies");

            migrationBuilder.DropForeignKey(
                name: "FK_Loans_DvdCopies_DvdCopyCopyNumber",
                table: "Loans");

            migrationBuilder.DropForeignKey(
                name: "FK_Loans_LoanTypes_LoanTypeNumber",
                table: "Loans");

            migrationBuilder.DropForeignKey(
                name: "FK_Loans_Members_MemberNumber",
                table: "Loans");

            migrationBuilder.RenameColumn(
                name: "MemberNumber",
                table: "Loans",
                newName: "MemberId");

            migrationBuilder.RenameColumn(
                name: "LoanTypeNumber",
                table: "Loans",
                newName: "LoanTypeId");

            migrationBuilder.RenameColumn(
                name: "DvdCopyCopyNumber",
                table: "Loans",
                newName: "DvDCopyNumber");

            migrationBuilder.RenameIndex(
                name: "IX_Loans_MemberNumber",
                table: "Loans",
                newName: "IX_Loans_MemberId");

            migrationBuilder.RenameIndex(
                name: "IX_Loans_LoanTypeNumber",
                table: "Loans",
                newName: "IX_Loans_LoanTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Loans_DvdCopyCopyNumber",
                table: "Loans",
                newName: "IX_Loans_DvDCopyNumber");

            migrationBuilder.RenameColumn(
                name: "DvdTitleDvdNumber",
                table: "DvdCopies",
                newName: "DvdNumber");

            migrationBuilder.RenameIndex(
                name: "IX_DvdCopies_DvdTitleDvdNumber",
                table: "DvdCopies",
                newName: "IX_DvdCopies_DvdNumber");

            migrationBuilder.AlterColumn<string>(
                name: "LoanTypee",
                table: "LoanTypes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_DvdCopies_DvdTitles_DvdNumber",
                table: "DvdCopies",
                column: "DvdNumber",
                principalTable: "DvdTitles",
                principalColumn: "DvdNumber",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_DvdCopies_DvDCopyNumber",
                table: "Loans",
                column: "DvDCopyNumber",
                principalTable: "DvdCopies",
                principalColumn: "CopyNumber",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_LoanTypes_LoanTypeId",
                table: "Loans",
                column: "LoanTypeId",
                principalTable: "LoanTypes",
                principalColumn: "LoanTypeNumber",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_Members_MemberId",
                table: "Loans",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "MemberNumber",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DvdCopies_DvdTitles_DvdNumber",
                table: "DvdCopies");

            migrationBuilder.DropForeignKey(
                name: "FK_Loans_DvdCopies_DvDCopyNumber",
                table: "Loans");

            migrationBuilder.DropForeignKey(
                name: "FK_Loans_LoanTypes_LoanTypeId",
                table: "Loans");

            migrationBuilder.DropForeignKey(
                name: "FK_Loans_Members_MemberId",
                table: "Loans");

            migrationBuilder.RenameColumn(
                name: "MemberId",
                table: "Loans",
                newName: "MemberNumber");

            migrationBuilder.RenameColumn(
                name: "LoanTypeId",
                table: "Loans",
                newName: "LoanTypeNumber");

            migrationBuilder.RenameColumn(
                name: "DvDCopyNumber",
                table: "Loans",
                newName: "DvdCopyCopyNumber");

            migrationBuilder.RenameIndex(
                name: "IX_Loans_MemberId",
                table: "Loans",
                newName: "IX_Loans_MemberNumber");

            migrationBuilder.RenameIndex(
                name: "IX_Loans_LoanTypeId",
                table: "Loans",
                newName: "IX_Loans_LoanTypeNumber");

            migrationBuilder.RenameIndex(
                name: "IX_Loans_DvDCopyNumber",
                table: "Loans",
                newName: "IX_Loans_DvdCopyCopyNumber");

            migrationBuilder.RenameColumn(
                name: "DvdNumber",
                table: "DvdCopies",
                newName: "DvdTitleDvdNumber");

            migrationBuilder.RenameIndex(
                name: "IX_DvdCopies_DvdNumber",
                table: "DvdCopies",
                newName: "IX_DvdCopies_DvdTitleDvdNumber");

            migrationBuilder.AlterColumn<string>(
                name: "LoanTypee",
                table: "LoanTypes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DvdCopies_DvdTitles_DvdTitleDvdNumber",
                table: "DvdCopies",
                column: "DvdTitleDvdNumber",
                principalTable: "DvdTitles",
                principalColumn: "DvdNumber",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_DvdCopies_DvdCopyCopyNumber",
                table: "Loans",
                column: "DvdCopyCopyNumber",
                principalTable: "DvdCopies",
                principalColumn: "CopyNumber",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_LoanTypes_LoanTypeNumber",
                table: "Loans",
                column: "LoanTypeNumber",
                principalTable: "LoanTypes",
                principalColumn: "LoanTypeNumber",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_Members_MemberNumber",
                table: "Loans",
                column: "MemberNumber",
                principalTable: "Members",
                principalColumn: "MemberNumber",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
