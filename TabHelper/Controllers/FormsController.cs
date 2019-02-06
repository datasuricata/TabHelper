using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Linq;
using TabHelper.Data.Persistence.Interfaces;
using TabHelper.Data.Transaction;
using TabHelper.Models;
using TabHelper.Models.Entities;
using TabHelper.Models.ViewModel;

namespace TabHelper.Controllers
{
    public class FormsController : BaseController
    {

        private readonly IRepository<FormAttribute> formAttRepo;

        public FormsController(IRepository<FormAttribute> formAttRepo, IUnitOfWork uow) : base(uow)
        {
            this.formAttRepo = formAttRepo;
        }

        public IActionResult Index()
        {
            var atts = formAttRepo.List().ToList();
            var vm = new FormViewModel { FormAttibutes = atts.ConvertAll(e => (FormAttModel)e) };
            return View(vm);
        }

        public IActionResult Create()
        {
            try
            {
                return View(new FormAttModel());
            }
            catch (Exception e)
            {
                ViewData["Error"] = e.Message;
                return RedirectToAction("Index", "Dash");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
