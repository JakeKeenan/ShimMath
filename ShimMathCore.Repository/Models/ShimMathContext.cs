

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ShimMathCore.Repository.Models
{
    public class ShimMathContext : IdentityDbContext
    {
        public DbSet<UserRole> userrole { get; set; }
        public DbSet<User> user { get; set; }
        public ShimMathContext(DbContextOptions<ShimMathContext> options): base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //    => optionsBuilder
        //        .UseMySql(@"Server=localhost;Database=shimmath;User=keenanj3;Password=Fivehundred500#1#;");


    }
}
