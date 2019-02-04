using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.Linq;
using TabHelper.Data.Persistence.Interfaces;
using TabHelper.Data.Transaction;
using TabHelper.Helpers;
using TabHelper.Models;
using TabHelper.Models.Entities;
using TabHelper.Models.ViewModel;
using TabHelper.Services;

namespace TabHelper.Controllers
{
    public class UserController : Controller
    {
        private IRepository<User> userRepo;
        private IRepository<Department> deptRepo;
        private IUnitOfWork uow;

        public UserController(IRepository<User> userRepo, IRepository<Department> deptRepo, IUnitOfWork uow)
        {
            this.userRepo = userRepo;
            this.deptRepo = deptRepo;
            this.uow = uow;
        }

        public IActionResult Index()
        {
            try
            {
                var usrs = userRepo.List().Where(x => x.Id != 1).ToList();
                return View(new UserViewModel { Users = usrs });
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
                ViewBag.Access = Utils.GetAccessDropdown();
                ViewBag.Departments = deptRepo.List().Where(x => x.Name != "Root")
                    .Select(x => new SelectListItem(x.Name, x.Id.ToString())).ToList();

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
        public IActionResult Create(UserManagerModel form)
        {
            try
            {
                var dept = deptRepo.GetById(form.DepartmentId);
                userRepo.Create(new User(form.Name, form.Email, form.Password, dept, form.UserAccess));
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ViewData["Error"] = e.Message;
                return RedirectToAction("Index");
            }
        }

        public IActionResult Login()
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

        public IActionResult Block(int id)
        {
            try
            {
                var user = userRepo.GetById(id);
                DomainValidation.When(user == null, "User not found.");
                return View(new UserAccessModel
                {
                    Display = user.IsDeleted ? "desbloquear" : "bloquear",
                    Id = user.Id,
                    Name = user.Name
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
        public IActionResult Block(UserAccessModel form)
        {
            try
            {
                var user = userRepo.GetById(form.Id);
                DomainValidation.When(user is null, "User not found.");
                user.IsDeleted = !user.IsDeleted;
                userRepo.Update(user);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ViewData["Error"] = e.Message;
                return RedirectToAction("Index");
            }
        }

        public IActionResult Delete()
        {
            try
            {
                return View();
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
                var user = userRepo.GetQueriable().Include(x => x.Department)
                    .Where(x => x.Id == id).SingleOrDefault();

                DomainValidation.When(user == null, "User not found.");

                ViewBag.Access = Utils.GetAccessDropdown();
                ViewBag.Departments = deptRepo.List().Where(x => x.Name != "Root")
                    .Select(x => new SelectListItem(x.Name, x.Id.ToString())).ToList();

                return View(new UserManagerModel
                {
                    DepartmentId = user.Department.Id,
                    Email = user.Email,
                    Name = user.Name,
                    UserAccess = user.UserAccess,
                    Password = user.Password
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
        public IActionResult Edit(UserManagerModel form)
        {
            try
            {
                var user = userRepo.GetQueriable().Include(x => x.Department)
                    .Where(x => x.Id == form.Id).SingleOrDefault();

                var dept = deptRepo.GetById(form.DepartmentId);
                var entity = new User(form.Name, form.Email, form.Password, dept, form.UserAccess);

                user.Edit(entity);

                userRepo.Update(user);

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