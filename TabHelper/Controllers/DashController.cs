using Microsoft.AspNetCore.Mvc;
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
        private readonly IRepository<User> userRepo;
        private readonly IRepository<Department> deptRepo;

        public DashController(IRepository<User> userRepo, IRepository<Department> deptRepo, IUnitOfWork uow) : base(uow)
        {
            this.userRepo = userRepo;
            this.deptRepo = deptRepo;
        }

        public IActionResult Index()
        {
            var usr = userRepo.GetQueriable();
            var dpt = deptRepo.GetQueriable();

            var vm = new DashViewModel
            {
                DeptCountAct = dpt.Where(x => !x.IsDeleted).Count(),
                DeptCountInt = dpt.Where(x => x.IsDeleted).Count(),
                UserCountAct = usr.Where(x => !x.IsDeleted).Count(),
                UserCountInt = usr.Where(x => x.IsDeleted).Count(),
            };

            return View(vm);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
