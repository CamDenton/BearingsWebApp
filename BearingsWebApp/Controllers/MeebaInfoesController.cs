using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BearingsWebApp.Models;
using Newtonsoft.Json;
using Microsoft.AspNet.Identity;

namespace BearingsWebApp.Controllers
{
    public class MeebaInfoesController : Controller
    {
        private BearingsWebAppContext db = new BearingsWebAppContext();
        // Integers that handle the Pull of the Meeba chart
        int userOuter = 1;
        int userInner = 1;
        
        // GET: MeebaInfoes
        //The Index holds and handles the Meeba chart
        
        [Authorize]
        public ActionResult Index()
        {
            // Each var is a base integer for the points of the Meeba
            // Each point is relative to a category filled in by the user
            var userAppt = 1;
            var userSocial = 1;
            var userWork = 1;
            var userPersonal = 1;
            var userEvent = 1;
            var userOther = 1;

            // Gets the Id of the current user
            // Then uses that ID to filter out data from other
            // Users and only gather the entries of the currently
            // Logged in User
            var currentUser = User.Identity.GetUserId();
            var userMeeba = from user in db.MeebaInfoes
                            where user.userID == currentUser
                            select user;
            
            // Each of the foreach methods applies the databse integers 
            // To the controller counterpart
            foreach (var appointment in userMeeba)
            {
                userAppt += appointment.apptInt;
            }

            foreach (var social in userMeeba)
            {
                userSocial += social.socInt;
            }

            foreach (var work in userMeeba)
            {
                userWork += work.workInt;
            }

            foreach (var personal in userMeeba)
            {
                userPersonal += personal.persInt;
            }

            foreach (var evt in userMeeba)
            {
                userEvent += evt.evtInt;
            }

            foreach (var other in userMeeba)
            {
                userOther += other.otherInt;
            }

            foreach (var inner in userMeeba)
            {
                userInner += inner.innerInt;
            }

            foreach (var outer in userMeeba)
            {
                userOuter += outer.OuterInt;
            }

            // Saves the controller integers to ViewData values
            // To be access via the view
            ViewData["Appointments"] = userAppt;
            ViewData["Social"] = userSocial;
            ViewData["Work"] = userWork;
            ViewData["Events"] = userEvent;
            ViewData["Personal"] = userPersonal;
            ViewData["Other"] = userOther;
            ViewData["Inner"] = userInner;
            ViewData["Outer"] = userOuter;
            return View(userMeeba.ToList()); 
        }

        // GET: MeebaInfoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MeebaInfo meebaInfo = db.MeebaInfoes.Find(id);
            if (meebaInfo == null)
            {
                return HttpNotFound();
            }
            return View(meebaInfo);
        }

        // GET: MeebaInfoes/Create
        public ActionResult Create()
        {
            return View();

        }

        // POST: MeebaInfoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,itemName,category,pull,apptInt,workInt,socInt,evtInt,persInt,otherInt,innerInt,OuterInt")] MeebaInfo meebaInfo)
        {
            if (ModelState.IsValid)
            {

                db.MeebaInfoes.Add(meebaInfo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(meebaInfo);
        }

        // GET: MeebaInfoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MeebaInfo meebaInfo = db.MeebaInfoes.Find(id);
            if (meebaInfo == null)
            {
                return HttpNotFound();
            }
            return View(meebaInfo);
        }

        // POST: MeebaInfoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,itemName,category,pull,apptInt,workInt,socInt,evtInt,persInt,otherInt,innerInt,OuterInt")] MeebaInfo meebaInfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(meebaInfo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(meebaInfo);
        }

