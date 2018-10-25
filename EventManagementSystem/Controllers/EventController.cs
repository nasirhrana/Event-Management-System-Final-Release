using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EventManagementSystem.Models;
using EventManagementSystem.ViewModel;

namespace EventManagementSystem.Controllers
{
    public class EventController : Controller
    {
        //
        // GET: /Event/
        private EMSDbContext dbContext = new EMSDbContext();

        public ActionResult EventIndex()
        {
            if (Session["Id"] == null)
            {
                return RedirectToAction("Login", "Home")
                    ;
            }
            return View();
        }

        [HttpGet]
        public ActionResult CreateEvent()
        {
            if (Session["Id"] == null)
            {
                return RedirectToAction("Login", "Home")
                    ;
            }
            return View();
        }

        [HttpPost]
        public ActionResult CreateEvent(Event eEvent)
        {
            eEvent.UserId = (int) Session["Id"];
            eEvent.CreatedBy = (string) Session["user"];
            eEvent.TimeOfCreation = DateTime.Now;
            bool isExist = false;
            var msg = "";

            var eventList =
                dbContext.Events.Where(m => m.EventVenue == eEvent.EventVenue && m.EventDate == eEvent.EventDate)
                    .ToList();
            if (eventList.Count == 0)
            {
                dbContext.Events.Add(eEvent);
                dbContext.SaveChanges();
                msg = "Event Information has been saved successfully";
            }
            foreach (var ev in eventList)
            {
                if ((eEvent.StartTime >= ev.StartTime && eEvent.StartTime < ev.EndTime)
                    || (eEvent.EndTime > ev.StartTime && eEvent.EndTime <= ev.StartTime))
                {
                    msg = "Event Time conflicted";
                }
                else
                {
                    dbContext.Events.Add(eEvent);
                    dbContext.SaveChanges();
                    msg = "Event Information has been saved successfully";
                }
            }


            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ShowDateWiseEvent()
        {
            if (Session["Id"] == null)
            {
                return RedirectToAction("Login", "Home")
                    ;
            }
            return View();
        }

        public JsonResult ShowDateWiseEventByRange(DateTime frmDate, DateTime edDate)
        {
            var eventList = dbContext.Events.Where(x => x.EventDate >= frmDate && x.EventDate <= edDate).ToList();

            List<DateWiseEventViewModel> aList=new List<DateWiseEventViewModel>();
            foreach (var list in eventList)
            {
                DateWiseEventViewModel aModel=new DateWiseEventViewModel();
                aModel.EventName = list.EventName;
                aModel.EventVenue = list.EventVenue;
                aModel.EventDate = list.EventDate;
                aModel.StartTime = list.StartTime.ToString();
                aModel.EndTime = list.EndTime.ToString();
                aList.Add(aModel);

            }

            return Json(aList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ShowEventWiseReport()
        {
            if (Session["Id"] == null)
            {
                return RedirectToAction("Login", "Home")
                    ;
            }
            ViewBag.EventList = dbContext.Events.ToList();

            return View();
        }

        public ActionResult GetEventVisitorByEventId(int eventID)
        {
            var visitorList = dbContext.VisitorRegistrations.Where(m => m.EventId == eventID).ToList();
            return Json(visitorList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsVisitorEmailExist(string email)
        {
            bool isExist = false;
            if (dbContext.VisitorRegistrations.Any(o => o.VisitorEmail == email))
            {
                isExist = true;
            }
            return Json(!isExist, JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsVisitorContactNoExist(string contactNo)
        {
            bool isExist = false;
            if (dbContext.VisitorRegistrations.Any(o => o.VisitorContactNo == contactNo))
            {
                isExist = true;
            }
            return Json(!isExist, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ShowEventReportGenderWise()
        {
            if (Session["Id"] == null)
            {
                return RedirectToAction("Login", "Home")
                    ;
            }
            return View();
        }

        public ActionResult ShowDateWiseEventReportByGender(DateTime frmDate, DateTime edDate, string gender)
        {

            var query = dbContext.Events // your starting point - table in the "from" statement
                .Join(dbContext.VisitorRegistrations, // the source table of the inner join
                    post => post.EventId,
                    // Select the primary key (the first part of the "on" clause in an sql "join" statement)
                    meta => meta.EventId, // Select the foreign key (the second part of the "on" clause)
                    (post, meta) => new {Events = post, VisitorRegistrations = meta}) // selection
                .Where(
                    postAndMeta =>
                        postAndMeta.Events.EventDate >= frmDate && postAndMeta.Events.EventDate <= edDate &&
                        postAndMeta.VisitorRegistrations.Gender == gender).ToList();
            List<GenderWiseEventViewModel> alList = new List<GenderWiseEventViewModel>();
            foreach (var data in query)
            {
                GenderWiseEventViewModel model = new GenderWiseEventViewModel();
                model.EventId = data.Events.EventId;
                model.TotalVisitor = 1;
                bool containsItem = false;

                containsItem = alList.Any(item => item.EventId == model.EventId);
                if (containsItem)
                {
                    GenderWiseEventViewModel obj = alList.FirstOrDefault(x => x.EventId == model.EventId);

                    obj.EventName = data.Events.EventName;
                    obj.TotalVisitor += 1;
                    obj.EventDate = data.Events.EventDate.ToString();
                    DateTime a1 = DateTime.Now;
                    DateTime b1 = data.VisitorRegistrations.DateOfBirth;
                    var day1 = (a1 - b1).Days;
                    if (day1 <= 9125)
                    {
                        obj.ZeroToTwentyFiveCount += 1;
                    }
                    if (day1 > 9125 && day1 <= 14600)
                    {
                        obj.TwentySixToFortyCount += 1;
                    }
                    if (day1 > 14600)
                    {
                        obj.FortyAboveCount += 1;
                    }
                    int index = alList.IndexOf(alList.Where(x => x.EventId == obj.EventId).FirstOrDefault());
                    alList.RemoveAt(index);
                    alList.Add(obj);
                }
                else
                {
                    model.EventName = data.Events.EventName;
                    model.EventDate = data.Events.EventDate.ToString();
                    DateTime a = DateTime.Now;
                    DateTime b = data.VisitorRegistrations.DateOfBirth;
                    var day = (a - b).Days;
                    if (day <= 9125)
                    {
                        model.ZeroToTwentyFiveCount += 1;
                    }
                    if (day > 9125 && day <= 14600)
                    {
                        model.TwentySixToFortyCount += 1;
                    }
                    if (day > 14600)
                    {
                        model.FortyAboveCount += 1;
                    }

                    alList.Add(model);
                }




            }

            return Json(alList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Dashboard()
        {
            if (Session["Id"] == null)
            {
                return RedirectToAction("Login", "Home")
                    ;
                
            }
            var list = dbContext.Events.Where(x => x.EventDate == DateTime.Today).ToList();
            int totalEvent = list.Count;
            ViewBag.TotalEvent = totalEvent;
            var visitorList=dbContext.Events // your starting point - table in the "from" statement
                .Join(dbContext.VisitorRegistrations, // the source table of the inner join
                    post => post.EventId,
                    // Select the primary key (the first part of the "on" clause in an sql "join" statement)
                    meta => meta.EventId, // Select the foreign key (the second part of the "on" clause)
                    (post, meta) => new {Events = post, VisitorRegistrations = meta}) // selection
                .Where(
                    postAndMeta =>
                        postAndMeta.Events.EventDate ==DateTime.Today ).ToList();
            int visitorTotal = visitorList.Count;
            ViewBag.TotalVisitor = visitorTotal;
            int maleVisitor=0;
            int femaleVisitor=0;
            foreach (var visitor in visitorList)
            {
                if (visitor.VisitorRegistrations.Gender=="Male")
                {
                    maleVisitor += 1;
                }
                else
                {
                    femaleVisitor += 1;
                }
            }
            ViewBag.Male = maleVisitor;
            ViewBag.Female = femaleVisitor;

            return View();

        }
    }
}