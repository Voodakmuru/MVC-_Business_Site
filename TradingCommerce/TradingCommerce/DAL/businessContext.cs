using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TradingCommerce.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace TradingCommerce.DAL
{
    public class businessContext : DbContext
    {
        public businessContext() : base("businessContext")
        {
            //Empty
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Business> Businesses {get; set;}
        public DbSet<Event> Events { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}