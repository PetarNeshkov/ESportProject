using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_SportManager.Data.Migrations
{
    public partial class addedWonAndLostMatchesToTeams : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LostGames",
                table: "Teams",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WonGames",
                table: "Teams",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LostGames",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "WonGames",
                table: "Teams");
        }
    }
}
