using Contacts.Models;
using Microsoft.EntityFrameworkCore;

namespace Contacts.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Contact> Contacts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>().HasIndex(c => c.Phone).IsUnique();
            modelBuilder.Entity<Contact>().HasIndex(c => c.Email).IsUnique();

            // modelBuilder.Entity<User>().HasData(
            //     new User
            //     {
            //         Id = "1",
            //         Name = "William",
            //         Phone = "Shakespeare"
            //     }
            // );

            // modelBuilder.Entity<Contact>().HasData(
            //     new Contact { Id = "1", Name = "Mostafa", Phone = "+201113653866" }
            // );
        }
    }
}