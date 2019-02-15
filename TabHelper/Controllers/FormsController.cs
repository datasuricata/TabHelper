using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
        private readonly IRepository<Form> formRepo;
        private readonly IRepository<FormAttribute> formAttRepo;


        #endregion

        #region [ ctor ]

        public FormsController(IRepository<Form> formRepo, IRepository<FormAttribute> formAttRepo, IFormManager formManager, IUnitOfWork uow) : base(uow)
        {
            this.formAttRepo = formAttRepo;
            this.formManager = formManager;
            this.formRepo = formRepo;
        }

        #endregion

        #region [ get ]

        public IActionResult Index()
        {
            try
            {
                var forms = formRepo.GetQueriable().Include(x => x.FormTabs) as List<Form>;
                return View(new FormViewModel { Forms = forms.ConvertAll(e => (FormModel)e) });
            }
            catch (Exception e)
            {
                SetMessage(e.Message, MsgType.Error); return RedirectToAction("Index", "Dash");
            }
        }

        public IActionResult CreateAtt()
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

        public IActionResult CreateForm()
        {
            try
            {
                return View(new FormTabModel());
            }
            catch (Exception e)
            {
                SetMessage(e.Message, MsgType.Error); return RedirectToAction("Index");
            }
        }

        public IActionResult ManageForms()
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateAtt(FormAttModel form)
        {
            try
            {
                //var formAtt = new FormAttribute(form.Name, form.ComponentType, form.Title, form.Value, form.Info, form.Detail, form.IsNumeric);
                //formManager.Create(formAtt);
                //SetMessage(Messenger.Created(formAtt), MsgType.Success);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                SetMessage(e.Message, MsgType.Error); return RedirectToAction("Index");
            }
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult CreateForm(FormModel form)
        //{
        //    try
        //    {
        //        var form = new form
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
