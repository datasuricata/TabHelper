using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using TabHelper.Data.Transaction;
using TabHelper.Models;

namespace TabHelper.Controllers
{
    public class TabController : BaseController
    {
        #region [ ctor ]

        public TabController(IUnitOfWork uow) : base(uow)
        {

        }

        #endregion

        #region [ get ]

        public IActionResult Index()
        {
            try
            {
                return View();
            }
            catch (Exception e)
            {
                SetMessage(e.Message, MsgType.Error); return RedirectToAction("Error");
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

        #endregion

        #region [ error ]

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        #endregion
    }
}
