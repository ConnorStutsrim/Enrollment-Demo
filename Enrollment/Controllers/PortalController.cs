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
using Enrollment.ViewModels;

namespace Enrollment.Controllers
{
    public class PortalController : UtilityController
    {
        // GET: Portal
        public ActionResult Index()
        {
            PortalIndexViewModel pivm = new PortalIndexViewModel(GetCurrentParticipant().PersonID, db);
            return View(pivm);
        }

        // GET: Portal/EditPerson/5
        public ActionResult EditPerson(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonAddressViewModel pavm = new PersonAddressViewModel(db.People.Find(id), db.People.Find(id).Address, db);
            return View("../Enroll/CreateParticipantPerson", pavm);
        }

        // POST: Portal/EditPerson/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPerson(
            [Bind(Prefix = "Person", Include = "PersonID,AddressID,FirstName,MiddleInitial,LastName,BirthDate,SSN,GenderID,HomePhone,CellPhone,Email")]Person person,
            [Bind(Prefix = "Address", Include = "AddressID,Address1,Address2,City,State,Province,ZipCode,PostalCode,Country")]Address address)
        {
            if (ModelState.IsValid)
            {
                db.Entry(person).State = EntityState.Modified;
                db.Entry(address).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            PersonAddressViewModel pavm = new PersonAddressViewModel(db.People.Find(person.PersonID), db.People.Find(person.PersonID).Address, db);
            return View("../Enroll/CreateParticipantPerson", pavm);
        }

        // GET: Portal/EditEmployer/5
        public ActionResult EditEmployer(int? id)
        {
            if (id == null || db.EmploymentInformation.Find(id) == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (db.EmploymentInformation.Find(id).EndDate == null)
            {
                AddCurrentEmployerViewModel acevm = new AddCurrentEmployerViewModel(db);
                acevm.Participant = GetCurrentParticipant();
                acevm.EmploymentInformation = db.EmploymentInformation.Find(id);
                return View("../Enroll/AddCurrentEmployer", acevm);
            }
            else
            {
                AddPreviousEmployerViewModel apevm = new AddPreviousEmployerViewModel(db);
                apevm.Participant = GetCurrentParticipant();
                apevm.EmploymentInformation = db.EmploymentInformation.Find(id);
                return View("../Enroll/AddPreviousEmployer", apevm);
            }
        }

        // POST: Portal/EditEmployer/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditEmployer(
            [Bind(Include = "EmploymentInformationID,EmployerID,HireDate,OccupationCodeID,WorkStatusID,WorkHours,EndDate")]EmploymentInformation employmentInformation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employmentInformation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            if (db.EmploymentInformation.Find(employmentInformation.EmploymentInformationID).EndDate == null)
            {
                AddCurrentEmployerViewModel acevm = new AddCurrentEmployerViewModel(db);
                acevm.Participant = GetCurrentParticipant();
                acevm.EmploymentInformation = db.EmploymentInformation.Find(employmentInformation.EmploymentInformationID);
                return View("../Enroll/AddCurrentEmployer", acevm);
            }
            else
            {
                AddPreviousEmployerViewModel apevm = new AddPreviousEmployerViewModel(db);
                apevm.Participant = GetCurrentParticipant();
                apevm.EmploymentInformation = db.EmploymentInformation.Find(employmentInformation.EmploymentInformationID);
                return View("../Enroll/AddPreviousEmployer", apevm);
            }
        }
    }
}
