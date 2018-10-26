using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TradingCommerce.Models;
using System.Data.Entity.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace TradingCommerce.Models
{   
    public class User
    { 
        [Key]
        [Column(Order =1)]
        public int userID { get; set; }
        public string securityLevel { get; set; }
        public string username { get; set; }        
        public string password { get; set; }       
        public virtual ICollection<Business> Businesses { get; set; }
    }
}