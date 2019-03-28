using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
using TabHelper.Services;
using TabHelper.Services.Interfaces;

namespace TabHelper.Controllers
{
    public class FormsController : BaseController
    {
        #region [ properties ]

        private readonly IFormManager formManager;
        private readonly IRepository<Form> formRepo;
        private readonly IRepository<FormAttribute> formAttRepo;
        private readonly IRepository<Tabulation> tabRepo;
        private readonly IViewRender viewRender;

        #endregion

        #region [ ctor ]

        public FormsController(IViewRender viewRender, IRepository<Tabulation> tabRepo, IRepository<Form> formRepo, IRepository<FormAttribute> formAttRepo, IFormManager formManager, IUnitOfWork uow) : base(uow)
        {
            this.tabRepo = tabRepo;
            this.formRepo = formRepo;
            this.viewRender = viewRender;
            this.formAttRepo = formAttRepo;
            this.formManager = formManager;
        }

        #endregion

        #region [ get ]

        public IActionResult Index()
        {
            try
            {
                var forms = formRepo.GetQueriable()
                    .Include(x => x.FormTabs)
                    .ThenInclude(x => x.Tabulation)
                    .ToList();

                ViewBag.Tabulations = GetDropDown(tabRepo.List(), nameof(Tabulation.Name), nameof(Tabulation.Id));

                return View(new FormViewModel {
                    Forms = forms.ConvertAll(e => (FormModel)e)
                });
            }
            catch (Exception e)
            {
                SetMessage(e.Message, MsgType.Error); return RedirectToAction("Error");
            }
        }

        /// <summary>
        /// Load Attribute rendered form via service
        /// </summary>
        /// <param name="count"></param>
        /// <returns>Json Result</returns>
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

        public IActionResult Edit(int id)
        {
            try
            {
                var form = formRepo.GetById(id);
                return View((FormModel)form);
            }
            catch (Exception e)
            {
                SetMessage(e.Message, MsgType.Error); return RedirectToAction("Index");
            }
        }

        public IActionResult EditAttribute(int id)
        {
            try
            {
                var att = formAttRepo.GetById(id);
                return View((FormAttModel)att);
            }
            catch (Exception e)
            {
                SetMessage(e.Message, MsgType.Error); return RedirectToAction("Index");
            }
        }

        public IActionResult Delete(int id)
        {
            try
            {
                var form = formRepo.GetById(id);
                DomainValidation.When(form is null, "Formulário não encontrado, contate o suporte");
                return View((FormModel)form);
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
        public IActionResult CreateForm(FormManager vm)
        {
            try
            {
                var entity = new Form(vm.Form.Name, vm.Form.Code);

                var attrs = vm.Attributes.Select(x => new FormAttribute(
                        entity, x.Name, x.ComponentType, x.Title, x.Value,
                        x.Info, x.Detail, x.IsNumeric, x.Order, x.Repeat
                    )).ToList();

                SetMessage(Messenger.Created(formRepo.Create(entity)), MsgType.Success);

                formAttRepo.CreateRange(attrs);

                if (vm.Tabulations.Any())
                    vm.Tabulations.ForEach(x => { formManager.Register(entity.Id, x); });

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                SetMessage(e.Message, MsgType.Error); return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(FormModel form)
        {
            try
            {
                var entity = formRepo.GetById(form.Id);
                entity.Edit(form.Name, form.Code);

                SetMessage(Messenger.Changed(formRepo.Update(entity)), MsgType.Info);

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                SetMessage(e.Message, MsgType.Error); return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditFormAtt(FormAttModel form)
        {
            try
            {
                var attribute = formAttRepo.GetById(form.Id);
                attribute.Edit(form.Name, form.ComponentType,
                                form.Title, form.Value,
                                form.Info, form.Detail,
                                form.IsNumeric, form.Order, form.Repeat);

                SetMessage(Messenger.Changed(formAttRepo.Update(attribute)), MsgType.Info);

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                SetMessage(e.Message, MsgType.Error); return RedirectToAction("Index");
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(FormModel form)
        {
            try
            {
                var entity = formRepo.GetById(form.Id);
                DomainValidation.When(form is null, "Formulário não encontrado, contate o suporte");
                SetMessage(Messenger.SoftExclude(formRepo.SoftExclude(entity)), MsgType.Success);
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

