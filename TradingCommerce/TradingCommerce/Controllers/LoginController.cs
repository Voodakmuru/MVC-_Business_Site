using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TradingCommerce.DAL;
using TradingCommerce.Models;
using System.Data.Entity;
using System.Data;
using System.Data.SqlClient;

namespace TradingCommerce.Controllers
{
    public class LoginController : Controller
    {
        SqlConnection conn = new SqlConnection(@"Data Source=(Localdb)\MSSQLLocalDB;Initial Catalog=businessContext;Integrated Security=SSPI");
        private businessContext db = new businessContext();
        
        // GET: Login
        public ActionResult loginPage()
        { 
            return View();
        }

        //([Bind(Include = "businessID,businessName,userID")] Business business)

        public ActionResult login()
        {

            return View();
        }

        [HttpPost]
        public ActionResult login([Bind(Include = "username, password")] User user)
        {
                if ((user.username == null || user.password == null) || !Security.login (user.username, user.password))
                {
                    ViewBag.message = "Invalid Login";
                    return View();
                }    
                
            return View();  
        }

        public ActionResult logout()
        {
            if (Session["userID"] == null && Session["securityLevel"] == null)
            {
                Response.Redirect("/Login/Login");
            }
            else
            {
                Session["userID"] = null;
                Session["securityLevel"] = null;
                ViewBag.message = "Your session has ended.";
            }
            return View();
        }
    }
}