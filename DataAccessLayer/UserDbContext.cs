using DataAccessLayer.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer
{
    public class UserDbContext:DbContext
    {
        string connectionStringName;
        public UserDbContext(string connectionString)
        {
            connectionStringName = connectionString;
        }
        public UserDbContext() : base()
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           // optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=InteliasTest;Trusted_Connection=True;");
            base.OnConfiguring(optionsBuilder);
        }
        public UserDbContext(DbContextOptions options) : base(options)
        {
           
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
