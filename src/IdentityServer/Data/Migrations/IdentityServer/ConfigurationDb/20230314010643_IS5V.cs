#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace IdentityServer.Data.Migrations.IdentityServer.ConfigurationDb;

/// <inheritdoc />
public partial class IS5V : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropIndex(
            "IX_IdentityResourceProperties_IdentityResourceId",
            "IdentityResourceProperties");

        migrationBuilder.DropIndex(
            "IX_IdentityResourceClaims_IdentityResourceId",
            "IdentityResourceClaims");

        migrationBuilder.DropIndex(
            "IX_ClientScopes_ClientId",
            "ClientScopes");

        migrationBuilder.DropIndex(
            "IX_ClientRedirectUris_ClientId",
            "ClientRedirectUris");

        migrationBuilder.DropIndex(
            "IX_ClientProperties_ClientId",
            "ClientProperties");

        migrationBuilder.DropIndex(
            "IX_ClientPostLogoutRedirectUris_ClientId",
            "ClientPostLogoutRedirectUris");

        migrationBuilder.DropIndex(
            "IX_ClientIdPRestrictions_ClientId",
            "ClientIdPRestrictions");

        migrationBuilder.DropIndex(
            "IX_ClientGrantTypes_ClientId",
            "ClientGrantTypes");

        migrationBuilder.DropIndex(
            "IX_ClientCorsOrigins_ClientId",
            "ClientCorsOrigins");

        migrationBuilder.DropIndex(
            "IX_ClientClaims_ClientId",
            "ClientClaims");

        migrationBuilder.DropIndex(
            "IX_ApiScopeProperties_ScopeId",
            "ApiScopeProperties");

        migrationBuilder.DropIndex(
            "IX_ApiScopeClaims_ScopeId",
            "ApiScopeClaims");

        migrationBuilder.DropIndex(
            "IX_ApiResourceScopes_ApiResourceId",
            "ApiResourceScopes");

        migrationBuilder.DropIndex(
            "IX_ApiResourceProperties_ApiResourceId",
            "ApiResourceProperties");

        migrationBuilder.DropIndex(
            "IX_ApiResourceClaims_ApiResourceId",
            "ApiResourceClaims");

        migrationBuilder.AddColumn<int>(
            "CibaLifetime",
            "Clients",
            "int",
            nullable: true);

        migrationBuilder.AddColumn<bool>(
            "CoordinateLifetimeWithUserSession",
            "Clients",
            "bit",
            nullable: true);

        migrationBuilder.AddColumn<int>(
            "PollingInterval",
            "Clients",
            "int",
            nullable: true);

        migrationBuilder.AlterColumn<string>(
            "RedirectUri",
            "ClientRedirectUris",
            "nvarchar(400)",
            maxLength: 400,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "nvarchar(2000)",
            oldMaxLength: 2000);

        migrationBuilder.AlterColumn<string>(
            "PostLogoutRedirectUri",
            "ClientPostLogoutRedirectUris",
            "nvarchar(400)",
            maxLength: 400,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "nvarchar(2000)",
            oldMaxLength: 2000);

        migrationBuilder.AddColumn<DateTime>(
            "Created",
            "ApiScopes",
            "datetime2",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

        migrationBuilder.AddColumn<DateTime>(
            "LastAccessed",
            "ApiScopes",
            "datetime2",
            nullable: true);

        migrationBuilder.AddColumn<bool>(
            "NonEditable",
            "ApiScopes",
            "bit",
            nullable: false,
            defaultValue: false);

        migrationBuilder.AddColumn<DateTime>(
            "Updated",
            "ApiScopes",
            "datetime2",
            nullable: true);

        migrationBuilder.AddColumn<bool>(
            "RequireResourceIndicator",
            "ApiResources",
            "bit",
            nullable: false,
            defaultValue: false);

        migrationBuilder.CreateTable(
            "IdentityProviders",
            table => new
            {
                Id = table.Column<int>("int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Scheme = table.Column<string>("nvarchar(200)", maxLength: 200, nullable: false),
                DisplayName = table.Column<string>("nvarchar(200)", maxLength: 200, nullable: true),
                Enabled = table.Column<bool>("bit", nullable: false),
                Type = table.Column<string>("nvarchar(20)", maxLength: 20, nullable: false),
                Properties = table.Column<string>("nvarchar(max)", nullable: true),
                Created = table.Column<DateTime>("datetime2", nullable: false),
                Updated = table.Column<DateTime>("datetime2", nullable: true),
                LastAccessed = table.Column<DateTime>("datetime2", nullable: true),
                NonEditable = table.Column<bool>("bit", nullable: false)
            },
            constraints: table => { table.PrimaryKey("PK_IdentityProviders", x => x.Id); });

        migrationBuilder.CreateIndex(
            "IX_IdentityResourceProperties_IdentityResourceId_Key",
            "IdentityResourceProperties",
            new[] { "IdentityResourceId", "Key" },
            unique: true);

        migrationBuilder.CreateIndex(
            "IX_IdentityResourceClaims_IdentityResourceId_Type",
            "IdentityResourceClaims",
            new[] { "IdentityResourceId", "Type" },
            unique: true);

        migrationBuilder.CreateIndex(
            "IX_ClientScopes_ClientId_Scope",
            "ClientScopes",
            new[] { "ClientId", "Scope" },
            unique: true);

        migrationBuilder.CreateIndex(
            "IX_ClientRedirectUris_ClientId_RedirectUri",
            "ClientRedirectUris",
            new[] { "ClientId", "RedirectUri" },
            unique: true);

        migrationBuilder.CreateIndex(
            "IX_ClientProperties_ClientId_Key",
            "ClientProperties",
            new[] { "ClientId", "Key" },
            unique: true);

        migrationBuilder.CreateIndex(
            "IX_ClientPostLogoutRedirectUris_ClientId_PostLogoutRedirectUri",
            "ClientPostLogoutRedirectUris",
            new[] { "ClientId", "PostLogoutRedirectUri" },
            unique: true);

        migrationBuilder.CreateIndex(
            "IX_ClientIdPRestrictions_ClientId_Provider",
            "ClientIdPRestrictions",
            new[] { "ClientId", "Provider" },
            unique: true);

        migrationBuilder.CreateIndex(
            "IX_ClientGrantTypes_ClientId_GrantType",
            "ClientGrantTypes",
            new[] { "ClientId", "GrantType" },
            unique: true);

        migrationBuilder.CreateIndex(
            "IX_ClientCorsOrigins_ClientId_Origin",
            "ClientCorsOrigins",
            new[] { "ClientId", "Origin" },
            unique: true);

        migrationBuilder.CreateIndex(
            "IX_ClientClaims_ClientId_Type_Value",
            "ClientClaims",
            new[] { "ClientId", "Type", "Value" },
            unique: true);

        migrationBuilder.CreateIndex(
            "IX_ApiScopeProperties_ScopeId_Key",
            "ApiScopeProperties",
            new[] { "ScopeId", "Key" },
            unique: true);

        migrationBuilder.CreateIndex(
            "IX_ApiScopeClaims_ScopeId_Type",
            "ApiScopeClaims",
            new[] { "ScopeId", "Type" },
            unique: true);

        migrationBuilder.CreateIndex(
            "IX_ApiResourceScopes_ApiResourceId_Scope",
            "ApiResourceScopes",
            new[] { "ApiResourceId", "Scope" },
            unique: true);

        migrationBuilder.CreateIndex(
            "IX_ApiResourceProperties_ApiResourceId_Key",
            "ApiResourceProperties",
            new[] { "ApiResourceId", "Key" },
            unique: true);

        migrationBuilder.CreateIndex(
            "IX_ApiResourceClaims_ApiResourceId_Type",
            "ApiResourceClaims",
            new[] { "ApiResourceId", "Type" },
            unique: true);

        migrationBuilder.CreateIndex(
            "IX_IdentityProviders_Scheme",
            "IdentityProviders",
            "Scheme",
            unique: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            "IdentityProviders");

        migrationBuilder.DropIndex(
            "IX_IdentityResourceProperties_IdentityResourceId_Key",
            "IdentityResourceProperties");

        migrationBuilder.DropIndex(
            "IX_IdentityResourceClaims_IdentityResourceId_Type",
            "IdentityResourceClaims");

        migrationBuilder.DropIndex(
            "IX_ClientScopes_ClientId_Scope",
            "ClientScopes");

        migrationBuilder.DropIndex(
            "IX_ClientRedirectUris_ClientId_RedirectUri",
            "ClientRedirectUris");

        migrationBuilder.DropIndex(
            "IX_ClientProperties_ClientId_Key",
            "ClientProperties");

        migrationBuilder.DropIndex(
            "IX_ClientPostLogoutRedirectUris_ClientId_PostLogoutRedirectUri",
            "ClientPostLogoutRedirectUris");

        migrationBuilder.DropIndex(
            "IX_ClientIdPRestrictions_ClientId_Provider",
            "ClientIdPRestrictions");

        migrationBuilder.DropIndex(
            "IX_ClientGrantTypes_ClientId_GrantType",
            "ClientGrantTypes");

        migrationBuilder.DropIndex(
            "IX_ClientCorsOrigins_ClientId_Origin",
            "ClientCorsOrigins");

        migrationBuilder.DropIndex(
            "IX_ClientClaims_ClientId_Type_Value",
            "ClientClaims");

        migrationBuilder.DropIndex(
            "IX_ApiScopeProperties_ScopeId_Key",
            "ApiScopeProperties");

        migrationBuilder.DropIndex(
            "IX_ApiScopeClaims_ScopeId_Type",
            "ApiScopeClaims");

        migrationBuilder.DropIndex(
            "IX_ApiResourceScopes_ApiResourceId_Scope",
            "ApiResourceScopes");

        migrationBuilder.DropIndex(
            "IX_ApiResourceProperties_ApiResourceId_Key",
            "ApiResourceProperties");

        migrationBuilder.DropIndex(
            "IX_ApiResourceClaims_ApiResourceId_Type",
            "ApiResourceClaims");

        migrationBuilder.DropColumn(
            "CibaLifetime",
            "Clients");

        migrationBuilder.DropColumn(
            "CoordinateLifetimeWithUserSession",
            "Clients");

        migrationBuilder.DropColumn(
            "PollingInterval",
            "Clients");

        migrationBuilder.DropColumn(
            "Created",
            "ApiScopes");

        migrationBuilder.DropColumn(
            "LastAccessed",
            "ApiScopes");

        migrationBuilder.DropColumn(
            "NonEditable",
            "ApiScopes");

        migrationBuilder.DropColumn(
            "Updated",
            "ApiScopes");

        migrationBuilder.DropColumn(
            "RequireResourceIndicator",
            "ApiResources");

        migrationBuilder.AlterColumn<string>(
            "RedirectUri",
            "ClientRedirectUris",
            "nvarchar(2000)",
            maxLength: 2000,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "nvarchar(400)",
            oldMaxLength: 400);

        migrationBuilder.AlterColumn<string>(
            "PostLogoutRedirectUri",
            "ClientPostLogoutRedirectUris",
            "nvarchar(2000)",
            maxLength: 2000,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "nvarchar(400)",
            oldMaxLength: 400);

        migrationBuilder.CreateIndex(
            "IX_IdentityResourceProperties_IdentityResourceId",
            "IdentityResourceProperties",
            "IdentityResourceId");

        migrationBuilder.CreateIndex(
            "IX_IdentityResourceClaims_IdentityResourceId",
            "IdentityResourceClaims",
            "IdentityResourceId");

        migrationBuilder.CreateIndex(
            "IX_ClientScopes_ClientId",
            "ClientScopes",
            "ClientId");

        migrationBuilder.CreateIndex(
            "IX_ClientRedirectUris_ClientId",
            "ClientRedirectUris",
            "ClientId");

        migrationBuilder.CreateIndex(
            "IX_ClientProperties_ClientId",
            "ClientProperties",
            "ClientId");

        migrationBuilder.CreateIndex(
            "IX_ClientPostLogoutRedirectUris_ClientId",
            "ClientPostLogoutRedirectUris",
            "ClientId");

        migrationBuilder.CreateIndex(
            "IX_ClientIdPRestrictions_ClientId",
            "ClientIdPRestrictions",
            "ClientId");

        migrationBuilder.CreateIndex(
            "IX_ClientGrantTypes_ClientId",
            "ClientGrantTypes",
            "ClientId");

        migrationBuilder.CreateIndex(
            "IX_ClientCorsOrigins_ClientId",
            "ClientCorsOrigins",
            "ClientId");

        migrationBuilder.CreateIndex(
            "IX_ClientClaims_ClientId",
            "ClientClaims",
            "ClientId");

        migrationBuilder.CreateIndex(
            "IX_ApiScopeProperties_ScopeId",
            "ApiScopeProperties",
            "ScopeId");

        migrationBuilder.CreateIndex(
            "IX_ApiScopeClaims_ScopeId",
            "ApiScopeClaims",
            "ScopeId");

        migrationBuilder.CreateIndex(
            "IX_ApiResourceScopes_ApiResourceId",
            "ApiResourceScopes",
            "ApiResourceId");

        migrationBuilder.CreateIndex(
            "IX_ApiResourceProperties_ApiResourceId",
            "ApiResourceProperties",
            "ApiResourceId");

        migrationBuilder.CreateIndex(
            "IX_ApiResourceClaims_ApiResourceId",
            "ApiResourceClaims",
            "ApiResourceId");
    }
}