using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace data_access.Migrations
{
    public partial class BaseModelAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tests");

            migrationBuilder.DropTable(
                name: "users");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tests",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: true),
                    name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tests", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    createdDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    email = table.Column<string>(type: "text", nullable: true),
                    password = table.Column<string>(type: "text", nullable: true),
                    username = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });
        }
    }
}
