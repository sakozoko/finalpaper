using IdentityServer.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.Persistence;

public class IdentityServerContext : IdentityDbContext<User, Role, Guid>
{
    public IdentityServerContext(DbContextOptions<IdentityServerContext> options) : base(options)
    {
    }

    public DbSet<VerificationSmsCode> VerifySmsCodes { get; set; } = default!;
}