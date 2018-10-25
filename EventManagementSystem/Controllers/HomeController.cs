using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventManagementSystem.Models;

namespace EventManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        private EMSDbContext dbContext=new EMSDbContext();
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            if (Session["Id"] != null)
            {
                Session["Id"] = null;
                ;
            }
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginModel aLoginModel)
        {
            var status = dbContext.Users.Where(m => m.Email == aLoginModel.Email);
            if (status.Count()>0)
            {
                var name = status.FirstOrDefault().Name;
                Session["Id"] = status.FirstOrDefault().Id;
                Session["user"] = name;
                Session["status"] = true;
                Session["UserType"] = status.FirstOrDefault().UserTypeId;
                if (status.FirstOrDefault().UserTypeId==1)
                {
                    return RedirectToAction("Dashboard", "Event");
                }
                else
                {
                    return RedirectToAction("Index", "Executive");
                }
                
            }
            return View();
        }
        public ActionResult LogOut()
        {
            Session["Id"] = null;
            return RedirectToAction("Login", "Home");

        }
	}
}