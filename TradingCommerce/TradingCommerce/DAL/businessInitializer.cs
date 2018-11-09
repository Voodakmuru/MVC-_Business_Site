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

            var events = new List<Event>
            {
                new Event{details="This company sucks. Dont come here", businessID=1},
                new Event{details="This palce has some sweet sales. Come and spend your money", businessID=2},
                new Event{details="We are very poor and are now bankrupt. Life sucks.", businessID=1},
                new Event{details="Public meetup on some random date. Come and see some stuff", businessID=2}
            };

            events.ForEach(s => context.Events.Add(s));
            context.SaveChanges();
        }
    }
}