        // GET: MeebaInfoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MeebaInfo meebaInfo = db.MeebaInfoes.Find(id);
            if (meebaInfo == null)
            {
                return HttpNotFound();
            }
            return View(meebaInfo);
        }

        // POST: MeebaInfoes/Delete/5
        // Passes the id, category, and pull of the selected 
        // Database object to the controller for deletion
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, string category, string pull)
        {
           
            MeebaInfo meebaInfo = db.MeebaInfoes.Find(id);

            // Runs a switch statement with the category passed in
            // That will decrement the value of that category from the Meeba
            // If the object is holds the category 
            switch (category)
            {
                case "Social":
                    meebaInfo.socInt--;
                    break;

                case "Appointment":
                    meebaInfo.apptInt--;
                    break;

                case "Work":
                    meebaInfo.workInt--;
                    break;

                case "Events":
                    meebaInfo.evtInt--;
                    break;

                case "Other":
                    meebaInfo.otherInt--;
                    break;

                case "Personal":
                    meebaInfo.persInt--;
                    break;

            }

            switch (pull)
            {
                case "Inner":
                    meebaInfo.innerInt--;
                    break;

                case "Outer":
                    meebaInfo.OuterInt--;
                    break;
            }

            db.MeebaInfoes.Remove(meebaInfo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // Passes the users input from the View back to the controller 
        // To be saved into the database
        // However, if it is not a valid input
        // The user will be redirected to an error page 
        public ActionResult PostEvent([Bind(Include = "ID,itemName,category,pull")]MeebaInfo meeba)
        {
            // Passes the category into the switch statement
            // If the category is matches a case, it will increment
            // That category in the database
            switch (meeba.category)
            {
                case "Social":
                    meeba.socInt++;
                    break;

                case "Appointment":
                    meeba.apptInt++;
                    break;

                case "Work":
                    meeba.workInt++;
                    break;

                case "Events":
                    meeba.evtInt++;
                    break;

                case "Other":
                    meeba.otherInt++;
                    break;

                case "Personal":
                    meeba.persInt++;
                    break;

            }
            //Gets a list of inputs for the user
            //Based on the currently logged in user
            
            var currentUser = User.Identity.GetUserId();
            var userMeeba = from user in db.MeebaInfoes
                            where user.userID == currentUser
                            select user;

            // Applies the integer for inner and outer
            // Based on the integers in the database
            foreach (var outer in userMeeba)
            {
                userOuter += outer.OuterInt;
            }
            foreach (var inner in userMeeba)
            {
                userInner += inner.innerInt;
            }

            // Applies the integers to a ViewData value
            // To be accessed by the View
            ViewData["Outer"] = userOuter;
            ViewData["Inner"] = userInner;

          // Passes the pull into the switch statement
          // If the pull matches the case, increment 
          // The pull, and decrement its opposite
          // Value, so long as that value is greater 
          // Than 1
            switch (meeba.pull)
            {
                case "Outer":
                    meeba.OuterInt++;
                    if (userInner > 1)
                    {
                        meeba.innerInt--;
                    }
                    break;

                case "Inner":
                    meeba.innerInt++;
                    if (userOuter > 1)
                    {
                        meeba.OuterInt--;
                    }
                    break;

                    
            }

            meeba.userID = User.Identity.GetUserId();

            // If the input is valid for the Model
            // Save the input to the database
            if (ModelState.IsValid)
            {

                db.MeebaInfoes.Add(meeba);
                db.SaveChanges();
                return RedirectToAction("Index");
                
            }

            // If the input is incorrect,
            // Redirect to the error page
            if(!ModelState.IsValid)
            {
                return RedirectToAction("Error");
                
            }

            return new JsonResult() { Data = JsonConvert.SerializeObject(meeba.ID), JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }

        // Creates a Json formated object from the list 
        // Of events created by the currently logged in user
        // This is to pull that info from the controller
        // And use in the view 
        public JsonResult GetEvents()
        {
            var currentUser = User.Identity.GetUserId();
            var evts = from events in db.MeebaInfoes
                       where events.userID == currentUser
                       select new
                       {
                           events.itemName,
                           events.category,
                           events.pull

                       };
            var evtsOutput = JsonConvert.SerializeObject(evts.ToList());

            return Json(evtsOutput, JsonRequestBehavior.AllowGet);
        }

        // Returns the Error View
        public ActionResult Error()
        {
            return View();
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
