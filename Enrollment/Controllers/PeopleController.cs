using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Enrollment.DAL;
using Enrollment.Models;

namespace Enrollment.Controllers
{
    public class PeopleController : Controller
    {
        private EnrollmentContext db = new EnrollmentContext();

        // GET: People
        public ActionResult Index()
        {
            var people = db.People.Include(p => p.Address).Include(p => p.Gender).Include(p => p.Participant);
            return View(people.ToList());
        }

        // GET: People/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.People.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        // GET: People/Create
        public ActionResult Create()
        {
            ViewBag.AddressID = new SelectList(db.Addresses, "AddressID", "Address1");
            ViewBag.GenderID = new SelectList(db.Genders, "GenderID", "GenderName");
            ViewBag.PersonID = new SelectList(db.Participants, "PersonID", "PersonID");
            return View();
        }

        // POST: People/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PersonID,FirstName,MiddleInitial,LastName,BirthDate,SSN,GenderID,HomePhone,CellPhone,Email,AddressID")] Person person)
        {
            if (ModelState.IsValid)
            {
                db.People.Add(person);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AddressID = new SelectList(db.Addresses, "AddressID", "Address1", person.AddressID);
            ViewBag.GenderID = new SelectList(db.Genders, "GenderID", "GenderName", person.GenderID);
            ViewBag.PersonID = new SelectList(db.Participants, "PersonID", "PersonID", person.PersonID);
            return View(person);
        }

        // GET: People/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.People.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            ViewBag.AddressID = new SelectList(db.Addresses, "AddressID", "Address1", person.AddressID);
            ViewBag.GenderID = new SelectList(db.Genders, "GenderID", "GenderName", person.GenderID);
            ViewBag.PersonID = new SelectList(db.Participants, "PersonID", "PersonID", person.PersonID);
            return View(person);
        }

        // POST: People/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PersonID,FirstName,MiddleInitial,LastName,BirthDate,SSN,GenderID,HomePhone,CellPhone,Email,AddressID")] Person person)
        {
            if (ModelState.IsValid)
            {
                db.Entry(person).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AddressID = new SelectList(db.Addresses, "AddressID", "Address1", person.AddressID);
            ViewBag.GenderID = new SelectList(db.Genders, "GenderID", "GenderName", person.GenderID);
            ViewBag.PersonID = new SelectList(db.Participants, "PersonID", "PersonID", person.PersonID);
            return View(person);
        }

        // GET: People/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.People.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Person person = db.People.Find(id);
            db.People.Remove(person);
            db.SaveChanges();
            return RedirectToAction("Index");
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
