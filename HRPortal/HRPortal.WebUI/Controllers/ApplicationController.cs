using HRPortal.BLL;
using HRPortal.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HRPortal.WebUI.Controllers
{
    public class ApplicationController : Controller
    {
        AppManager appManager = new AppManager();

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Apply()
        {
            var app = new Application();
            return View(app);
        }

        [HttpPost]
        public ActionResult Apply(Application app)
        {
            if (ModelState.IsValid)
            {
                appManager.Add(app);

                return RedirectToAction("CompletedApplication");
            }

            else
            {
                return View(app);
            }
        }

        [HttpGet]
        public ActionResult CompletedApplication()
        {
            return View();
        }
    }
}