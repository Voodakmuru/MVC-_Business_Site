using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using TradingCommerce.Models;

namespace TradingCommerce.DAL
{
    public class businessInitializer : System.Data.Entity.
        DropCreateDatabaseIfModelChanges<businessContext>
    {
        protected override void Seed(businessContext context)
        {
            var users = new List<User>
            {
                new User{securityLevel="admin", username="cade", password="123"},
                new User{securityLevel="Client", username="madie", password="123"}
            };

            users.ForEach(s => context.Users.Add(s));
            context.SaveChanges();

            var businesses = new List<Business>
            {
                new Business{businessName="Cades Place", filePath="~/Photos/Beer_logo.png", userID=1},
                new Business{businessName="Madies place", filePath="~/Photos/visa_logo.png", userID=2}
            };

            businesses.ForEach(s => context.Businesses.Add(s));
            context.SaveChanges();
        }
    }
}