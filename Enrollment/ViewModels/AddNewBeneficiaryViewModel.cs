using Enrollment.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Enrollment.ViewModels
{
    public class AddNewBeneficiaryViewModel
    {
        public AddNewBeneficiaryViewModel(EnrollmentContext db)
        {
            PersonAddressViewModel = new PersonAddressViewModel(db);
            RelationshipTypeList = new SelectList(db.RelationshipTypes, "RelationshipTypeID", "RelationshipTypeName");
        }

        public PersonAddressViewModel PersonAddressViewModel { get; set; }

        public Address ParticipantAddress { get; set; }

        public Beneficiary Beneficiary { get; set; }

        public SelectList RelationshipTypeList { get; set; }

        public string Relationship { get; set; }

        [Display(Name="Would you like to add any additional beneficiaries?")]
        public bool AdditionalBeneficiary { get; set; }
    }
}