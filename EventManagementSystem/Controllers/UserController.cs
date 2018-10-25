using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventManagementSystem.Models;

namespace EventManagementSystem.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/
        private EMSDbContext dbContext = new EMSDbContext();
        public ActionResult UserIndex()
        {
            if (Session["Id"] == null)
            {
                return RedirectToAction("Login", "Home")
                ;
            }
            return View();
        }
        [HttpGet]
        public ActionResult CreateUser()
        {
            if (Session["Id"] == null)
            {
                return RedirectToAction("Login", "Home")
                ;
            }
            ViewBag.UserTypeId = new SelectList(dbContext.UserTypes, "Id", "UserTypeName");
            return View();
        }
        [HttpPost]
        public ActionResult CreateUser(User user)
        {
            
            if (ModelState.IsValid)
            {
                dbContext.Users.Add(user);
                dbContext.SaveChanges();
                ViewBag.msg = "Employee has been saved succesfully";
            }
            else
            {
                ViewBag.msg = "Failed to save";
            }
            ViewBag.UserTypeId = new SelectList(dbContext.UserTypes, "Id", "UserTypeName",user.UserTypeId);
            return View();
        }
        public JsonResult IsEmpIdExist(string email)
        {
            bool isExist = false;
            if (dbContext.Users.Any(o => o.Email == email))
            {
                isExist = true;
            }
            return Json(!isExist, JsonRequestBehavior.AllowGet);
        }
	}
}