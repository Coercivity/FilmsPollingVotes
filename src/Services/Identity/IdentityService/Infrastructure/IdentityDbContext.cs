using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace Infrastructure
{
    public class IdentityDbContext : DbContext
    {

        public virtual DbSet<User> Users { get; set; }
        public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options)
        { 
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("users");
        }


    }
}
