using DataAccessLayer.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer
{
    public class UserDbContext:DbContext
    {
      
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        { }
        public UserDbContext() : base()
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
          
            base.OnConfiguring(optionsBuilder);
        }
      
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>().HasKey(x => x.Id);
            builder.Entity<User>().ToTable("Users");
            base.OnModelCreating(builder);
        }
    }
}
