using Enrollment.DAL;
using Enrollment.Models;
using Enrollment.ViewModels;
using Microsoft.AspNet.Identity;
using System.Web;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Enrollment.Controllers
{
    [Authorize]
    public class EnrollController : UtilityController
    {
        private RedirectToRouteResult RedirectToActionAndSetProgress(string actionName)
        {
            db.SetWizardProgress(actionName);
            return RedirectToAction(actionName);
        }

        private RedirectToRouteResult RedirectToPortal()
        {
            db.SetWizardProgress("Portal");
            return RedirectToAction("Index", "Portal", new { area = "" });
        }

        [HttpGet]
        public JsonResult GetOccupationCodes(int employerID)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            Employer employer = db.Employers.Find(employerID);
            foreach (EmployerOccupationCode employerOccupationCode in db.Employers.Find(employerID).EmployerOccupationCodes.OrderBy(x => x.OccupationCode.OccupationCodeName))
            {
                items.Add(new SelectListItem { Text = employerOccupationCode.OccupationCode.OccupationCodeName, Value = employerOccupationCode.OccupationCode.OccupationCodeID.ToString() });
            }
            return Json(items, JsonRequestBehavior.AllowGet);
        }

        // GET: Enroll/Redirect
        public ActionResult Redirect()
        {
            Guid userID = Guid.Parse(User.Identity.GetUserId());
            if (db.Participants.SingleOrDefault(s => s.IdentityID == userID) == null) return RedirectToAction("CreateParticipantPerson");
            else if (GetCurrentParticipant().WizardProgress.ActionResult == "Portal") return RedirectToPortal();
            else return RedirectToAction(GetCurrentParticipant().WizardProgress.ActionResult);
        }

        // GET: Enroll/CreateParticipantPerson
        public ActionResult CreateParticipantPerson()
        {
            PersonAddressViewModel pavm = new PersonAddressViewModel(db);
#if DEBUG
            pavm.SetTestData(); 
#endif
            pavm.Address.State.StateID = db.States.Single(s => s.StateAbbreviation == "NY").StateID;
            return View(pavm);
        }

        // POST: Enroll/CreateParticipantPerson
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateParticipantPerson(
            [Bind(Prefix = "Person", Include = "FirstName,MiddleInitial,LastName,BirthDate,SSN,GenderID,HomePhone,CellPhone,Email")]Person person,
            [Bind(Prefix = "Address", Include = "Address1,Address2,City,Province,ZipCode,PostalCode,State.StateID,Country")]Address address,
            [Bind(Prefix = "Address.State", Include = "StateID")]State state)
        {
            if (ModelState.IsValid)
            {
                db.AddParticipant(person, address, state.StateID);
                return RedirectToActionAndSetProgress("AddCurrentEmployer");
            }
            PersonAddressViewModel pavm = new PersonAddressViewModel(person, address, db);
            return View(pavm);
        }

        // GET: Enroll/AddCurrentEmployer
        public ActionResult AddCurrentEmployer()
        {
            AddCurrentEmployerViewModel acevm = new AddCurrentEmployerViewModel(db);
#if DEBUG
            acevm.EmploymentInformation.EmployerID = 3;
            acevm.EmploymentInformation.OccupationCodeID = 2;
            acevm.EmploymentInformation.HireDate = DateTime.Parse("1/1/2010");//TODO Delete test data
            acevm.EmploymentInformation.WorkStatusID = 1;
            acevm.EmploymentInformation.WorkHours = 50;
            acevm.PreviousPensionEmployers = true;
#endif
            return View(acevm);
        }

        // POST: Enroll/AddCurrentEmployer
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCurrentEmployer(
            [Bind(Include="EmployerID,HireDate,OccupationCodeID,WorkStatusID,WorkHours")]EmploymentInformation employmentInformation,
            [Bind]bool previousPensionEmployers)
        {
            if(ModelState.IsValid)
            {
                db.EmploymentInformation.Add(employmentInformation);
                GetCurrentParticipant().EmploymentInformation.Add(employmentInformation);
                GetCurrentParticipant().EmployerID = employmentInformation.EmployerID;
                db.SaveChanges();
                if (previousPensionEmployers) return RedirectToActionAndSetProgress("AddPreviousEmployer");
                else return RedirectToActionAndSetProgress("SetMaritalStatus");
            }
            AddCurrentEmployerViewModel acevm = new AddCurrentEmployerViewModel(db, employmentInformation);
            return View(acevm);
        }

        // GET: Enroll/AddPreviousEmployer
        public ActionResult AddPreviousEmployer()
        {
            AddPreviousEmployerViewModel apevm = new AddPreviousEmployerViewModel(db);
#if DEBUG
            apevm.EmploymentInformation.EmployerID = 1;
            apevm.EmploymentInformation.OccupationCodeID = 1;
            apevm.EmploymentInformation.HireDate = DateTime.Parse("1/1/2010");
            apevm.EmploymentInformation.EndDate = DateTime.Parse("1/1/2012");
            apevm.EmploymentInformation.WorkHours = 50;
#endif
            return View(apevm);
        }

        // POST: Enroll/AddPreviousEmployer
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddPreviousEmployer(
            [Bind(Include = "EmployerID,HireDate,OccupationCodeID,WorkHours,EndDate")]EmploymentInformation employmentInformation,
            [Bind]bool previousPensionEmployers)
        {
            if (ModelState.IsValid)
            {
                db.EmploymentInformation.Add(employmentInformation);
                GetCurrentParticipant().EmploymentInformation.Add(employmentInformation);
                db.SaveChanges();
                if (previousPensionEmployers) return RedirectToActionAndSetProgress("AddPreviousEmployer");
                else return RedirectToActionAndSetProgress("SetMaritalStatus");
            }
            AddPreviousEmployerViewModel apevm = new AddPreviousEmployerViewModel(db, employmentInformation);
            return View(apevm);
        }

        // GET: Enroll/SetMaritalStatus
        public ActionResult SetMaritalStatus()
        {
            SetMaritalStatusViewModel smsvm = new SetMaritalStatusViewModel(db);
            return View(smsvm);
        }

        // POST: Enroll/SetMaritalStatus
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SetMaritalStatus([Bind(Include = "MaritalStatusID")]MaritalStatus maritalStatus)
        {
            if (ModelState.IsValid)
            {
                int planType = (int)GetCurrentParticipant().Employer.PlanType;
                if (planType == 0) //Benefits
                {
                    if (maritalStatus.MaritalStatusID == 4) return RedirectToActionAndSetProgress("SetChildStatus"); //Single
                    else if (maritalStatus.MaritalStatusID == 2) return RedirectToActionAndSetProgress("AddCurrentSpouse"); //Married
                    else if (maritalStatus.MaritalStatusID == 1) return RedirectToActionAndSetProgress("SetChildStatus"); //Divorced
                    else if (maritalStatus.MaritalStatusID == 5) return RedirectToActionAndSetProgress("SetChildStatus"); //Widowed
                }
                else if (planType == 1) //Pension
                {
                    if (maritalStatus.MaritalStatusID == 4) return RedirectToPortal(); //Single
                    else if (maritalStatus.MaritalStatusID == 2) return RedirectToActionAndSetProgress("AddCurrentSpouse"); //Married
                    else if (maritalStatus.MaritalStatusID == 1) return RedirectToActionAndSetProgress("AddDivorceSpouse"); //Divorced
                    else if (maritalStatus.MaritalStatusID == 5) return RedirectToActionAndSetProgress("AddDeceaseSpouse"); //Widowed
                }
                else if (planType == 2) //Joint
                {
                    if (maritalStatus.MaritalStatusID == 4) return RedirectToActionAndSetProgress("SetChildStatus"); //Single
                    else if (maritalStatus.MaritalStatusID == 2) return RedirectToActionAndSetProgress("AddCurrentSpouse"); //Married
                    else if (maritalStatus.MaritalStatusID == 1) return RedirectToActionAndSetProgress("AddDivorceSpouse"); //Divorced
                    else if (maritalStatus.MaritalStatusID == 5) return RedirectToActionAndSetProgress("AddDeceaseSpouse"); //Widowed
                }
                
            }
            SetMaritalStatusViewModel smsvm = new SetMaritalStatusViewModel(db);
            return View(smsvm);
        }

        // GET: Enroll/AddCurrentSpouse
        public ActionResult AddCurrentSpouse()
        {
            AddCurrentSpouseViewModel acsvm = new AddCurrentSpouseViewModel(db);
            acsvm.ParticipantAddress = GetCurrentParticipant().Person.Address;
#if DEBUG
            acsvm.PersonAddressViewModel.SetTestData();
            acsvm.MarriageDate = DateTime.Parse("12/25/2012");
            acsvm.InsuranceCompany = "United Healthcare";
            acsvm.InsurancePhone = "555 555-5555";
            acsvm.InsuranceID = "123456789";
#endif
            return View(acsvm);
        }

        // POST: Enroll/AddCurrentSpouse
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCurrentSpouse(
            [Bind(Prefix = "PersonAddressViewModel.Person", Include = "FirstName,MiddleInitial,LastName,BirthDate,SSN,GenderID,HomePhone,CellPhone,Email")]Person person,
            [Bind(Prefix = "PersonAddressViewModel.Address", Include = "Address1,Address2,City,Province,ZipCode,PostalCode,Country")]Address address,
            [Bind(Include= "MarriageDate,InsuranceCompany,InsurancePhone,InsuranceID")]Dependent dependent, 
            [Bind(Prefix = "PersonAddressViewModel.Address.State", Include = "StateID")]State state)
        {
            if (ModelState.IsValid)
            {
                int personID = GetCurrentParticipant().PersonID;
                int planType = (int)db.Participants.Find(personID).Employer.PlanType;
                db.AddPersonAddress(person, address, state.StateID);
                db.AddDependent(dependent, person, personID);
                if (planType == 1) return RedirectToPortal();
                else return RedirectToActionAndSetProgress("SetChildStatus");
            }
            AddCurrentSpouseViewModel acsvm = new AddCurrentSpouseViewModel(db);
            acsvm.ParticipantAddress = GetCurrentParticipant().Person.Address;
            return View(acsvm);
        }

        // GET: Enroll/AddDivorceSpouse
        public ActionResult AddDivorceSpouse()
        {
            AddDivorceSpouseViewModel adsvm = new AddDivorceSpouseViewModel(db);
            adsvm.ParticipantAddress = GetCurrentParticipant().Person.Address;
#if DEBUG
            adsvm.PersonAddressViewModel.SetTestData();
            adsvm.MarriageDate = DateTime.Parse("12/25/2012");
            adsvm.DivorceDate = DateTime.Parse("12/25/2014");
#endif
            return View(adsvm);
        }

        // POST: Enroll/AddDivorceSpouse
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddDivorceSpouse(
            [Bind(Prefix = "PersonAddressViewModel.Person", Include = "FirstName,MiddleInitial,LastName,BirthDate,SSN,GenderID,HomePhone,CellPhone,Email")]Person person,
            [Bind(Prefix = "PersonAddressViewModel.Address", Include = "Address1,Address2,City,Province,ZipCode,PostalCode,Country")]Address address,
            [Bind(Include = "MarriageDate,DivorceDate")]Dependent dependent, [Bind(Prefix = "PersonAddressViewModel.Address.State", Include = "StateID")]State state)
        {
            if (ModelState.IsValid)
            {
                int personID = GetCurrentParticipant().PersonID;
                int planType = (int)db.Participants.Find(personID).Employer.PlanType;
                db.AddPersonAddress(person, address, state.StateID);
                db.AddDependent(dependent, person, personID);
                if (planType == 2) return RedirectToActionAndSetProgress("SetChildStatus");
                else return RedirectToPortal();
            }
            AddDivorceSpouseViewModel adsvm = new AddDivorceSpouseViewModel(db);
            adsvm.ParticipantAddress = GetCurrentParticipant().Person.Address;
            return View(adsvm);
        }

        // GET: Enroll/AddDeceaseSpouse
        public ActionResult AddDeceaseSpouse()
        {
            AddDeceaseSpouseViewModel adsvm = new AddDeceaseSpouseViewModel(db);
            adsvm.ParticipantAddress = GetCurrentParticipant().Person.Address;
#if DEBUG
            adsvm.PersonAddressViewModel.SetTestData();
            adsvm.MarriageDate = DateTime.Parse("12/25/2012");
            adsvm.DeathDate = DateTime.Parse("12/25/2014");
#endif
            return View(adsvm);
        }

        // POST: Enroll/AddDeceaseSpouse
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddDeceaseSpouse(
            [Bind(Prefix = "PersonAddressViewModel.Person", Include = "FirstName,MiddleInitial,LastName,BirthDate,SSN,GenderID,HomePhone,CellPhone,Email")]Person person,
            [Bind(Prefix = "PersonAddressViewModel.Address", Include = "Address1,Address2,City,Province,ZipCode,PostalCode,Country")]Address address,
            [Bind(Include = "MarriageDate,DeathDate")]Dependent dependent, [Bind(Prefix = "PersonAddressViewModel.Address.State", Include = "StateID")]State state)
        {
            if (ModelState.IsValid)
            {
                int personID = GetCurrentParticipant().PersonID;
                int planType = (int)db.Participants.Find(personID).Employer.PlanType;
                db.AddPersonAddress(person, address, state.StateID);
                db.AddDependent(dependent, person, personID);
                if (planType == 2) return RedirectToActionAndSetProgress("SetChildStatus");
                else return RedirectToPortal();
            }
            AddDeceaseSpouseViewModel adsvm = new AddDeceaseSpouseViewModel(db);
            adsvm.ParticipantAddress = GetCurrentParticipant().Person.Address;
            return View(adsvm);
        }

        // GET: Enroll/SetChildStatus
        public ActionResult SetChildStatus()
        {
            SetChildStatusViewModel scsvm = new SetChildStatusViewModel();
            return View(scsvm);
        }

        // POST: Enroll/SetChildStatus()
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SetChildStatus([Bind]bool hasChildren)
        {
            if(ModelState.IsValid)
            {
                if (hasChildren) return RedirectToActionAndSetProgress("AddChild");
                else return RedirectToActionAndSetProgress("AddBeneficiaryType");
            }
            SetChildStatusViewModel scsvm = new SetChildStatusViewModel();
            return View(scsvm);
        }

        // GET: Enroll/AddChild
        public ActionResult AddChild()
        {
            AddChildViewModel acvm = new AddChildViewModel(db);
            acvm.ParticipantAddress = GetCurrentParticipant().Person.Address;
#if DEBUG
            acvm.PersonAddressViewModel.SetTestData();
            acvm.InsuranceCompany = "United Healthcare";
            acvm.InsurancePhone = "555 555-5555";
            acvm.InsuranceID = "123456789";
#endif
            return View(acvm);
        }

        // POST: Enroll/AddChild
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddChild(
            [Bind(Prefix = "PersonAddressViewModel.Person", Include = "FirstName,MiddleInitial,LastName,BirthDate,SSN,GenderID,HomePhone,CellPhone,Email")]Person person,
            [Bind(Prefix = "PersonAddressViewModel.Address", Include = "Address1,Address2,City,Province,ZipCode,PostalCode,Country")]Address address,
            [Bind]bool additionalChildren, [Bind(Include = "InsuranceCompany,InsurancePhone,InsuranceID")]Dependent dependent,
            [Bind(Prefix = "PersonAddressViewModel.Address.State", Include = "StateID")]State state)
        {
            if(ModelState.IsValid)
            {
                int personID = GetCurrentParticipant().PersonID;
                db.AddPersonAddress(person, address, state.StateID);
                db.AddDependent(dependent, person, personID);
                if (additionalChildren) return RedirectToActionAndSetProgress("AddChild");
                else return RedirectToActionAndSetProgress("AddBeneficiaryType");
            }
            AddChildViewModel acvm = new AddChildViewModel(db);
            acvm.ParticipantAddress = GetCurrentParticipant().Person.Address;
            return View(acvm);
        }

        // GET: Enroll/AddBeneficiaryType
        public ActionResult AddBeneficiaryType()
        {
            AddBeneficiaryTypeViewModel abtvm = new AddBeneficiaryTypeViewModel();
            int userID = GetCurrentParticipant().PersonID;
            var excludedDependents = new List<Dependent>();
            foreach (Dependent dep in db.Participants.Find(userID).Dependents)
            {
                foreach (Beneficiary ben in db.Participants.Find(userID).Beneficiaries)
                {
                    if (ben.PersonID == dep.PersonID) excludedDependents.Add(dep);
                }
            }
            var remainingDependents = new List<Dependent>();
            abtvm.RemainingDependentsCount = new List<Dependent>(db.Participants.Find(userID).Dependents.Except(excludedDependents)).Count();
            return View(abtvm);
        }

        // POST Enroll/AddBeneficiaryType
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddBeneficiaryType(string submitButton)
        {
            if (submitButton == "New") return RedirectToActionAndSetProgress("AddNewBeneficiary");
            else if (submitButton == "Existing") return RedirectToActionAndSetProgress("AddExistingBeneficiary");
            else if (submitButton == "None") return RedirectToPortal();
            else
            {
                AddBeneficiaryTypeViewModel abtvm = new AddBeneficiaryTypeViewModel();
                return View(abtvm);
            }
        }

        // GET: Enroll/AddNewBeneficiary
        public ActionResult AddNewBeneficiary()
        {
            AddNewBeneficiaryViewModel anbvm = new AddNewBeneficiaryViewModel(db);
            anbvm.ParticipantAddress = GetCurrentParticipant().Person.Address;
#if DEBUG
            anbvm.PersonAddressViewModel.SetTestData();
#endif
            return View(anbvm);
        }

        // POST: Enroll/AddNewBeneficiary
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddNewBeneficiary(
            [Bind(Include = "PersonID,RelationshipTypeID")]Beneficiary beneficiary, [Bind(Include = "additionalBeneficiary")]bool additionalBeneficiary,
            [Bind(Prefix = "PersonAddressViewModel.Person", Include = "FirstName,MiddleInitial,LastName,BirthDate,SSN,GenderID,HomePhone,CellPhone,Email")]Person person,
            [Bind(Prefix = "PersonAddressViewModel.Address", Include = "Address1,Address2,City,Province,ZipCode,PostalCode,Country")]Address address,
            [Bind(Prefix = "PersonAddressViewModel.Address.State", Include = "StateID")]State state)
        {
            if (ModelState.IsValid)
            {
                int personID = GetCurrentParticipant().PersonID;
                db.AddPersonAddress(person, address, state.StateID);
                Beneficiary newBeneficiary = new Beneficiary { PersonID = person.PersonID, ParticipantID = personID, RelationshipType = db.RelationshipTypes.Find(beneficiary.RelationshipTypeID) };
                db.Beneficiaries.Add(newBeneficiary);
                db.Participants.Find(personID).Beneficiaries.Add(newBeneficiary);
                db.SaveChanges();
                if (additionalBeneficiary) return RedirectToActionAndSetProgress("AddBeneficiaryType");
                else return RedirectToPortal();
            }
            AddNewBeneficiaryViewModel anbvm = new AddNewBeneficiaryViewModel(db);
            anbvm.ParticipantAddress = GetCurrentParticipant().Person.Address;
            return View(anbvm);
        }

        // GET: Enroll/AddExistingBeneficiary
        public ActionResult AddExistingBeneficiary()
        {
            AddExistingBeneficiaryViewModel aebvm = new AddExistingBeneficiaryViewModel(db);
            return View(aebvm);
        }

        // POST: Enroll/AddExistingBeneficiary
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddExistingBeneficiary([Bind(Include = "PersonID,RelationshipTypeID")]Beneficiary beneficiary, 
            [Bind(Include = "additionalBeneficiary")]bool additionalBeneficiary)
        {
            if (ModelState.IsValid)
            {
                int personID = GetCurrentParticipant().PersonID;
                beneficiary.ParticipantID = personID;
                beneficiary.RelationshipType = db.RelationshipTypes.Find(beneficiary.RelationshipTypeID);
                db.Beneficiaries.Add(beneficiary);
                db.Participants.Find(personID).Beneficiaries.Add(beneficiary);
                db.SaveChanges();
                if (additionalBeneficiary) return RedirectToActionAndSetProgress("AddBeneficiaryType");
                else return RedirectToPortal();
            }
            AddExistingBeneficiaryViewModel aebvm = new AddExistingBeneficiaryViewModel(db);
            return View(aebvm);
        }
        
    }
}