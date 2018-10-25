using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using EventManagementSystem.Models;
using Rotativa;
using Rotativa.MVC;

namespace EventManagementSystem.Controllers
{
    public class ExecutiveController : Controller
    {
        //
        // GET: /Executive/
        private EMSDbContext dbContext=new EMSDbContext();
        public ActionResult Index()
        {
            if (Session["Id"] == null)
            {
                return RedirectToAction("Login", "Home")
                ;
            }
            return View();
        }
        [HttpGet]
        public ActionResult VisitorRegistration()
        {
            if (Session["Id"] == null)
            {
               return RedirectToAction("Login", "Home")
                ;
            }
            ViewBag.EventId = new SelectList(dbContext.Events, "EventId", "EventName");
            return View();
        }
        [HttpPost]
        public ActionResult VisitorRegistration(VisitorRegistration visitorRegistration)
        {
            ViewBag.EventId = new SelectList(dbContext.Events, "EventId", "EventName",visitorRegistration.EventId);
            visitorRegistration.TimeOfRegistration = DateTime.Now;
            var Visitor = dbContext.VisitorRegistrations.ToList();
            var count = Visitor.Count;
            var ZeroToBeAdded = 3;
            string number = "";
            for (int i = 0; i < ZeroToBeAdded; i++)
            {
                number += 0;
            }
            count += 1;
            visitorRegistration.TicketNo ="CODEBREAKERS" + "-" + number + count;
                dbContext.VisitorRegistrations.Add(visitorRegistration);
                dbContext.SaveChanges();
                ViewBag.msg = "Visitor Has been  succesfully registered";

                return RedirectToAction("VisitorList");
        }

        public ActionResult GetEventById(int eId)
        {
            var eEvent=
            dbContext.Events.Where(m=>m.EventId==eId);
            return Json(eEvent.ToList(), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult VisitorList()
        {
            if (Session["Id"] == null)
            {
                return RedirectToAction("Login", "Home")
                    ;
            }

            return View(dbContext.VisitorRegistrations.ToList());

        }
        [HttpGet]

        public ActionResult PrintDetails(int? Id)
        {
            var id = dbContext.VisitorRegistrations.Select(p => p.VisitorId).DefaultIfEmpty(0).Max();

            VisitorRegistration visitor = new VisitorRegistration();

            visitor = dbContext.VisitorRegistrations.Find(id);
            if (visitor == null)
            {
                return HttpNotFound();
            }

            var aVisitor = visitor;

            return View(aVisitor);

        }
        public ActionResult PrintTicket(int? id)
        {
            var printTicket = new ActionAsPdf("PrintDetails" , new {id=id});
            return printTicket;
        }

	}
}