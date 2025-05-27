using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
namespace TMS.DATA.Model
{
    public class TMSDbContext : DbContext, IDisposable
    {
        public TMSDbContext(DbContextOptions<TMSDbContext> options) : base(options)
        {
        }
        public virtual DbSet<EmailMessage> Emails { get; set; }
        public TMSDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("DbConnection");
                base.OnConfiguring(optionsBuilder);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            object value = modelBuilder.HasDefaultSchema("TMS");
            modelBuilder.Entity<EmailMessage>().HasKey(am => new
            {
                am.EMAILId
            });

            modelBuilder.Entity<EmailMessage>().Property(l => l.EMAILId).ValueGeneratedOnAdd();

            base.OnModelCreating(modelBuilder);
        }
        
       




    }
}
