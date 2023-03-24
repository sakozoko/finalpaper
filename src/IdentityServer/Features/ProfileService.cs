using System.Security.Claims;
using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using IdentityServer.Entities;
using Microsoft.AspNetCore.Identity;

namespace IdentityServer.Features;

public class ProfileService : IProfileService
{
    private readonly ILogger<ProfileService> _logger;
    private readonly IUserClaimsPrincipalFactory<User> _userClaimsPrincipalFactory;
    private readonly UserManager<User> _userManager;

    public ProfileService(
        ILogger<ProfileService> logger,
        IUserClaimsPrincipalFactory<User> userClaimsPrincipalFactory,
        UserManager<User> userManager
    )
    {
        _logger = logger;
        _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
        _userManager = userManager;
    }

    public async Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
        var subjectId = context.Subject.GetSubjectId();
        var user = await FindUserBySubjectIdAsync(subjectId);
        var claimsPrincipal = await _userClaimsPrincipalFactory.CreateAsync(user!);
        context.AddRequestedClaims(claimsPrincipal.Claims);
        //adding custom properties
        context.AddRequestedClaims(GetUserCustomClaims(user));
    }

    public async Task IsActiveAsync(IsActiveContext context)
    {
        var subjectId = context.Subject.GetSubjectId();
        var applicationUser = await _userManager.FindByIdAsync(subjectId);
        context.IsActive = applicationUser != null;
    }

    private static IEnumerable<Claim> GetUserCustomClaims(User? user)
    {
        const int maxCustomClaims = 1;
        var claims = new List<Claim>(maxCustomClaims);
        if(user is null) return claims;
        claims.Add(new Claim("username", user.UserName!));
        return claims;
    }

    private async Task<User?> FindUserBySubjectIdAsync(string subjectId)
    {
        var user = await _userManager.FindByIdAsync(subjectId);
        if (user == null) _logger.LogWarning("No user found matching subject ID: {SubjectId}", subjectId);

        return user;
    }
}