using System;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;
using Castle.DynamicProxy.Generators;
using TabHelper.Data.ORM;
using TabHelper.Data.Persistence;
using TabHelper.Models;
using TabHelper.Data.Persistence.Interfaces;
using TabHelper.Models.Entities;
using TabHelper.Views.Shared;

namespace TabHelper.Controllers
{
    public class UserController : Controller
    {
        private IRepository<User> repository;

        public UserController()
        {
        }

        public IActionResult Index()
        {
            try
            {
                return View();
            }
            catch (Exception e)
            {
                ViewData["Error"] = e.Message;
                return RedirectToAction("Index","Dash");
            }
        }
        
        public IActionResult List()
        {
            var u = repository.All();
            var userViewModels = u.Select(x => 
                new UserViewModel() {Id = x.Id, Name = x.Name, Email = x.Email, UserAccess = x.UserAccess}).ToList();
            return PartialView(userViewModels);
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
        
        public IActionResult Block()
        {
            return View();
        }
        
        public IActionResult Delete()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}