using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ofernandoavila.Mailman.Data.Migrations
{
    /// <inheritdoc />
    public partial class LICENSE_ENTITY : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_LICENSE",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ApplicationName = table.Column<string>(type: "varchar(100)", nullable: false),
                    ValidUntil = table.Column<DateTime>(type: "timestamp", nullable: true),
                    Hosts = table.Column<string>(type: "varchar(900)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "timezone('America/Bahia', now())"),
                    Key = table.Column<string>(type: "character varying(100)", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_LICENSE", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TB_LICENSE_TB_USER_UserId",
                        column: x => x.UserId,
                        principalTable: "TB_USER",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_APPLICATIONNAME_TB_LICENSE",
                table: "TB_LICENSE",
                column: "ApplicationName");

            migrationBuilder.CreateIndex(
                name: "IX_USERID_TB_LICENSE",
                table: "TB_LICENSE",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_LICENSE");
        }
    }
}
