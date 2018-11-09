using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TradingCommerce.Models
{
    public class Event
    {
        public int eventID { get; set; }
        public string details { get; set; }
        public int businessID { get; set; }
        public virtual Business Business { get; set; }
    }
}