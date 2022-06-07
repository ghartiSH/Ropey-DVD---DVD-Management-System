using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace grouptest.Migrations
{
    public partial class titname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DvdTitleName",
                table: "DvdTitles",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DvdTitleName",
                table: "DvdTitles");
        }
    }
}
