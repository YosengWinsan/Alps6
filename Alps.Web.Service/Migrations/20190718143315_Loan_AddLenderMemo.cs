using Microsoft.EntityFrameworkCore.Migrations;

namespace Alps.Web.Service.Migrations
{
    public partial class Loan_AddLenderMemo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Memo",
                table: "Lenders",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Memo",
                table: "Lenders");
        }
    }
}
