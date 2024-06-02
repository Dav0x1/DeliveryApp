using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryApp.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Privilege> Privileges { get; set; }

        public DbSet<Delivery> Deliveries { get; set; }
        public DbSet<DeliveryStatusUpdate> DeliveryStatusUpdates { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlite("Data Source=products.db");
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Delivery>()
                .HasOne(d => d.CurrentStatus)
                .WithMany()
                .HasForeignKey(d => d.CurrentStatusId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Delivery>()
                .HasMany(d => d.StatusHistory)
                .WithOne(s => s.Delivery)
                .HasForeignKey(s => s.DeliveryId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<DeliveryStatusUpdate>()
                .HasOne(dsu => dsu.Delivery)
                .WithMany(d => d.StatusHistory)
                .HasForeignKey(dsu => dsu.DeliveryId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Privilege>().HasData(
                new Privilege { Id = 1 ,Name = "CanDisplayDeliveries" },
                new Privilege { Id = 2 ,Name = "CanRegisterDelivery" },
                new Privilege { Id = 3 ,Name = "CanDisplayRoles" },
                new Privilege { Id = 4 ,Name = "CanAddRole" },
                new Privilege { Id = 5 ,Name = "CanDisplayUsers" },
                new Privilege { Id = 6 ,Name = "CanModifyUser" }
            );
        }
    }
}