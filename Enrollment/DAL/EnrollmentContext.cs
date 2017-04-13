using Enrollment.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Enrollment.DAL
{
    public class EnrollmentContext : DbContext
    {
        public EnrollmentContext(DbContextOptions<EnrollmentContext> options) : base(options) { }

        public DbSet<Gender> Genders { get; set; }

        public DbSet<RelationshipType> RelationshipTypes { get; set; }

        public DbSet<State> States { get; set; }

        public DbSet<Address> Addresses { get; set; }

        public DbSet<Person> People { get; set; }

        public DbSet<MaritalStatus> MaritalStatuses { get; set; }

        public DbSet<Beneficiary> Beneficiaries { get; set; }

        public DbSet<Dependent> Dependents { get; set; }

        public DbSet<OccupationCode> OccupationCodes { get; set; }

        public DbSet<Employer> Employers { get; set; }

        public DbSet<EmployerOccupationCode> EmployerOccupationCodes { get; set; }

        public DbSet<EmploymentInformation> EmploymentInformation { get; set; }

        public DbSet<WorkStatus> WorkStatuses { get; set; }

        public DbSet<WizardProgress> WizardProgresses { get; set; }

        public DbSet<Participant> Participants { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

        //    base.OnModelCreating(modelBuilder);
        //}

        public void SetWizardProgress(string actionResult, Guid userID)
        {
            //Guid userID = Guid.Parse(HttpContext.Current.User.Identity.GetUserId());
            Participants.Single(s => s.IdentityID == userID).WizardProgress = WizardProgresses.Single(s => s.ActionResult == actionResult);
            SaveChanges();
        }

        public void AddParticipant(Person person, Address address, int stateID, Guid userID)
        {
            AddPersonAddress(person, address, stateID);
            Participant participant = new Participant
            {
                Person = person,
                WizardProgress = new WizardProgress { ActionResult = "" },
                IdentityID = userID
            };
            Participants.Add(participant);
            SaveChanges();
        }

        public void AddPersonAddress(Person person, Address address, int stateID)
        {
            address.State = States.Find(stateID);
            person.FullName = person.FirstName + " " + person.MiddleInitial + " " + person.LastName;
            address.FullAddress = address.Address1 + " " + address.Address2 + ", " + address.City + ", " + address.State.StateName + ", " + address.ZipCode;
            Addresses.Add(address);
            person.Address = address;
            People.Add(person);
            SaveChanges();
        }

        public void AddDependent(Dependent dependent, Person person, int personID)
        {
            dependent.PersonID = person.PersonID;
            dependent.ParticipantID = personID;
            Dependents.Add(dependent);
            Participants.Find(personID).Dependents.Add(dependent);
            SaveChanges();
        }
    }
}
