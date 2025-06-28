using API.FlySic.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.FlySic.Infrastructure
{
    public class ApiFlySicDbContext : DbContext
    {
        public ApiFlySicDbContext(DbContextOptions<ApiFlySicDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<FlightForm> FlightForms { get; set; }
        public DbSet<FlightFormInterest> FlightFormInterests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FlightForm>()
            .HasOne(f => f.User)
            .WithMany()
            .HasForeignKey(f => f.UserId)
            .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
    }
}
