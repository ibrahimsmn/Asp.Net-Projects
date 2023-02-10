using GameNews.Configurations;
using GameNews.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace GameNews.Context
{
    public class MyDbContext:IdentityDbContext<AppUser,AppUserRole,int>
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) :base(options)
        {

        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new NewsConfiguration());
            
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Contact> Contacts { get; set; }
       
    }
    
}
