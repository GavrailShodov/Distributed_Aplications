using Microsoft.EntityFrameworkCore;
using BikesApi.Entities;

namespace BikesApi.Database
{
    public class DBBikesContext: DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Order> Orders { get; set; }
  
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-BFI7K36\\SQLEXPRESS;Database=BikesDB;Trusted_Connection=True;TrustServerCertificate=True");
        }
    }
}
