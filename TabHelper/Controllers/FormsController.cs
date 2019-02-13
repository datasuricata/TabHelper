using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Linq;
using TabHelper.Data.Persistence.Interfaces;
using TabHelper.Data.Transaction;
using TabHelper.Filters;
using TabHelper.Helpers;
using TabHelper.Models;
using TabHelper.Models.Entities;
using TabHelper.Models.ViewModel;
using TabHelper.Services;

namespace TabHelper.Controllers
{
   // [TabExceptionFilter]
    public class FormsController : BaseController
    {
        #region [ properties ]

        private readonly IFormManager formManager;


        #endregion

        #region [ ctor ]

        public FormsController(IFormManager formManager, IUnitOfWork uow) : base(uow)
        {
            this.formManager = formManager;
        }

        #endregion

        #region [ get ]

        public IActionResult Index()
        {
            try
            {
                return View(new FormViewModel());
            }
            catch (Exception e)
            {
                SetMessage(e.Message, MsgType.Error); return RedirectToAction("Index", "Dash");
            }
        }

        public IActionResult Create()
        {
            try
            {
                return View(new FormAttModel());
            }
            catch (Exception e)
            {
                SetMessage(e.Message, MsgType.Error); return RedirectToAction("Index");
            }
        }

        #endregion

        #region [ post ]

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Create(FormAttModel form)
        //{
        //    try
        //    {
        //        var formAtt = new FormAttribute(form.Name, form.ComponentType, form.Title, form.Value, form.Info, form.Detail, form.IsNumeric);
        //        formAttRepo.Create(formAtt);
        //        SetMessage(Messenger.Created(formAtt), MsgType.Success);
        //        return RedirectToAction("Index");
        //    }
        //    catch (Exception e)
        //    {
        //        SetMessage(e.Message, MsgType.Error); return RedirectToAction("Index");
        //    }
        //}

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
