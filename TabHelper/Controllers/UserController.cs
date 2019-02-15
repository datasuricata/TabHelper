using Microsoft.AspNetCore.Mvc;
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
    //[TabExceptionFilter]
    public class UserController : BaseController
    {
        #region [ properties ]

        private readonly IRepository<User> userRepo;
        private readonly IRepository<Department> deptRepo;

        #endregion

        #region [ ctor ]

        public UserController(IRepository<User> userRepo, IRepository<Department> deptRepo, IUnitOfWork uow) : base(uow)
        {
            this.userRepo = userRepo;
            this.deptRepo = deptRepo;
        }

        #endregion

        #region [ get ]

        public IActionResult Index()
        {
            try
            {
                var usrs = userRepo.List().ToList();
                return View(new UserViewModel { Users = usrs.ConvertAll(e => (UserModel)e) });
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
                ViewBag.Departments = GetDropDown(deptRepo.List()
                    .Where(x => x.Name != "Root").ToList(), "Name", "Id");

                return View();
            }
            catch (Exception e)
            {
                SetMessage(e.Message, MsgType.Error); return RedirectToAction("Index");
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
                SetMessage(e.Message, MsgType.Error); return RedirectToAction("Index");
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
                SetMessage(e.Message, MsgType.Error); return RedirectToAction("Index");
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
                SetMessage(e.Message, MsgType.Error); return RedirectToAction("Index");
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
                SetMessage(e.Message, MsgType.Error); return RedirectToAction("Index");
            }
        }

        #endregion

        #region [ post ]

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(UserModel form)
        {
            try
            {
                var dept = deptRepo.GetById(form.DepartmentId);
                SetMessage(Messenger.Created(userRepo.Create(
                    new User(form.Name, form.Email, form.Password, dept, form.UserAccess))), MsgType.Success);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                SetMessage(e.Message, MsgType.Error); return RedirectToAction("Index");
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
                SetMessage(Messenger.Changed(userRepo.Update(user)), MsgType.Success);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                SetMessage(e.Message, MsgType.Error); return RedirectToAction("Index");
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
                SetMessage(Messenger.SoftExclude(userRepo.SoftExclude(user)), MsgType.Success);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                SetMessage(e.Message, MsgType.Error); return RedirectToAction("Index");
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

                user.Edit(new User(form.Name, form.Email, form.Password, deptRepo.GetById(form.DepartmentId), form.UserAccess));
                SetMessage(Messenger.Changed(userRepo.Update(user)), MsgType.Success);

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