using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace grouptest.Migrations
{
    public partial class CWgroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChargeViewModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TotalCharge = table.Column<int>(type: "int", nullable: false),
                    DateDue = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateReturn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StandardCharge = table.Column<int>(type: "int", nullable: false),
                    PenaltyCharge = table.Column<int>(type: "int", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CopyNumber = table.Column<int>(type: "int", nullable: false),
                    DvdTitleName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChargeViewModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DvdCopiesViewModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CopyNumber = table.Column<int>(type: "int", nullable: false),
                    DatePurchased = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DvdCopiesViewModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LoanModelView",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoanNumber = table.Column<int>(type: "int", nullable: false),
                    CopyNumber = table.Column<int>(type: "int", nullable: false),
                    MemberNumber = table.Column<int>(type: "int", nullable: false),
                    MemberLastname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MemberFirstname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MemberAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DvdTitleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOut = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateDue = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateReturn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NoOfDays = table.Column<int>(type: "int", nullable: false),
                    TotalLoans = table.Column<int>(type: "int", nullable: false),
                    MemberShipID = table.Column<int>(type: "int", nullable: false),
                    MembershipCategoryTotalLoans = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanModelView", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChargeViewModel");

            migrationBuilder.DropTable(
                name: "DvdCopiesViewModel");

            migrationBuilder.DropTable(
                name: "LoanModelView");
        }
    }
}
