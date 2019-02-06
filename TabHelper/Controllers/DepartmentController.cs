using System;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TabHelper.Data.Persistence.Interfaces;
using TabHelper.Models;
using TabHelper.Models.Entities;
using System.Linq;
using TabHelper.Models.ViewModel;
using TabHelper.Services;

namespace TabHelper.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IRepository<Department> deptRepo;

        public DepartmentController(IRepository<Department> deptRepo)
        {
            this.deptRepo = deptRepo;
        }

        public IActionResult Index()
        {
            try
            {
                var depts = deptRepo.List().ToList();
                var model = depts.ConvertAll(e => (DepartmentModel)e);
                return View(new DepartmentViewModel { Departments = model });
            }
            catch (Exception e)
            {
                ViewData["Error"] = e.Message;
                return RedirectToAction("Index", "Dash");
            }
        }

        public IActionResult List()
        {
            return PartialView();
        }

        public IActionResult Create()
        {
            try
            {
                return View();
            }
            catch (Exception e)
            {
                ViewData["Error"] = e.Message;
                return RedirectToAction("Index", "Dash");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(DepartmentModel form)
        {
            try
            {
                var dept = new Department(form.Name, form.Description);
                deptRepo.Create(dept);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ViewData["Error"] = e.Message;
                return RedirectToAction("Index");
            }
        }

        public IActionResult Delete(int id)
        {
            try
            {
                var dept = deptRepo.GetById(id);
                DomainValidation.When(dept == null, "Dept not found.");
                return View(new DepartmentModel
                {
                    Id = dept.Id,
                    Name = dept.Name,
                    Description = dept.Description,
                });
            }
            catch (Exception e)
            {
                ViewData["Error"] = e.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(DepartmentModel form)
        {
            try
            {
                var dept = deptRepo.GetById(form.Id);
                DomainValidation.When(dept == null, "Dept not found.");
                deptRepo.SoftExclude(dept);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ViewData["Error"] = e.Message;
                return RedirectToAction("Index");
            }
        }

        public IActionResult Edit(int id)
        {
            try
            {
                var dept = deptRepo.GetById(id);
                DomainValidation.When(dept == null, "User not found.");

                return View(new DepartmentModel {
                    Id = dept.Id,
                    Name = dept.Name,
                    Description = dept.Description,
                });
            }
            catch (Exception e)
            {
                ViewData["Error"] = e.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(DepartmentModel form)
        {
            try
            {
                var dept = deptRepo.GetById(form.Id);
                var edited = new Department(form.Name, form.Description);

                dept.Edit(edited);
                deptRepo.Update(dept);

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ViewData["Error"] = e.Message;
                return PartialView("_ValidationMaster");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
