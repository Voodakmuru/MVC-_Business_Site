using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TradingCommerce.Models;
using System.Data.SqlClient;
using TradingCommerce.DAL;
using System.Data.Entity;

namespace TradingCommerce.Controllers
{
    public class HomeController : Controller
    {
        private businessContext db = new businessContext();

        public ActionResult Index()
        {
            var events = db.Events.Include(b => b.Business);
            ViewData["sessionUserID"] = System.Web.HttpContext.Current.Session["sessionUserID"];
            return View(events.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}