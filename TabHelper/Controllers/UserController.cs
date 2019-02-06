using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.Linq;
using TabHelper.Data.Persistence.Interfaces;
using TabHelper.Data.Transaction;
using TabHelper.Models;
using TabHelper.Models.Entities;
using TabHelper.Models.ViewModel;
using TabHelper.Services;

namespace TabHelper.Controllers
{
    public class UserController : BaseController
    {
        private readonly IRepository<User> userRepo;
        private readonly IRepository<Department> deptRepo;

        public UserController(IRepository<User> userRepo, IRepository<Department> deptRepo, IUnitOfWork uow) : base(uow)
        {
            this.userRepo = userRepo;
            this.deptRepo = deptRepo;
        }

        public IActionResult Index()
        {
            try
            {
                var usrs = userRepo.List().ToList();
                return View(new UserViewModel { Users = usrs.ConvertAll(e => (UserModel)e) });
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
                ViewBag.Departments = GetDropDown(deptRepo.List()
                    .Where(x => x.Name != "Root").ToList(), "Name", "Id");

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
        public IActionResult Create(UserModel form)
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
                return View((UserLightModel)user);
            }
            catch (Exception e)
            {
                ViewData["Error"] = e.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Block(UserLightModel form)
        {
            try
            {
                var user = userRepo.GetById(form.Id);
                DomainValidation.When(user is null, "User not found.");

                user.Block();
                userRepo.Update(user);

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
                var user = userRepo.GetById(id);
                DomainValidation.When(user == null, "User not found.");
                return View((UserLightModel)user);
            }
            catch (Exception e)
            {
                ViewData["Error"] = e.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(UserLightModel form)
        {
            try
            {
                var user = userRepo.GetById(form.Id);
                DomainValidation.When(user == null, "User not found.");

                userRepo.SoftExclude(user);
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
                var user = userRepo.GetQueriable().Include(x => x.Department)
                    .Where(x => x.Id == id).SingleOrDefault();

                DomainValidation.When(user == null, "User not found.");

                ViewBag.Departments = GetDropDown(deptRepo.List()
                    .Where(x => x.Name != "Root").ToList(), "Name", "Id");

                return View((UserModel)user);
            }
            catch (Exception e)
            {
                ViewData["Error"] = e.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(UserModel form)
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