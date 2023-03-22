#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace IdentityServer.Data.Migrations.IdentityServer.PersistedGrantDb;

/// <inheritdoc />
public partial class IS5VPG : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropPrimaryKey(
            "PK_PersistedGrants",
            "PersistedGrants");

        migrationBuilder.AlterColumn<string>(
            "Key",
            "PersistedGrants",
            "nvarchar(200)",
            maxLength: 200,
            nullable: true,
            oldClrType: typeof(string),
            oldType: "nvarchar(200)",
            oldMaxLength: 200);

        migrationBuilder.AddColumn<long>(
                "Id",
                "PersistedGrants",
                "bigint",
                nullable: false,
                defaultValue: 0L)
            .Annotation("SqlServer:Identity", "1, 1");

        migrationBuilder.AddPrimaryKey(
            "PK_PersistedGrants",
            "PersistedGrants",
            "Id");

        migrationBuilder.CreateTable(
            "Keys",
            table => new
            {
                Id = table.Column<string>("nvarchar(450)", nullable: false),
                Version = table.Column<int>("int", nullable: false),
                Created = table.Column<DateTime>("datetime2", nullable: false),
                Use = table.Column<string>("nvarchar(450)", nullable: true),
                Algorithm = table.Column<string>("nvarchar(100)", maxLength: 100, nullable: false),
                IsX509Certificate = table.Column<bool>("bit", nullable: false),
                DataProtected = table.Column<bool>("bit", nullable: false),
                Data = table.Column<string>("nvarchar(max)", nullable: false)
            },
            constraints: table => { table.PrimaryKey("PK_Keys", x => x.Id); });

        migrationBuilder.CreateTable(
            "ServerSideSessions",
            table => new
            {
                Id = table.Column<int>("int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Key = table.Column<string>("nvarchar(100)", maxLength: 100, nullable: false),
                Scheme = table.Column<string>("nvarchar(100)", maxLength: 100, nullable: false),
                SubjectId = table.Column<string>("nvarchar(100)", maxLength: 100, nullable: false),
                SessionId = table.Column<string>("nvarchar(100)", maxLength: 100, nullable: true),
                DisplayName = table.Column<string>("nvarchar(100)", maxLength: 100, nullable: true),
                Created = table.Column<DateTime>("datetime2", nullable: false),
                Renewed = table.Column<DateTime>("datetime2", nullable: false),
                Expires = table.Column<DateTime>("datetime2", nullable: true),
                Data = table.Column<string>("nvarchar(max)", nullable: false)
            },
            constraints: table => { table.PrimaryKey("PK_ServerSideSessions", x => x.Id); });

        migrationBuilder.CreateIndex(
            "IX_PersistedGrants_ConsumedTime",
            "PersistedGrants",
            "ConsumedTime");

        migrationBuilder.CreateIndex(
            "IX_PersistedGrants_Key",
            "PersistedGrants",
            "Key",
            unique: true,
            filter: "[Key] IS NOT NULL");

        migrationBuilder.CreateIndex(
            "IX_Keys_Use",
            "Keys",
            "Use");

        migrationBuilder.CreateIndex(
            "IX_ServerSideSessions_DisplayName",
            "ServerSideSessions",
            "DisplayName");

        migrationBuilder.CreateIndex(
            "IX_ServerSideSessions_Expires",
            "ServerSideSessions",
            "Expires");

        migrationBuilder.CreateIndex(
            "IX_ServerSideSessions_Key",
            "ServerSideSessions",
            "Key",
            unique: true);

        migrationBuilder.CreateIndex(
            "IX_ServerSideSessions_SessionId",
            "ServerSideSessions",
            "SessionId");

        migrationBuilder.CreateIndex(
            "IX_ServerSideSessions_SubjectId",
            "ServerSideSessions",
            "SubjectId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            "Keys");

        migrationBuilder.DropTable(
            "ServerSideSessions");

        migrationBuilder.DropPrimaryKey(
            "PK_PersistedGrants",
            "PersistedGrants");

        migrationBuilder.DropIndex(
            "IX_PersistedGrants_ConsumedTime",
            "PersistedGrants");

        migrationBuilder.DropIndex(
            "IX_PersistedGrants_Key",
            "PersistedGrants");

        migrationBuilder.DropColumn(
            "Id",
            "PersistedGrants");

        migrationBuilder.AlterColumn<string>(
            "Key",
            "PersistedGrants",
            "nvarchar(200)",
            maxLength: 200,
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "nvarchar(200)",
            oldMaxLength: 200,
            oldNullable: true);

        migrationBuilder.AddPrimaryKey(
            "PK_PersistedGrants",
            "PersistedGrants",
            "Key");
    }
}