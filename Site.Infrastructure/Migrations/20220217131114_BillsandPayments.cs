using Microsoft.EntityFrameworkCore.Migrations;

namespace Site.Infrastructure.Migrations
{
    public partial class BillsandPayments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dues");

            migrationBuilder.AddColumn<decimal>(
                name: "Dues",
                table: "Bills",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Dues",
                table: "BillPayments",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Dues",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "Dues",
                table: "BillPayments");

            migrationBuilder.CreateTable(
                name: "Dues",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fee = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Month = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dues", x => x.ID);
                });
        }
    }
}
