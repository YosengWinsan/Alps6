using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Alps.Web.Service.Migrations
{
    public partial class loan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "InvalidDate",
                table: "Loan_LoanVouchers",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsInvalid",
                table: "Loan_LoanVouchers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Loan_LoanVoucher2s",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    CreateTime = table.Column<DateTimeOffset>(nullable: false),
                    Creater = table.Column<string>(nullable: true),
                    LenderID = table.Column<Guid>(nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InterestSettlementDate = table.Column<DateTimeOffset>(nullable: false),
                    InterestRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VoucherNumber = table.Column<string>(nullable: true),
                    IdentifyingCode = table.Column<int>(nullable: false),
                    IsInvalid = table.Column<bool>(nullable: false),
                    InvalidDate = table.Column<DateTimeOffset>(nullable: true),
                    InvalidMaker = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loan_LoanVoucher2s", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Loan_LoanVoucher2s_Loan_Lenders_LenderID",
                        column: x => x.LenderID,
                        principalTable: "Loan_Lenders",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Loan_LoanRecord",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    CreateTime = table.Column<DateTimeOffset>(nullable: false),
                    Creater = table.Column<string>(nullable: true),
                    OperateTime = table.Column<DateTimeOffset>(nullable: false),
                    LoanVoucherID = table.Column<Guid>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Interest = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Memo = table.Column<string>(nullable: true),
                    Reviewer = table.Column<string>(nullable: true),
                    ReviewTime = table.Column<DateTimeOffset>(nullable: false),
                    IsInvalid = table.Column<bool>(nullable: false),
                    InvalidDate = table.Column<DateTimeOffset>(nullable: true),
                    InvalidMaker = table.Column<string>(nullable: true),
                    LoanVoucher2ID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loan_LoanRecord", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Loan_LoanRecord_Loan_LoanVoucher2s_LoanVoucher2ID",
                        column: x => x.LoanVoucher2ID,
                        principalTable: "Loan_LoanVoucher2s",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Loan_LoanRecord_LoanVoucher2ID",
                table: "Loan_LoanRecord",
                column: "LoanVoucher2ID");

            migrationBuilder.CreateIndex(
                name: "IX_Loan_LoanVoucher2s_LenderID",
                table: "Loan_LoanVoucher2s",
                column: "LenderID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Loan_LoanRecord");

            migrationBuilder.DropTable(
                name: "Loan_LoanVoucher2s");

            migrationBuilder.DropColumn(
                name: "InvalidDate",
                table: "Loan_LoanVouchers");

            migrationBuilder.DropColumn(
                name: "IsInvalid",
                table: "Loan_LoanVouchers");
        }
    }
}
