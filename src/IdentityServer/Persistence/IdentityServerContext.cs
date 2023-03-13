using IdentityServer.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.Persistence;

public class IdentityServerContext : IdentityDbContext<User, Role, Guid>
{
    public DbSet<VerificationSmsCode> VerifySmsCodes { get; set; }
    public IdentityServerContext(DbContextOptions<IdentityServerContext> options) : base(options)
    {
    }
}