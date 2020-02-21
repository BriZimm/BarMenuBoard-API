using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BarMenuBoardAPI.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Recipes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    Instructions = table.Column<string>(nullable: false),
                    Ingredients = table.Column<string>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    Glass = table.Column<int>(nullable: false),
                    Served = table.Column<int>(nullable: false),
                    Garnish = table.Column<string>(nullable: false),
                    SimilarTastes = table.Column<string>(nullable: false),
                    Strength = table.Column<int>(nullable: false),
                    Image = table.Column<string>(nullable: false),
                    CurrentSpecial = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TodaysSpecials",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    Glass = table.Column<int>(nullable: false),
                    Served = table.Column<int>(nullable: false),
                    Garnish = table.Column<string>(nullable: false),
                    SimilarTastes = table.Column<string>(nullable: false),
                    Strength = table.Column<int>(nullable: false),
                    Image = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TodaysSpecials", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Recipes");

            migrationBuilder.DropTable(
                name: "TodaysSpecials");
        }
    }
}
