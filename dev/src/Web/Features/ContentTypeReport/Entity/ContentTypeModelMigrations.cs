using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Perficient.Web.Features.ContentTypeReport.Entity
{
    public partial class ContentTypeModelMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContentUsageDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContentID = table.Column<int>(type: "int", nullable: false),
                    UseWhen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DonotUseWhen = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentUsageDetail", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContentUsageImage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContentID = table.Column<int>(type: "int", nullable: false),
                    UsageImage = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentUsageImage", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContentUsageDetail");

            migrationBuilder.DropTable(
                name: "ContentUsageImage");
        }
    }
}
