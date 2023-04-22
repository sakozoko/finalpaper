using System.Security.Claims;

namespace WebApi.Features;

public static class ClaimHelper
{
    public static Guid? GetGuidUserId(this IEnumerable<Claim> claims)
    {
        var userId = claims.GetClaimValue(ClaimTypes.NameIdentifier);
        return userId is null ? null : Guid.Parse(userId);
    }

    public static bool? GetEmailConfirmed(this IEnumerable<Claim> claims)
    {
        var emailConfirmed = claims.GetClaimValue("email_verified");
        if (emailConfirmed == null) throw new ArgumentException("Email confirmed claim is null");
        return bool.Parse(emailConfirmed);
    }

    public static string? GetUserName(this IEnumerable<Claim> claims)
    {
        return claims.GetClaimValue("name");
    }

    public static string? GetUserEmail(this IEnumerable<Claim> claims)
    {
        return claims.GetClaimValue(ClaimTypes.Email);
    }

    public static string? GetClaimValue(this IEnumerable<Claim> claims, string claimType)
    {
        return claims.FirstOrDefault(x => x.Type == claimType)?.Value;
    }
}