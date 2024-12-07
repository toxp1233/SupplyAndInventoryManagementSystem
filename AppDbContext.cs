
using ISMS.Models;
using Microsoft.EntityFrameworkCore;
using ISMS.Enums;

namespace ISMS.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Product> Products { get; set; }

        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=WIN-U0EH07T0KTT\RAXACA;Initial Catalog=SupplyAnInventoryManagementDb;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
             .HasIndex(u => u.UserName)
             .IsUnique();

            modelBuilder.Entity<User>()
             .HasIndex(u => u.Email)
             .IsUnique();

            modelBuilder.Entity<User>()
             .HasOne(u => u.Role)
             .WithMany(r => r.users)
             .HasForeignKey(u => u.RoleId);

            modelBuilder.Entity<Product>()
             .HasOne(p => p.Supplier)
             .WithMany(s => s.Products) 
             .HasForeignKey(p => p.SupplierId);


            modelBuilder.Entity<Role>()
             .HasData(
                new Role { Id = 1, UserRole = RoleType.Admin, description = "Administrator" },
                new Role { Id = 2, UserRole = RoleType.Manager, description = "Manager" },
                new Role { Id = 3, UserRole = RoleType.Viewer, description = "Viewer" }
             );


        }
    }
}
