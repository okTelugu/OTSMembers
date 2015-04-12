using OTSMembers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OTSMembers.Controllers
{
    [RequireHttps]

    public class HomeController : Controller
    {
        private OtsDb db = new OtsDb();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "About OTS Services";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "You can mail your donations or sponsorships to :";
            return View(db.OTSAddresses.ToList());

        }
    }
}