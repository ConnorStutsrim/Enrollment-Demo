using Enrollment.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Enrollment.ViewModels;
using Microsoft.AspNet.Identity;
using Enrollment.Models;

namespace Enrollment.Controllers
{
    public class HomeController : UtilityController
    {
        public ActionResult Index()
        {
            return View();
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