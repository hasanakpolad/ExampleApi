using ExampleApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ExampleApi.Context
{
    public class MasterContext : DbContext
    {
        public MasterContext()
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Todo> Todos { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("Server=localhost;Database=root;Uid=root;Pwd=root;");
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(e => e.Id);
            modelBuilder.Entity<Product>().HasKey(e => e.Id);
            modelBuilder.Entity<Todo>().HasKey(e => e.Id);
            base.OnModelCreating(modelBuilder);
        }
    }
}
