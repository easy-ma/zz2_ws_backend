using Microsoft.EntityFrameworkCore.Migrations;

namespace data_access.Migrations
{
    public partial class RefreshTokenUpdateFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "RefreshTokens",
                newName: "id");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "RefreshTokens",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "RefreshTokens");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "RefreshTokens",
                newName: "Id");
        }
    }
}
