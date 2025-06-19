using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ofernandoavila.Mailman.Data.Migrations
{
    /// <inheritdoc />
    public partial class DATABASE_INIT : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_ROLE",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "varchar(50)", nullable: false),
                    Active = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_ROLE", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TB_USER",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", nullable: false),
                    Email = table.Column<string>(type: "varchar(100)", nullable: false),
                    Password = table.Column<string>(type: "varchar(64)", nullable: false),
                    FirstAccess = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    SignUpEmailSent = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordResetEmailSent = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "timezone('America/Bahia', now())"),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_USER", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TB_USER_TB_ROLE_RoleId",
                        column: x => x.RoleId,
                        principalTable: "TB_ROLE",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TB_SESSION",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserAgent = table.Column<string>(type: "varchar(500)", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "timezone('America/Bahia', now())"),
                    ExpirationTime = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Token = table.Column<string>(type: "varchar(1000)", nullable: false),
                    RefreshToken = table.Column<string>(type: "varchar(100)", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_SESSION", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TB_SESSION_TB_USER_UserId",
                        column: x => x.UserId,
                        principalTable: "TB_USER",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "TB_ROLE",
                columns: new[] { "Id", "Active", "Description" },
                values: new object[,]
                {
                    { new Guid("0b9b96b8-c083-4c5e-b2b3-c9b142302def"), 1, "System" },
                    { new Guid("4b4f973b-2f57-4671-a302-60cfecbc1bf9"), 1, "StoreOwner" },
                    { new Guid("63569f70-acee-4296-83a7-ef495203c890"), 1, "Client" },
                    { new Guid("775611a5-7f0b-46b9-8a2c-1f2526d865e5"), 1, "Administrator" }
                });

            migrationBuilder.InsertData(
                table: "TB_USER",
                columns: new[] { "Id", "Active", "Email", "Name", "Password", "PasswordResetEmailSent", "RoleId", "SignUpEmailSent" },
                values: new object[] { new Guid("e4493800-676b-4d9a-921a-f0bf171462f1"), true, "fernandoavilajunior@gmail.com", "System", "12e8f78ad914d3aef2532615a71c541bbed8473a6140399ca54d3ddd516e1ad8", false, new Guid("0b9b96b8-c083-4c5e-b2b3-c9b142302def"), false });

            migrationBuilder.CreateIndex(
                name: "IX_DESCRIPTION_TB_ROLE",
                table: "TB_ROLE",
                column: "Description",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TB_SESSION_UserId",
                table: "TB_SESSION",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_EMAIL_TB_USER",
                table: "TB_USER",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NAME_TB_USER",
                table: "TB_USER",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_ROLEID_TB_USER",
                table: "TB_USER",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_SESSION");

            migrationBuilder.DropTable(
                name: "TB_USER");

            migrationBuilder.DropTable(
                name: "TB_ROLE");
        }
    }
}
