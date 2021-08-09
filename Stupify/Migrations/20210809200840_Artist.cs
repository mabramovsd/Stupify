using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Stupify.Migrations
{
    public partial class Artist : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Artists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artists", x => x.Id);
                });

            migrationBuilder.AddColumn<int>(
                name: "ArtistId",
                table: "Songs",
                type: "integer",
                nullable: false,
                defaultValue: 0);



            migrationBuilder.InsertData(
                table: "Artists",
                columns: new[] { "Id", "Name", "Description" },
                values: new object[,]
                {
                    { 1, "Неизвестно", "Всякая народная музыка" },
                    { 2, "Slipknot", "Slipknot - nu-metal группа из США. Основана в 1995 году..." },
                    { 3, "Zerodovich", "Zerodovich" }
                });

            migrationBuilder.Sql("UPDATE \"Songs\" SET \"ArtistId\" = 1 WHERE \"Artist\" = 'Народное'");
            migrationBuilder.Sql("UPDATE \"Songs\" SET \"ArtistId\" = 2 WHERE \"Artist\" = 'Slipknot'");
            migrationBuilder.Sql("UPDATE \"Songs\" SET \"ArtistId\" = 3 WHERE \"Artist\" = 'Zerodovich'");




            migrationBuilder.DropColumn(
                name: "Artist",
                table: "Songs");

            migrationBuilder.CreateIndex(
                name: "IX_Songs_ArtistId",
                table: "Songs",
                column: "ArtistId");

            migrationBuilder.AddForeignKey(
                name: "FK_Songs_Artists_ArtistId",
                table: "Songs",
                column: "ArtistId",
                principalTable: "Artists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Songs_Artists_ArtistId",
                table: "Songs");

            migrationBuilder.DropTable(
                name: "Artists");

            migrationBuilder.DropIndex(
                name: "IX_Songs_ArtistId",
                table: "Songs");

            migrationBuilder.DropColumn(
                name: "ArtistId",
                table: "Songs");

            migrationBuilder.AddColumn<string>(
                name: "Artist",
                table: "Songs",
                type: "text",
                nullable: true);
        }
    }
}
