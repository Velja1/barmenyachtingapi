using BarmenYachting.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarmenYachting.DataAccess
{
    public class BarmenYachtingDbContext : DbContext
    {

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-C3VBCB6;Initial Catalog=barmen;Integrated Security=True");
        }

        public DbSet<Manufacter> Manufacters { get; set; }
        public DbSet<Poll> Polls { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Domain.Type> Types { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Vessel> Vessels { get; set; }
        public DbSet<Log> Logs { get; set; }
    }
}
