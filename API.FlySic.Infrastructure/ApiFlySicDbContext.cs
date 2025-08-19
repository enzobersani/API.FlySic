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
        public DbSet<RecoveryCode> RecoveryCodes { get; set; }
        public DbSet<FlightRating> FlightRatings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FlightForm>()
            .HasOne(f => f.User)
            .WithMany()
            .HasForeignKey(f => f.PilotId)
            .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
    }
}
