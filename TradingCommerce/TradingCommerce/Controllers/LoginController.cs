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
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDb)\MSSQLLocalDb;Initial Catalog=businessContext;Integrated Security=SSPI");
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
            if (user.username == null || user.password == null)
            {
                ViewBag.message = "Invalid Login";
                return View();
            }
            conn.Open();
            SqlCommand co = new SqlCommand("", conn);
            co.CommandText = "select userID, securityLevel from [User] where username='" + user.username + "'";

            SqlDataReader reader = co.ExecuteReader();
            int userID = -1;
            string securityLevel = "";
            if (reader.HasRows)
            {
                reader.Read();
                userID = Convert.ToInt32(reader["userID"]);
                securityLevel = reader["securityLevel"].ToString();
                ViewBag.message = "data present";
                user.userID = userID;
                user.securityLevel = securityLevel;
                Session["userID"] = user.userID;
                Session["securityLevel"] = user.securityLevel;
            }          
            conn.Close();

            return View();
        }
    }
}