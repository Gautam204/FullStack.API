using FullStack.API.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace FullStack.API.Data
{
    public class FullStackDbContext : DbContext
    {
        public FullStackDbContext(DbContextOptions options) : base(options)
        {
        }

        //DbSet
        public DbSet<UserPost> UserPosts { get; set; }

        public DbSet<SubscriptionPlanLimit> SubscriptionPlanLimits { get; set; }
    }
}
