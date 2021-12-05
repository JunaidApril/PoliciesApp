using Microsoft.EntityFrameworkCore;
using Wtw.Policies.Domain.Models;
using Wtw.Policies.Infrastructure.Data.EntityTypeConfigurations;

namespace Wtw.Policies.Infrastructure.Data
{
    public class PoliciesContext : DbContext
    {
        public DbSet<PolicyHolder> PolicyHolders { get; set; }

        public DbSet<Policy> Policies { get; set; }

        public PoliciesContext(DbContextOptions<PoliciesContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PolicyHolderConfiguration());
            modelBuilder.ApplyConfiguration(new PolicyConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Data Source=(local);Initial Catalog=Wtw.Policies;Integrated Security=SSPI;");
        //}
    }
}
