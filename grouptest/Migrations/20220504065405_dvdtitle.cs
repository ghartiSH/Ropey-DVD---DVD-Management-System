using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace grouptest.Migrations
{
    public partial class dvdtitle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DvdTitles_DvdCategories_DvdCategoryCategoryNumber",
                table: "DvdTitles");

            migrationBuilder.RenameColumn(
                name: "DvdCategoryCategoryNumber",
                table: "DvdTitles",
                newName: "CategoryNumber");

            migrationBuilder.RenameIndex(
                name: "IX_DvdTitles_DvdCategoryCategoryNumber",
                table: "DvdTitles",
                newName: "IX_DvdTitles_CategoryNumber");

            migrationBuilder.AlterColumn<string>(
                name: "StudioName",
                table: "Studios",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CategoryDescription",
                table: "DvdCategories",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_DvdTitles_DvdCategories_CategoryNumber",
                table: "DvdTitles",
                column: "CategoryNumber",
                principalTable: "DvdCategories",
                principalColumn: "CategoryNumber",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DvdTitles_DvdCategories_CategoryNumber",
                table: "DvdTitles");

            migrationBuilder.RenameColumn(
                name: "CategoryNumber",
                table: "DvdTitles",
                newName: "DvdCategoryCategoryNumber");

            migrationBuilder.RenameIndex(
                name: "IX_DvdTitles_CategoryNumber",
                table: "DvdTitles",
                newName: "IX_DvdTitles_DvdCategoryCategoryNumber");

            migrationBuilder.AlterColumn<string>(
                name: "StudioName",
                table: "Studios",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CategoryDescription",
                table: "DvdCategories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DvdTitles_DvdCategories_DvdCategoryCategoryNumber",
                table: "DvdTitles",
                column: "DvdCategoryCategoryNumber",
                principalTable: "DvdCategories",
                principalColumn: "CategoryNumber",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
