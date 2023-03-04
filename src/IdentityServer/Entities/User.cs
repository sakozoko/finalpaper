using Microsoft.AspNetCore.Identity;

namespace IdentityServer.Entities;

public class User : IdentityUser<Guid>
{
    public string? ProviderName { get; set; }
    public string? ProviderSubjectId { get; set; }

}