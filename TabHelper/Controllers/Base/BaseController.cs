using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using TabHelper.Data.Transaction;
using TabHelper.Models;

namespace TabHelper.Controllers
{
    public class BaseController : Controller
    {
        #region [ properties ]

        private readonly IUnitOfWork uow;

        #endregion

        #region [ ctor ]

        public BaseController(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        #endregion

        #region [ components ]

        /// <summary>
        /// Return generic dropdown
        /// </summary>
        /// <typeparam name="T">Entity object</typeparam>
        /// <param name="obj">List of entity object</param>
        /// <param name="pT">name of property for take text value from object</param>
        /// <param name="pV">name of property for take key value from object</param>
        /// <returns>List of SelecListItem</returns>
        protected List<SelectListItem> GetDropDown<T>(List<T> obj, string pT, string pV)
        {
            dynamic o = new { Text = "", Value = "" };
            var list = new List<SelectListItem>();

            foreach (var x in obj)
            {
                foreach (var p in x.GetType().GetProperties())
                    if (p.Name == pT)
                        o.Text = p.GetValue(x).ToString();

                foreach (var p in x.GetType().GetProperties())
                    if (p.Name == pV)
                        o.Value = p.GetValue(x).ToString();

                list.Add(new SelectListItem(o.Text, o.Value));
            }
            return list;
        }

        #endregion

        #region [ network methods ]

        /// <summary>
        /// return ip address
        /// </summary>
        /// <returns>Remote Ip Address</returns>
        protected string RequestIp()
        {
            return HttpContext.Connection.RemoteIpAddress.ToString();
        }

        #endregion

        #region [ messages ]

        protected void SetMessage(string msg, MsgType msgType)
        {
            switch (msgType)
            {
                case MsgType.Error:
                    TempData["Error"] = msg;
                    break;
                case MsgType.Message:
                    TempData["Message"] = msg;
                    break;
                case MsgType.Info:
                    TempData["Info"] = msg;
                    break;
                default:
                    break;
            }
        }

        #endregion
    }
}