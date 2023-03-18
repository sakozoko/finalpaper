using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;
using WebApiCore.Models;

namespace WebApiApplication.Context
{
    public interface IWebApiDbContext
    {
        public DbSet<HelpRequestEntity> HelpRequests{get;set;}
        public Task<int> SaveChangesAsync();
    }
}