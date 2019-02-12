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

        private readonly IRepository<Form> formRepo;
        private readonly IRepository<Tabulation> TabRepo;
        private readonly IRepository<FormAttribute> formAttRepo;


        #endregion

        #region [ ctor ]

        public FormsController(IRepository<Form> formRepo, IRepository<FormAttribute> formAttRepo, IRepository<Tabulation> TabRepo, IUnitOfWork uow) : base(uow)
        {
            this.TabRepo = TabRepo;
            this.formRepo = formRepo;
            this.formAttRepo = formAttRepo;
        }

        #endregion

        #region [ get ]

        public IActionResult Index()
        {
            try
            {
                var atts = formAttRepo.List().ToList();
                ViewBag.DropDown = GetDropDown(TabRepo.List(), "Name", "Id");
                return View(new FormViewModel { FormAttibutes = atts.ConvertAll(e => (FormAttModel)e) });
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(FormAttModel form)
        {
            try
            {
                var formAtt = new FormAttribute(form.Name, form.ComponentType, form.Title, form.Value, form.Info, form.Detail, form.IsNumeric);
                formAttRepo.Create(formAtt);
                SetMessage(Messenger.Created(formAtt), MsgType.Success);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                SetMessage(e.Message, MsgType.Error); return RedirectToAction("Index");
            }
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
