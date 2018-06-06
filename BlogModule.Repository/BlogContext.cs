using BlogModule.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogModule.Repository
{
    public class BlogContext : DbContext
    {
        public virtual DbSet<Blog> Blog { get; set; }
        public virtual DbSet<Post> Post { get; set; }


        public BlogContext()
        { }

        public BlogContext(DbContextOptions<BlogContext> options)
            : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=BlogDatabase.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            
        }
    }
}
