using Enrollment.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace Enrollment.ViewModels
{
    public class AddCurrentSpouseViewModel
    {
        public AddCurrentSpouseViewModel(EnrollmentContext db)
        {
            PersonAddressViewModel = new PersonAddressViewModel(db);
        }
        
        public PersonAddressViewModel PersonAddressViewModel { get; set; }

        public Address ParticipantAddress { get; set; }

        [Display(Name = "Date of Marriage")]
        [DataType(DataType.Date, ErrorMessage = "Date of Marriage must be a valid date")]
        public DateTime MarriageDate { get; set; }

        [Display(Name = "Health Insurance Company")]
        public string InsuranceCompany { get; set; }

        [Display(Name = "Company's Phone Number")]
        [Phone(ErrorMessage = "Please enter a valid phone number")]
        public string InsurancePhone { get; set; }

        [Display(Name = "Insurance ID Number")]
        public string InsuranceID { get; set; }
    }
}