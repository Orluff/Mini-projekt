using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using shared.Model;

namespace Neddit.Data
{
    public class Context : DbContext
    {
        public DbSet<ThreadPost> Threads { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<User> Users { get; set; }
        public string DbPath { get; }

        public Context()
        {
            DbPath = "bin/Neddit.db";
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ThreadPost>().ToTable("Threads");
            modelBuilder.Entity<Comment>().ToTable("Comments");
            modelBuilder.Entity<User>().ToTable("Users");
        }
    }
}
