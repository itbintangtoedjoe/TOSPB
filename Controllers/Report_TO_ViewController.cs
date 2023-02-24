using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class Report_TO_ViewController : Controller
    {
        // GET: Report_TO_View
        public ActionResult Report_TO_Preview(string No_TO, string Status, string LineNumber)
        {
            return View();
        }
        public ActionResult Report_TO_CR(string No_TO)
        {
            ViewBag.No_Surat = No_TO;
            return View();
        }
    }
}