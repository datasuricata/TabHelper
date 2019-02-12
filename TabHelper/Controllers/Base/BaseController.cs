using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using TabHelper.Data.Transaction;
using TabHelper.Models;

namespace TabHelper.Controllers
{
    public class BaseController : Controller
    {
        #region [ propeties ]

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
        /// return a generic dropdown
        /// </summary>
        /// <typeparam name="T">entity model object</typeparam>
        /// <param name="listT">list of entity model objects</param>
        /// <param name="Text">text of property for search</param>
        /// <param name="Value">text of property for search</param>
        /// <returns></returns>
        protected List<SelectListItem> GetDropDown<T>(IEnumerable<T> listT, string Text, string Value)
        {
            var list = new List<SelectListItem>();
            
            foreach (var x in listT)
            {
                var pt = string.Empty;
                var pv = string.Empty;

                foreach (var p in x.GetType().GetProperties())
                    if (p.Name == Text)
                        pt = p.GetValue(x).ToString();

                foreach (var p in x.GetType().GetProperties())
                    if (p.Name == Value)
                        pv = p.GetValue(x).ToString();

                list.Add(new SelectListItem(pt, pv));
            }
            return list;
        }

        #endregion

        #region [ notification ]

        /// <summary>
        /// set tempdata messages
        /// </summary>
        /// <param name="msg">Write your message</param>
        /// <param name="msgType">Define your message type</param>
        protected void SetMessage(string msg, MsgType msgType)
        {
            switch (msgType)
            {
                case MsgType.Success:
                    TempData["Message"] = msg;
                    break;
                case MsgType.Error:
                    TempData["Error"] = msg;
                    break;
                case MsgType.Info:
                    TempData["Info"] = msg;
                    break;
            }
        }

        #endregion

        #region [ network ]

        /// <summary>
        /// return ip from request context
        /// </summary>
        /// <returns></returns>
        protected string RequestIp()
        {
            return HttpContext.Connection.RemoteIpAddress.ToString();
        }

        #endregion
    }
}