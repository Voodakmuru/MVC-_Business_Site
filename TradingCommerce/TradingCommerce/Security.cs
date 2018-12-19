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
        static public string securityLevel;

        static public bool login(string username, string password)
        {
            SqlConnection conn = new SqlConnection(@"Data Source=(Localdb)\MSSQLLocalDB;Initial Catalog=businessContext;Integrated Security=SSPI");
            conn.Open();
            SqlCommand co = new SqlCommand("", conn);
            co.CommandText = "select userID, securityLevel from [User] where username='" + username + "' and password='" + password + "'";

            SqlDataReader reader = co.ExecuteReader();
            int userID = -1;
            securityLevel = "";
            if (reader.HasRows)
            {
                reader.Read();
                userID = Convert.ToInt32(reader["userID"]);
                securityLevel = reader["securityLevel"].ToString();
                HttpContext.Current.Session["userID"] = userID;
                HttpContext.Current.Session["securityLevel"] = securityLevel;
            }
            conn.Close();

            if (userID == -1)
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
            HttpContext.Current.Session["isInvalid"] = isInvalid;

            if (HttpContext.Current.Session["securityLevel"] == null)
            {

            }
            else
            {
                securityLevel = HttpContext.Current.Session["securityLevel"].ToString();
                if (securityLevel == "admin")
                {
                    isInvalid = false;
                    HttpContext.Current.Session["isInvalid"] = isInvalid;
                }
                else if(securityLevel == "Client")
                {
                        if (required == "Client")
                        {
                            isInvalid = false;
                            HttpContext.Current.Session["isInvalid"] = isInvalid;
                        }                  
                }
                else
                {
                    isInvalid = true;
                    HttpContext.Current.Response.Redirect("/Login/Login");
                }
            }
        }
    }
}



