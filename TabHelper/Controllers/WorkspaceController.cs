using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TabHelper.Data.Transaction;
using TabHelper.Models;

namespace TabHelper.Controllers
{
    public class WorkspaceController : BaseController
    {
        #region [ ctor ]

        public WorkspaceController(IUnitOfWork uow) : base(uow)
        {

        }

        #endregion

        #region [ get ]

        public IActionResult Index()
        {
            return View();
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
