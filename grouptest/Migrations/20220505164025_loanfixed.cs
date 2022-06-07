using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace grouptest.Migrations
{
    public partial class loanfixed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Loans_DvdCopies_DvDCopyNumber",
                table: "Loans");

            migrationBuilder.DropForeignKey(
                name: "FK_Loans_LoanTypes_LoanTypeId",
                table: "Loans");

            migrationBuilder.RenameColumn(
                name: "LoanTypeId",
                table: "Loans",
                newName: "LoanTypeNumber");

            migrationBuilder.RenameColumn(
                name: "DvDCopyNumber",
                table: "Loans",
                newName: "DvdNumber");

            migrationBuilder.RenameIndex(
                name: "IX_Loans_LoanTypeId",
                table: "Loans",
                newName: "IX_Loans_LoanTypeNumber");

            migrationBuilder.RenameIndex(
                name: "IX_Loans_DvDCopyNumber",
                table: "Loans",
                newName: "IX_Loans_DvdNumber");

            migrationBuilder.AlterColumn<string>(
                name: "ActorSurname",
                table: "Actors",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "ActorFirstName",
                table: "Actors",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_DvdCopies_DvdNumber",
                table: "Loans",
                column: "DvdNumber",
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Loans_DvdCopies_DvdNumber",
                table: "Loans");

            migrationBuilder.DropForeignKey(
                name: "FK_Loans_LoanTypes_LoanTypeNumber",
                table: "Loans");

            migrationBuilder.RenameColumn(
                name: "LoanTypeNumber",
                table: "Loans",
                newName: "LoanTypeId");

            migrationBuilder.RenameColumn(
                name: "DvdNumber",
                table: "Loans",
                newName: "DvDCopyNumber");

            migrationBuilder.RenameIndex(
                name: "IX_Loans_LoanTypeNumber",
                table: "Loans",
                newName: "IX_Loans_LoanTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Loans_DvdNumber",
                table: "Loans",
                newName: "IX_Loans_DvDCopyNumber");

            migrationBuilder.AlterColumn<string>(
                name: "ActorSurname",
                table: "Actors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ActorFirstName",
                table: "Actors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

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
        }
    }
}
