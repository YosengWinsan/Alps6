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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InvalidDate",
                table: "Loan_LoanVouchers");

            migrationBuilder.DropColumn(
                name: "IsInvalid",
                table: "Loan_LoanVouchers");
        }
    }
}
