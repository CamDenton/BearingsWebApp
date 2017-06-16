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
        int userOuter = 1;
        int userInner = 1;
        // GET: MeebaInfoes
        [Authorize]
        public ActionResult Index()
        {
            var userAppt = 1;
            var userSocial = 1;
            var userWork = 1;
            var userPersonal = 1;
            var userEvent = 1;
            var userOther = 1;

            var currentUser = User.Identity.GetUserId();
            var userMeeba = from user in db.MeebaInfoes
                            where user.userID == currentUser
                            select user;

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
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, string category, string pull)
        {
           
            MeebaInfo meebaInfo = db.MeebaInfoes.Find(id);
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
            db.MeebaInfoes.Remove(meebaInfo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult PostEvent([Bind(Include = "ID,itemName,category,pull")]MeebaInfo meeba)
        {
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
            //start of nonsense
            var currentUser = User.Identity.GetUserId();
            var userMeeba = from user in db.MeebaInfoes
                            where user.userID == currentUser
                            select user;


            foreach (var outer in userMeeba)
            {
                userOuter += outer.OuterInt;
            }
            foreach (var inner in userMeeba)
            {
                userInner += inner.innerInt;
            }

            ViewData["Outer"] = userOuter;
            ViewData["Inner"] = userInner;

            //if (meeba.pull == "Inner" && userOuter > 11)
            //{
            //    meeba.innerInt++; 

            //    ViewBag.inside = "This didn't work";
            //    meeba.OuterInt--;
            //}


            //if (meeba.pull == "Outer" && meeba.innerInt > 0)
            //{
            //    meeba.OuterInt++;
            //    meeba.innerInt--;
            //}
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

                    //default:
                    //    ViewBag.didNotWork = "This didn't work";
                    //    break;
            }

            meeba.userID = User.Identity.GetUserId();

            if (ModelState.IsValid)
            {

                db.MeebaInfoes.Add(meeba);
                db.SaveChanges();
                return RedirectToAction("Index");
                
            }
            return new JsonResult() { Data = JsonConvert.SerializeObject(meeba.ID), JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }
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
