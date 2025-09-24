using destek_kayit_sistemi.destek_kayit_sistemi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace destek_kayit_sistemi.destek_kayit_sistemi.Infrastructure.Data;


    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Users> Users { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Tickets> Tickets { get; set; }
        public DbSet<Devices> Devices { get; set; }
        public DbSet<User_Devices> User_Devices { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Roles>().HasData(
            new Roles { RoleId = 1, RoleName = "Admin" },
            new Roles { RoleId = 2, RoleName = "Personel" },
            new Roles { RoleId = 3, RoleName = "Kullanıcı" }
            );

            modelBuilder.Entity<Users>().HasData(
            new Users { Id = 1, Username = "abdullah", Password = "123", RoleId = 1 }
        );


            modelBuilder.Entity<Users>()
                .HasOne(u => u.Roles)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId);


            modelBuilder.Entity<Tickets>()
                .HasOne(t => t.Users)
                .WithMany(u => u.CreatedTickets)
                .HasForeignKey(t => t.user_id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Tickets>()
                .HasOne(t => t.AssignedToUser)
                .WithMany(u => u.AssignedTickets)
                .HasForeignKey(t => t.assigned_to_user_id)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Devices>()
                .HasKey(d => d.DeviceId);


            modelBuilder.Entity<User_Devices>()
                .HasKey(ud => new { ud.UserId, ud.DeviceId });

            modelBuilder.Entity<User_Devices>()
                .HasOne(ud => ud.Users)
                .WithMany(u => u.User_Devices)
                .HasForeignKey(ud => ud.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User_Devices>()
                .HasOne(ud => ud.Devices)
                .WithMany(d => d.User_Devices)
                .HasForeignKey(ud => ud.DeviceId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Devices>()
                .Property(d => d.CreatedAt)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Devices>()
                .Property(d => d.UpdatedAt)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<User_Devices>()
                .Property(ud => ud.AssignedAt)
                .HasDefaultValueSql("GETDATE()");
        }


    }

