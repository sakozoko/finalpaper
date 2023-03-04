using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdentityServer.Migrations
{
    /// <inheritdoc />
    public partial class UserModelUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProviderName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProviderSubjectId",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProviderName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ProviderSubjectId",
                table: "AspNetUsers");
        }
    }
}
