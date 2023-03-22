using Microsoft.EntityFrameworkCore;
using WebApiCore.Models;

namespace WebApiApplication.Context;

public interface IWebApiDbContext
{
    public DbSet<HelpRequestEntity> HelpRequests { get; set; }
    public Task<int> SaveChangesAsync();
}