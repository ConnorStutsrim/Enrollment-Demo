using Enrollment.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace Enrollment.ViewModels
{
    public class PersonAddressViewModel
    {
        public PersonAddressViewModel(EnrollmentContext db)
        {
            Person = new Person();
            Address = new Address { State = new State { } };
            GenderList = new SelectList(db.Genders, "GenderID", "GenderName");
            StateList = new SelectList(db.States, "StateID", "StateName");
        }

        public PersonAddressViewModel(Person person, Address address, EnrollmentContext db)
        {
            Person = person;
            Address = address;
            GenderList = new SelectList(db.Genders, "GenderID", "GenderName");
            StateList = new SelectList(db.States, "StateID", "StateName");
        }

        public Participant Participant { get; set; }

        public Person Person { get; set; }

        public Address Address { get; set; }

        public SelectList GenderList { get; set; }

        public SelectList StateList { get; set; }

        public void SetTestData()
        {
            Person = new Person
            {
                FirstName = "John",
                MiddleInitial = "G",
                LastName = "Doe",
                GenderID = 2,
                BirthDate = DateTime.Parse("1/1/2000"),
                SSN = "123-45-6789",
                CellPhone = "518-555-5555",
                HomePhone = "518-999-9999",
                Email = "johndoe@email.com"
            };

            Address = new Address
            {
                Address1 = "100 Main Street",
                City = "Albany",
                State = new State(),
                ZipCode = "12205",
                Country = "United States of America"
            };
        }
    }
}