using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LotoNumber_Users_UserId",
                table: "LotoNumber");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LotoNumber",
                table: "LotoNumber");

            migrationBuilder.RenameTable(
                name: "LotoNumber",
                newName: "LotoNumbers");

            migrationBuilder.RenameIndex(
                name: "IX_LotoNumber_UserId",
                table: "LotoNumbers",
                newName: "IX_LotoNumbers_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LotoNumbers",
                table: "LotoNumbers",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Prizes",
                keyColumn: "Id",
                keyValue: 1,
                column: "PrizeNumber",
                value: 16);

            migrationBuilder.UpdateData(
                table: "Prizes",
                keyColumn: "Id",
                keyValue: 2,
                column: "PrizeNumber",
                value: 21);

            migrationBuilder.UpdateData(
                table: "Prizes",
                keyColumn: "Id",
                keyValue: 3,
                column: "PrizeNumber",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Prizes",
                keyColumn: "Id",
                keyValue: 4,
                column: "PrizeNumber",
                value: 24);

            migrationBuilder.UpdateData(
                table: "Prizes",
                keyColumn: "Id",
                keyValue: 5,
                column: "PrizeNumber",
                value: 19);

            migrationBuilder.AddForeignKey(
                name: "FK_LotoNumbers_Users_UserId",
                table: "LotoNumbers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LotoNumbers_Users_UserId",
                table: "LotoNumbers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LotoNumbers",
                table: "LotoNumbers");

            migrationBuilder.RenameTable(
                name: "LotoNumbers",
                newName: "LotoNumber");

            migrationBuilder.RenameIndex(
                name: "IX_LotoNumbers_UserId",
                table: "LotoNumber",
                newName: "IX_LotoNumber_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LotoNumber",
                table: "LotoNumber",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Prizes",
                keyColumn: "Id",
                keyValue: 1,
                column: "PrizeNumber",
                value: 13);

            migrationBuilder.UpdateData(
                table: "Prizes",
                keyColumn: "Id",
                keyValue: 2,
                column: "PrizeNumber",
                value: 14);

            migrationBuilder.UpdateData(
                table: "Prizes",
                keyColumn: "Id",
                keyValue: 3,
                column: "PrizeNumber",
                value: 11);

            migrationBuilder.UpdateData(
                table: "Prizes",
                keyColumn: "Id",
                keyValue: 4,
                column: "PrizeNumber",
                value: 13);

            migrationBuilder.UpdateData(
                table: "Prizes",
                keyColumn: "Id",
                keyValue: 5,
                column: "PrizeNumber",
                value: 10);

            migrationBuilder.AddForeignKey(
                name: "FK_LotoNumber_Users_UserId",
                table: "LotoNumber",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
