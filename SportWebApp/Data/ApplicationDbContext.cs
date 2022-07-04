﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SportWebApp.Models;

namespace SportWebApp.Data
{        
    /// <summary>
    /// Application Database context
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>()
                .Ignore(user => user.ConcurrencyStamp)
                .Ignore(user => user.LockoutEnabled)
                .Ignore(user => user.LockoutEnd);

            modelBuilder.Entity<ApplicationUser>()
                .HasOne<UserProfile>(u => u.UserProfile)
                .WithOne(c => c.ApplicationUser)
                .HasForeignKey<UserProfile>(c => c.ApplicationUserId);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany<Exercise>(c => c.Exercises)
                .WithOne(e => e.ApplicationUser)
                .HasForeignKey(c=>c.ApplicationUserId);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany<Training>(c => c.Training)
                .WithOne(e => e.ApplicationUser)
                .HasForeignKey(c => c.ApplicationUserId);
           

            modelBuilder.Entity<ApplicationUser>()
             .HasOne<UserAvatar>(u => u.UserAvatar)
             .WithOne(c => c.ApplicationUser)
             .HasForeignKey<UserAvatar>(c => c.ApplicationUserId);


            modelBuilder.Entity<ApplicationUser>().ToTable("User");
        }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Training> Trainings { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<UserAvatar> UserAvatars { get; set; }
    }
}
