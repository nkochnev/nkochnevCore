using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace NkochnevCore.Infrastructure.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    Key = table.Column<string>(maxLength: 100, nullable: false),
                    Content = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    Preview = table.Column<string>(nullable: true),
                    SeoDescription = table.Column<string>(maxLength: 1000, nullable: true),
                    SeoKeyWords = table.Column<string>(maxLength: 1000, nullable: true),
                    Title = table.Column<string>(maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.Key);
                });

            migrationBuilder.CreateTable(
                name: "Auth",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PassHash = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auth", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tokens",
                columns: table => new
                {
                    RefreshToken = table.Column<string>(maxLength: 40, nullable: false),
                    Expiresin = table.Column<DateTime>(nullable: false),
                    JwtToken = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tokens", x => x.RefreshToken);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropTable(
                name: "Auth");

            migrationBuilder.DropTable(
                name: "Tokens");
        }
    }
}
