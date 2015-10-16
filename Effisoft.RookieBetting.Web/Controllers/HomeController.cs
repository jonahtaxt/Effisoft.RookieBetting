using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Effisoft.RookieBetting.Web.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return PartialView();
        }

        public ActionResult WeekGames()
        {
            return PartialView();
        }

        public ActionResult Stats()
        {
            return PartialView();
        }
    }
}