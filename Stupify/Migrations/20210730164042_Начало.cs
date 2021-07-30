using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Stupify.Migrations
{
    public partial class Начало : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Songs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Artist = table.Column<string>(type: "text", nullable: true),
                    Address = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Songs", x => x.Id);
                });


            migrationBuilder.InsertData(
                table: "Songs",
                columns: new[] { "Id", "Name", "Artist", "Address" },
                values: new object[,]
                {
                    { 1, "Шум леса", "Народное",
                    "https://noisefx.ru/noise_base/03/01410.mp3" },
                    { 2, "Wait And Bleed", "Slipknot",
                    "http://slipknotation.narod.ru/mp3/slipknot/Slipknot_-_Wait_and_bleed.mp3" },
                    { 3, "Баклажан (♂Right Version♂)", "Zerodovich",
                    "https://api.meowpad.me/v2/sounds/preview/23869.m4a" }
                });

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Songs");
        }
    }
}
