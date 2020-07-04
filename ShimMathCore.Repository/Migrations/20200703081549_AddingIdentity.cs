using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ShimMathCore.Repository.Migrations
{
    public partial class AddingIdentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user");

            migrationBuilder.DropTable(
                name: "userrole");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "userrole",
                columns: table => new
                {
                    UserRoleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserRoleTitle = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userrole", x => x.UserRoleID);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserEmail = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    UserName = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    UserPassword = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    UserRoleID = table.Column<int>(type: "int", nullable: false),
                    UserSalt = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.UserID);
                    table.ForeignKey(
                        name: "FK_user_userrole_UserRoleID",
                        column: x => x.UserRoleID,
                        principalTable: "userrole",
                        principalColumn: "UserRoleID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_user_UserRoleID",
                table: "user",
                column: "UserRoleID");
        }
    }
}
