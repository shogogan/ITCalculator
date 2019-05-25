using Microsoft.EntityFrameworkCore;
using Repository.Entities;

namespace Repository.Config
{
    public class DbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbSet<PersonEntity> People { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("Server=localhost;User Id=root;Password=admin;Database=itcalculator");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}