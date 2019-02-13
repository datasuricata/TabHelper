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
    public class DashController : BaseController
    {
        #region [ properties ]

        private readonly IRepository<User> userRepo;
        private readonly IRepository<Department> deptRepo;
        private readonly IFormManager formManager;

        #endregion

        #region [ ctor ]

        public DashController(IFormManager formManager, IRepository<User> userRepo, IRepository<Department> deptRepo, IUnitOfWork uow) : base(uow)
        {
            this.userRepo = userRepo;
            this.deptRepo = deptRepo;
            this.formManager = formManager;
        }

        #endregion

        #region [ get ]

        public IActionResult Index()
        {
            try
            {
                var usr = userRepo.GetQueriable();
                var dpt = deptRepo.GetQueriable();
                var tab = formManager.QueryTabs();
                var frm = formManager.QueryForms().GroupBy(x => x.TabulationId);

                return View(new DashViewModel
                {
                    DeptCountAct = dpt.Where(x => !x.IsDeleted).Count(),
                    DeptCountInt = dpt.Where(x => x.IsDeleted).Count(),
                    UserCountAct = usr.Where(x => !x.IsDeleted).Count(),
                    UserCountInt = usr.Where(x => x.IsDeleted).Count(),
                    FormCountAct = frm.Select(x => x.Key).Count(),
                    TabCountAct = tab.Where(x => !x.IsDeleted).Count(),
                    TabCountInt = tab.Where(x => x.IsDeleted).Count(),
                });
            }
            catch (Exception e)
            {
                SetMessage(e.Message, MsgType.Error);
                return RedirectToAction("Error");
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
