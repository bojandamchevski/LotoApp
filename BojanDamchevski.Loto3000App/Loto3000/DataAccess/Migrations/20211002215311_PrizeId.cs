using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class PrizeId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Prizes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Prizes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "PrizeNumber",
                table: "Prizes");

            migrationBuilder.AddColumn<string>(
                name: "Prize",
                table: "Users",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Prizes",
                keyColumn: "Id",
                keyValue: 3,
                column: "PrizeType",
                value: "50$ GiftCard");

            migrationBuilder.UpdateData(
                table: "Prizes",
                keyColumn: "Id",
                keyValue: 5,
                column: "PrizeType",
                value: "TV");

            migrationBuilder.InsertData(
                table: "Prizes",
                columns: new[] { "Id", "PrizeType" },
                values: new object[,]
                {
                    { 7, "Car" },
                    { 6, "Vacation" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Prizes",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Prizes",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DropColumn(
                name: "Prize",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "PrizeNumber",
                table: "Prizes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Prizes",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "PrizeNumber", "PrizeType" },
                values: new object[] { 1, "TV" });

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
                columns: new[] { "PrizeNumber", "PrizeType" },
                values: new object[] { 19, "50$ GiftCard" });

            migrationBuilder.InsertData(
                table: "Prizes",
                columns: new[] { "Id", "PrizeNumber", "PrizeType" },
                values: new object[,]
                {
                    { 1, 16, "Car" },
                    { 2, 21, "Vacation" }
                });
        }
    }
}
