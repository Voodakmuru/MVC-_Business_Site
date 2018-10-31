using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TradingCommerce.DAL;
using TradingCommerce.Models;
using System.Data.SqlClient;
using System.Data;

namespace TradingCommerce
{
    static public class Security
    {
        static public bool login( string username, string password)
        {
            SqlConnection conn = new SqlConnection(@"Data Source=(Localdb)\MSSQLLocalDB;Initial Catalog=businessContext;Integrated Security=SSPI");
            conn.Open();
            SqlCommand co = new SqlCommand("", conn);
            co.CommandText = "select userID, securityLevel from [User] where username='" + username + "' and password='" + password + "'";

            SqlDataReader reader = co.ExecuteReader();
            int userID = -1;
            string securityLevel = "";
            if (reader.HasRows)
            {
                reader.Read();
                userID = Convert.ToInt32(reader["userID"]);
                securityLevel = reader["securityLevel"].ToString();
                HttpContext.Current.Session["userID"] = userID;
                HttpContext.Current.Session["securityLevel"] = securityLevel;
            }
            conn.Close();

            if(userID == -1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        static public void checkLevel(string required)
        {
            bool isInvalid = true;

            if(HttpContext.Current.Session["securityLevel"] != null)
            {
                string securityLevel = HttpContext.Current.Session["securityLevel"].ToString();
                if(securityLevel == "admin")
                {
                    isInvalid = false;
                }
                else
                {
                    if(required == "client")
                    {
                        isInvalid = false;
                    }
                }               
            }
            if (isInvalid)
            {
                HttpContext.Current.Session.Abandon();
                HttpContext.Current.Response.Redirect("/Login/Login");
            }
        }
       

 

}
}


    
