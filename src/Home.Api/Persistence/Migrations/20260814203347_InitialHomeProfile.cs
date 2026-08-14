using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Home.Api.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialHomeProfile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HomeProfiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DisplayName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    CreatedAtUtc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomeProfiles", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HomeProfiles_DisplayName",
                table: "HomeProfiles",
                column: "DisplayName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HomeProfiles");
        }
    }
}
