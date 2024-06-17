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
                .HasMany(d => d.StatusHistory)
                .WithOne(s => s.Delivery)
                .HasForeignKey(s => s.DeliveryId)
                .OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<Delivery>()
		        .Property(d => d.CurrentStatus)
		        .HasColumnName("CurrentStatus");

			modelBuilder.Entity<DeliveryStatusUpdate>()
                .HasOne(dsu => dsu.Delivery)
                .WithMany(d => d.StatusHistory)
                .HasForeignKey(dsu => dsu.DeliveryId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Role>()
                .HasMany(r => r.Privileges)
                .WithMany(p => p.Roles)
                .UsingEntity<Dictionary<string, object>>(
                    "RolePrivilege",
                    j => j.HasOne<Privilege>().WithMany().HasForeignKey("PrivilegeId"),
                    j => j.HasOne<Role>().WithMany().HasForeignKey("RoleId"));

            modelBuilder.Entity<Privilege>().HasData(
                new Privilege { Id = 1 ,Name = "CanDisplayDeliveries" },
                new Privilege { Id = 2 ,Name = "CanRegisterDelivery" },
                new Privilege { Id = 3 ,Name = "CanChangeDeliveryStatus" },
                new Privilege { Id = 4 ,Name = "CanDisplayRoles" },
                new Privilege { Id = 5 ,Name = "CanAddRole" },
                new Privilege { Id = 6 ,Name = "CanDisplayUsers" },
                new Privilege { Id = 7 ,Name = "CanModifyUser" }
            );

            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "Admin"},
                new Role { Id = 2, Name = "Default"}
            );

            modelBuilder.Entity("RolePrivilege").HasData(
                new { RoleId = 1, PrivilegeId = 1 },
                new { RoleId = 1, PrivilegeId = 2 },
                new { RoleId = 1, PrivilegeId = 3 },
                new { RoleId = 1, PrivilegeId = 4 },
                new { RoleId = 1, PrivilegeId = 5 },
                new { RoleId = 1, PrivilegeId = 6 },
                new { RoleId = 1, PrivilegeId = 7 },
                new { RoleId = 2, PrivilegeId = 1 }
            );

            modelBuilder.Entity<User>().HasData(
                new { Id = 1, Username = "admin", Password = "admin", RoleId = 1}
            );
        }
    }
}