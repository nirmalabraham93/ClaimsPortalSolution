using ClaimsPortal.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimsPortal.Data.DBContext
{
    public class ClaimsPortalDbContext : DbContext
    {
        public ClaimsPortalDbContext(DbContextOptions<ClaimsPortalDbContext> options) : base(options)
        {
        }
        public DbSet<PolicyHolder> PolicyHolders { get; set; }
        public DbSet<Policy> Policies { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure relationships, if any
            modelBuilder.Entity<Policy>()
                .HasOne(p => p.PolicyHolder)
                .WithMany(ph => ph.Policies)
                .HasForeignKey(p => p.PolicyHolderId);

            modelBuilder.Entity<Vehicle>()
                .HasOne(v => v.Policy)
                .WithOne(p => p.Vehicle)
                .HasForeignKey<Vehicle>(v => v.PolicyId);
        }
    }
}
