using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace grouptest.Migrations
{
    public partial class castmemberadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CastMembers",
                columns: table => new
                {
                    ActorNumber = table.Column<int>(type: "int", nullable: false),
                    DvdNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_CastMembers_Actors_ActorNumber",
                        column: x => x.ActorNumber,
                        principalTable: "Actors",
                        principalColumn: "ActorNumber",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CastMembers_DvdTitles_DvdNumber",
                        column: x => x.DvdNumber,
                        principalTable: "DvdTitles",
                        principalColumn: "DvdNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CastMembers_ActorNumber",
                table: "CastMembers",
                column: "ActorNumber");

            migrationBuilder.CreateIndex(
                name: "IX_CastMembers_DvdNumber",
                table: "CastMembers",
                column: "DvdNumber");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CastMembers");
        }
    }
}
