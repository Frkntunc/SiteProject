using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Site.Domain.Authentication;
using Site.Domain.Entities;

namespace Site.Infrastructure.Contracts.Persistence
{
    public class AppDbContext : IdentityDbContext<User, Role, int>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Apartment> Apartments { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<BillPayment> BillPayments { get; set; }
        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>().HasIndex(u => u.Email).IsUnique();
            builder.Entity<Message>().Property(m => m.Read).HasDefaultValue(false);
            builder.Entity<Bill>().Property(b => b.TotalDept).HasComputedColumnSql("(Electric + Water + NaturalGas + Dues)");
            base.OnModelCreating(builder);
        }
    }
}
