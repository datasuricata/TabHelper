using Microsoft.AspNetCore.Mvc;
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
        private readonly IRepository<User> repository;


        #endregion

        #region [ ctor ]

        public FormsController(IRepository<User> repository, IFormManager formManager, IUnitOfWork uow) : base(uow)
        {
            this.formManager = formManager;
            this.repository = repository;
        }

        #endregion

        #region [ get ]

        public IActionResult Index()
        {
            try
            {
                var atts = formManager.ListFormAtt() as List<FormAttribute>;
                var frm = formManager.ListFormsF()
                    .GroupBy(x => x.Tabulation)
                    .Select(x => new FormModel
                    {
                        Tabulation = (TabModel)x.Key,
                        TabulationId = x.Key.Id,
                    }).ToList();

                return View(new FormViewModel { FormAttibutes = atts.ConvertAll(e => (FormAttModel)e), Forms = frm });
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
                return View(new FormModel());
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

        public IActionResult ListForms()
        {
            try
            {
                var usrs = repository.List() as List<User>;
                return View(new UserViewModel { Users = usrs.ConvertAll(e => (UserModel)e) });
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
                var formAtt = new FormAttribute(form.Name, form.ComponentType, form.Title, form.Value, form.Info, form.Detail, form.IsNumeric);
                formManager.Create(formAtt);
                SetMessage(Messenger.Created(formAtt), MsgType.Success);
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
