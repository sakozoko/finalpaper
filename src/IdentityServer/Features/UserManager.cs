﻿using System.Security.Claims;
using IdentityModel;
using IdentityServer.Entities;
using IdentityServer4.Test;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using static System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler;

namespace IdentityServer.Features;

public class UserManager : UserManager<User>
{
    public UserManager(IUserStore<User> store,
                       IOptions<IdentityOptions> optionsAccessor,
                       IPasswordHasher<User> passwordHasher,
                       IEnumerable<IUserValidator<User>> userValidators,
                       IEnumerable<IPasswordValidator<User>> passwordValidators,
                       ILookupNormalizer keyNormalizer,
                       IdentityErrorDescriber errors,
                       IServiceProvider services,
                       ILogger<UserManager> logger) : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger){}
    public async Task<User?> FindByExternalProvider(string provider, string userId)
    {
        return await Users.FirstOrDefaultAsync(c=>c.ProviderName==provider && c.ProviderSubjectId==userId);
    }
    public async Task<User> AutoProvisionUser(string provider, string userId, List<Claim> claims)
    {
        // create a list of claims that we want to transfer into our store
            var filtered = new List<Claim>();

            foreach (var claim in claims)
            {
                // if the external system sends a display name - translate that to the standard OIDC name claim
                if (claim.Type == ClaimTypes.Name)
                {
                    filtered.Add(new Claim(JwtClaimTypes.Name, claim.Value));
                }
                // if the JWT handler has an outbound mapping to an OIDC claim use that
                else if (DefaultOutboundClaimTypeMap.TryGetValue(claim.Type, out var value))
                {
                    filtered.Add(new Claim(value, claim.Value));
                }
                // copy the claim as-is
                else
                {
                    filtered.Add(claim);
                }
            }

            // if no display name was provided, try to construct by first and/or last name
            if (filtered.All(x => x.Type != JwtClaimTypes.Name))
            {
                var first = filtered.FirstOrDefault(x => x.Type == JwtClaimTypes.GivenName)?.Value;
                var last = filtered.FirstOrDefault(x => x.Type == JwtClaimTypes.FamilyName)?.Value;
                if (first != null && last != null)
                {
                    filtered.Add(new Claim(JwtClaimTypes.Name, first + " " + last));
                }
                else if (first != null)
                {
                    filtered.Add(new Claim(JwtClaimTypes.Name, first));
                }
                else if (last != null)
                {
                    filtered.Add(new Claim(JwtClaimTypes.Name, last));
                }
            }
            
            // check if a display name is available, otherwise fallback to subject id
            var name = $"{Guid.NewGuid():N}";
            var email = filtered.FirstOrDefault(c=>c.Type==JwtClaimTypes.Email)?.Value;
            //check if user already exists
            var user = await Users.FirstOrDefaultAsync(c=>c.Email==email);
            if (user is not null)
            {
                user.ProviderName = provider;
                user.ProviderSubjectId = userId;
                await UpdateAsync(user);
                return user;
            }

            // create new user
            user = new User
            {
                NormalizedUserName = KeyNormalizer.NormalizeName(name),
                UserName = name,
                NormalizedEmail = KeyNormalizer.NormalizeEmail(email),
                Email = email,
                ProviderName = provider.ToUpper(),
                ProviderSubjectId = userId,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            await Store.CreateAsync(user, CancellationToken.None);
            return user;
        }


    public override async Task<IdentityResult> CreateAsync(User user)
    {
        var result = await base.CreateAsync(user);
        if (!result.Succeeded)
            return result;
        var role = Roles.User;
        if(user.UserName == "admin")
            role = Roles.Admin;
        return await AddToRoleAsync(user, role);
    }
}