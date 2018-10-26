using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TradingCommerce.Models;

namespace TradingCommerce.Models
{
    public class Business
    {
        public int businessID { get; set; }
        public string businessName { get; set; }
        public int userID { get; set; }
        public virtual User User { get; set; }
    }
}