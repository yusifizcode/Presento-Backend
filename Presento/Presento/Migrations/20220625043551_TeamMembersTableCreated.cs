using Microsoft.EntityFrameworkCore.Migrations;

namespace Presento.Migrations
{
    public partial class TeamMembersTableCreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TeamMembers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(maxLength: 25, nullable: false),
                    Profession = table.Column<string>(maxLength: 25, nullable: false),
                    Twitter = table.Column<string>(maxLength: 100, nullable: false),
                    Facebook = table.Column<string>(maxLength: 100, nullable: false),
                    Instagram = table.Column<string>(maxLength: 100, nullable: false),
                    Linkedin = table.Column<string>(maxLength: 100, nullable: false),
                    Image = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamMembers", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TeamMembers");
        }
    }
}
