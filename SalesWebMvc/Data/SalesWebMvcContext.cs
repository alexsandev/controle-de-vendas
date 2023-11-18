using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Models;

namespace SalesWebMvc.Data
{
    public class SalesWebMvcContext : DbContext
    {
        protected readonly IConfiguration Configuration;
        public SalesWebMvcContext (DbContextOptions<SalesWebMvcContext> options, IConfiguration configuration)
            : base(options)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = Configuration.GetConnectionString("SalesWebMvcContext");
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }

        public DbSet<Department> Department { get; set; } = default!;
        public DbSet<Seller> Seller { get; set; }
        public DbSet<SalesRecord> SalesRecord { get; set; }
    }
}
