using DoorKicker.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DoorKicker.Repositories
{
    public class DoorDbContext: IdentityDbContext<User>
    {
        public DbSet<Door> Doors { get; set; }

        public DbSet<Property> Properties { get; set; }

        public DbSet<PropertyUser> PropertyUser { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=./doors.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .Entity<User>()
                .HasMany<PropertyUser>()
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserId);

            modelBuilder
                .Entity<Property>()
                .HasMany<Door>()
                .WithOne(d => d.Property)
                .HasForeignKey(d => d.PropertyId);

            modelBuilder
                .Entity<Property>()
                .HasMany<PropertyUser>()
                .WithOne(p => p.Property)
                .HasForeignKey(p => p.PropertyId);

            modelBuilder
                .Entity<PropertyUser>()
                .HasOne<Property>()
                .WithMany(p => p.PropertyUsers)
                .HasForeignKey(p => p.PropertyId);

            modelBuilder
                .Entity<PropertyUser>()
                .HasOne<User>()
                .WithMany(p => p.UserProperties)
                .HasForeignKey(p => p.UserId);

            modelBuilder
               .Entity<Door>()
               .HasOne<Property>()
               .WithMany(p => p.Doors)
               .HasForeignKey(p => p.PropertyId);

            modelBuilder
                .Entity<Door>()
                .HasMany<Event>()
                .WithOne(e => e.Door)
                .HasForeignKey(e => e.DoorId);

            modelBuilder
                .Entity<Event>()
                .HasOne<Door>()
                .WithMany(d => d.Events)
                .HasForeignKey(e => e.DoorId);
        }
    }
}
