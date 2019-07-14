using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace VT.Data.Vehicle
{
    public class VehicleContext : IdentityDbContext<User, IdentityRole<Guid>, Guid, IdentityUserClaim<Guid>, IdentityUserRole<Guid>, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
    {
        public VehicleContext(DbContextOptions<VehicleContext> options)
            : base(options)
        { }

        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<SystemLog> SystemLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(b =>
            {
                b.ToTable("Users");
            });

            modelBuilder.Entity<IdentityUserClaim<Guid>>(b =>
            {
                b.ToTable("VTUserClaims");
            });

            modelBuilder.Entity<IdentityUserLogin<Guid>>(b =>
            {
                b.ToTable("VTUserLogins");
            });

            modelBuilder.Entity<IdentityUserToken<Guid>>(b =>
            {
                b.ToTable("VTUserTokens");
            });

            modelBuilder.Entity<IdentityRole<Guid>>(b =>
            {
                b.ToTable("VTRoles");
            });

            modelBuilder.Entity<IdentityRoleClaim<Guid>>(b =>
            {
                b.ToTable("VTRoleClaims");
            });

            modelBuilder.Entity<IdentityUserRole<Guid>>(b =>
            {
                b.ToTable("VTUserRoles");
            });
        }
    }
}
