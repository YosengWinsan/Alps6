using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Alps.Web.Service.Migrations
{
    public partial class LoanUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Loan_WithdrawRecords");

            migrationBuilder.DropColumn(
                name: "DepositDate",
                table: "Loan_LoanVouchers");

            migrationBuilder.DropColumn(
                name: "HashCode",
                table: "Loan_LoanVouchers");

            migrationBuilder.DropColumn(
                name: "ModifyDate",
                table: "Loan_LoanVouchers");

            migrationBuilder.DropColumn(
                name: "Operator",
                table: "Loan_LoanVouchers");

            migrationBuilder.RenameColumn(
                name: "OriginAmount",
                table: "Loan_LoanVouchers",
                newName: "DepositAmount");

            migrationBuilder.AlterColumn<decimal>(
                name: "InterestRate",
                table: "Loan_LoanVouchers",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,4)");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreateTime",
                table: "Loan_LoanVouchers",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "Creater",
                table: "Loan_LoanVouchers",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DepositTime",
                table: "Loan_LoanVouchers",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "IdentityCode",
                table: "Loan_LoanVouchers",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "InvalidDate",
                table: "Loan_LoanVouchers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InvalidMaker",
                table: "Loan_LoanVouchers",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsInvalid",
                table: "Loan_LoanVouchers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "VoucherTime",
                table: "Loan_LoanVouchers",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

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
                    InvalidMaker = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loan_LoanRecord", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Loan_LoanRecord_Loan_LoanVouchers_LoanVoucherID",
                        column: x => x.LoanVoucherID,
                        principalTable: "Loan_LoanVouchers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Loan_LoanSettings",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    MinDepositDay = table.Column<int>(nullable: false),
                    MinDepositAmount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loan_LoanSettings", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Loan_InterestRate",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    PublishDate = table.Column<DateTimeOffset>(nullable: false),
                    Publisher = table.Column<string>(nullable: true),
                    Rate = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    StartExecutionDate = table.Column<DateTimeOffset>(nullable: false),
                    LoanSettingID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loan_InterestRate", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Loan_InterestRate_Loan_LoanSettings_LoanSettingID",
                        column: x => x.LoanSettingID,
                        principalTable: "Loan_LoanSettings",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Loan_InterestRate_LoanSettingID",
                table: "Loan_InterestRate",
                column: "LoanSettingID");

            migrationBuilder.CreateIndex(
                name: "IX_Loan_LoanRecord_LoanVoucherID",
                table: "Loan_LoanRecord",
                column: "LoanVoucherID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Loan_InterestRate");

            migrationBuilder.DropTable(
                name: "Loan_LoanRecord");

            migrationBuilder.DropTable(
                name: "Loan_LoanSettings");

            migrationBuilder.DropColumn(
                name: "CreateTime",
                table: "Loan_LoanVouchers");

            migrationBuilder.DropColumn(
                name: "Creater",
                table: "Loan_LoanVouchers");

            migrationBuilder.DropColumn(
                name: "DepositTime",
                table: "Loan_LoanVouchers");

            migrationBuilder.DropColumn(
                name: "IdentityCode",
                table: "Loan_LoanVouchers");

            migrationBuilder.DropColumn(
                name: "InvalidDate",
                table: "Loan_LoanVouchers");

            migrationBuilder.DropColumn(
                name: "InvalidMaker",
                table: "Loan_LoanVouchers");

            migrationBuilder.DropColumn(
                name: "IsInvalid",
                table: "Loan_LoanVouchers");

            migrationBuilder.DropColumn(
                name: "VoucherTime",
                table: "Loan_LoanVouchers");

            migrationBuilder.RenameColumn(
                name: "DepositAmount",
                table: "Loan_LoanVouchers",
                newName: "OriginAmount");

            migrationBuilder.AlterColumn<decimal>(
                name: "InterestRate",
                table: "Loan_LoanVouchers",
                type: "decimal(7,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DepositDate",
                table: "Loan_LoanVouchers",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "HashCode",
                table: "Loan_LoanVouchers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ModifyDate",
                table: "Loan_LoanVouchers",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "Operator",
                table: "Loan_LoanVouchers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Loan_WithdrawRecords",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Date = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DepositDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Interest = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InterestRate = table.Column<decimal>(type: "decimal(7,4)", nullable: false),
                    LoanVoucherID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifyDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Operator = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loan_WithdrawRecords", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Loan_WithdrawRecords_Loan_LoanVouchers_LoanVoucherID",
                        column: x => x.LoanVoucherID,
                        principalTable: "Loan_LoanVouchers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Loan_WithdrawRecords_LoanVoucherID",
                table: "Loan_WithdrawRecords",
                column: "LoanVoucherID");
        }
    }
}
