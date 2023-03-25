using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel.Client;

namespace WebApi.Features
{
    public class UserClaimsHandler
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;

        public UserClaimsHandler(IConfiguration configuration)
        {
            _client = new HttpClient();
            _configuration = configuration;
        }
        public async Task<IEnumerable<Claim>> GetClaimsAsync(string accessToken){
            var result = await _client.GetUserInfoAsync(new UserInfoRequest
            {
                Address = _configuration["Authority"]+"/connect/userinfo",
                Token = accessToken.Replace("Bearer ", "")
            });
            if(result.IsError) throw new Exception(result.Error);
            return result.Claims;
        }
        public async Task<Claim?> GetClaimAsync(string accessToken, string claimType){
            var claims = await GetClaimsAsync(accessToken);
            return claims.FirstOrDefault(x => x.Type == claimType);
        }
        public async Task<bool> IsEmailConfirmed(string accessToken){
            var claim = await GetClaimAsync(accessToken, "email_verified");
            return claim != null && bool.Parse(claim.Value);
        }
    }
}