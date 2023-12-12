using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Models;

namespace SalesWebMvc.Data
{
    public class SalesWebMvcContext : IdentityDbContext<IdentityUser>
    {
        protected readonly IConfiguration Configuration;
        public SalesWebMvcContext(DbContextOptions<SalesWebMvcContext> options, IConfiguration configuration) : base(options)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //var connectionString = Configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING");
            //optionsBuilder.UseSqlServer(connectionString);

            var connectionString = Configuration.GetConnectionString("MYSQL_CONNECTIONSTRING");
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Seller>()
                .HasMany(e => e.Sales)
                .WithOne(e => e.Seller)
                .HasForeignKey(e => e.SellerId)
                .HasPrincipalKey(e => e.Id)
                .OnDelete(DeleteBehavior.Restrict);
        }

        public DbSet<Department> Department { get; set; } = default!;
        public DbSet<Seller> Seller { get; set; }
        public DbSet<SalesRecord> SalesRecord { get; set; }
    }
}
