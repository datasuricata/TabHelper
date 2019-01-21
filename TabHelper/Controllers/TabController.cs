using System;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TabHelper.Data.Persistence.Interfaces;
using TabHelper.Models;
using TabHelper.Models.Entities;

namespace TabHelper.Controllers
{
    public class TabController : Controller
    {
        //private readonly IRepository<Tabulation> repoTab;
        //private readonly IRepository<TabulationAttributes> repoTabAtt;

        //public TabController(IRepository<Tabulation> repoTab, IRepository<TabulationAttributes> repoTabAtt)
        //{
        //    this.repoTab = repoTab;
        //    this.repoTabAtt = repoTabAtt;
        //}

        public IActionResult Index()
        {
            try
            {
                return View();
            }
            catch (Exception e)
            {
                ViewData["Error"] = e.Message;
                return RedirectToAction("Index","Dash");
            }
        }

        public IActionResult Create()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Edit()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Delete()
        {
            return View();
        }

        public IActionResult List()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
