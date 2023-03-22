using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PublicNewEntityIsDeletedFlagAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PublishDate",
                table: "PublicNews",
                newName: "CreatedAt");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "PublicNews",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "PublicNews");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "PublicNews",
                newName: "PublishDate");
        }
    }
}
