using Microsoft.EntityFrameworkCore;
using WebApiApplication.Context;
using WebApiCore.Models;

namespace WebApiInfrastructure.Context
{
    public class WebApiDbContext : DbContext, IWebApiDbContext
    {
        public  DbSet<HelpRequestEntity> HelpRequests {get;set;} = default!;
        public WebApiDbContext(DbContextOptions<WebApiDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HelpRequestEntity>()
                .Property(x => x.Status)
                .HasConversion(
                    v => v.ToString(),
                    v => (HelpRequestStatus)Enum.Parse(typeof(HelpRequestStatus), v));
        }

        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }
    }
}