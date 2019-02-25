using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TabHelper.Data.Persistence.Interfaces;
using TabHelper.Data.Transaction;
using TabHelper.Helpers;
using TabHelper.Models;
using TabHelper.Models.ComponentModel;
using TabHelper.Models.Entities;
using TabHelper.Models.ViewModel;
using TabHelper.Services.Interfaces;

namespace TabHelper.Controllers
{
    public class FormsController : BaseController
    {
        #region [ properties ]

        private readonly IFormManager formManager;
        private readonly IRepository<Form> formRepo;
        private readonly IRepository<FormAttribute> formAttRepo;
        private readonly IViewRender viewRender;


        #endregion

        #region [ ctor ]

        public FormsController(IViewRender viewRender, IRepository<Form> formRepo, IRepository<FormAttribute> formAttRepo, IFormManager formManager, IUnitOfWork uow) : base(uow)
        {
            this.formAttRepo = formAttRepo;
            this.formManager = formManager;
            this.formRepo = formRepo;
            this.viewRender = viewRender;
        }

        #endregion

        #region [ get ]

        public IActionResult Index()
        {
            try
            {
                var forms = formRepo.List().ToList();
                return View(new FormViewModel { Forms = forms.ConvertAll(e => (FormModel)e) });
            }
            catch (Exception e)
            {
                SetMessage(e.Message, MsgType.Error); return RedirectToAction("Index", "Dash");
            }
        }

        public IActionResult Attribute(int count)
        {
            try
            {
                return new JsonResult(new HtmlString(viewRender.Render("Forms/Attribute", 
                    new ComponentBase { Counter = count }).AjustHtml()));
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
        public IActionResult CreateForm(CreateForm viewModel)
        {
            try
            {
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

