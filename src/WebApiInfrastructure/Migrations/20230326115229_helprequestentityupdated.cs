using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class helprequestentityupdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Answer",
                table: "HelpRequests",
                type: "nvarchar(max)",
                maxLength: 5000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserEmail",
                table: "HelpRequests",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "HelpRequests",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Answer",
                table: "HelpRequests");

            migrationBuilder.DropColumn(
                name: "UserEmail",
                table: "HelpRequests");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "HelpRequests");
        }
    }
}
