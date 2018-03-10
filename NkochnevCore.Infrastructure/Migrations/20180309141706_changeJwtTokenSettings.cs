using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace NkochnevCore.Infrastructure.Migrations
{
    public partial class changeJwtTokenSettings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "JwtToken",
                table: "Tokens",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "JwtToken",
                table: "Tokens",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
