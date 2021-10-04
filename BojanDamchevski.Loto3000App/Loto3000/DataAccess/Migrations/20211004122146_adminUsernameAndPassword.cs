using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class adminUsernameAndPassword : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Admin",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Admin",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Admin");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "Admin");
        }
    }
}
