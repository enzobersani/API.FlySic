using API.FlySic.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.FlySic.Infrastructure
{
    public class ApiFlySicDbContext : DbContext
    {
        public ApiFlySicDbContext(DbContextOptions<ApiFlySicDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
    }
}
