using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CrystalDecisions.Shared;
using PAOnline.Models;
using ZXing;
using System.Drawing;
using System.Drawing.Imaging;
using System.Reflection;
using ImageFormat = System.Drawing.Imaging.ImageFormat;

namespace WebApplication1.Controllers
{
    public class Report_SPB_ViewController : Controller
    {
        // GET: Report_SPB_View
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Report_SPB_Memo()
        {
            return View();
        }
        public ActionResult AddApproval_SPB()
        {
            return View();
        }

        public ActionResult Report_SPB_Preview()
        {
            return View();
        }
        public ActionResult Rpt_SPB(string No_Surat, string Status, string LineNumber)
        {
            ViewBag.No_Surat = No_Surat;
            ViewBag.Status = Status;
            return View();
        }
        public ActionResult Report_Lampiran_SPB(string No_Surat)
        {
            ViewBag.No_Surat = No_Surat;
            return View();
        }

    }
}