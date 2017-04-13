using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Enrollment.Models;
using Enrollment.DAL;

namespace Enrollment.ViewModels
{
    public class PortalIndexViewModel
    {
        public PortalIndexViewModel(int personID, EnrollmentContext db)
        {
            Person = db.People.Find(personID);
            Address = Person.Address;
            EmploymentInformation = new List<EmploymentInformation>();
            Person.Participant.EmploymentInformation.ToList().ForEach(s => EmploymentInformation.Add(s));
            Dependents = new List<Dependent>();
            Person.Participant.Dependents.ToList().ForEach(s => Dependents.Add(s));
            Beneficiaries = new List<Beneficiary>();
            Person.Participant.Beneficiaries.ToList().ForEach(s => Beneficiaries.Add(s));
        }

        public Person Person { get; set; }

        public Address Address { get; set; }

        public ICollection<EmploymentInformation> EmploymentInformation { get; set; }

        public ICollection<Dependent> Dependents { get; set; }

        public ICollection<Beneficiary> Beneficiaries { get; set; }
    }
}