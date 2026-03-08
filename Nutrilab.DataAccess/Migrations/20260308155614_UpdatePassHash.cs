using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nutrilab.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePassHash : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3L,
                column: "PasswordHash",
                value: "$2a$11$t58KV3nhGBV.iCCVlDo7cekNrrlpziyB5YX8GE5ZxBZg1yC3PvB8m");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4L,
                column: "PasswordHash",
                value: "$2a$11$t58KV3nhGBV.iCCVlDo7cekNrrlpziyB5YX8GE5ZxBZg1yC3PvB8m");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3L,
                column: "PasswordHash",
                value: "$2a$11$E67GgfyxuXJa4s1WwWMYb..4dqZco51JsjqToW/RPjLBD/0CzZcIS");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4L,
                column: "PasswordHash",
                value: "$2a$11$r76Xawm/dDZHs8Y4nVjb4ujEtfy4cN9Unw7oF/xuo/26Q9N5qRL52");
        }
    }
}
