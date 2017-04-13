using Enrollment.DAL;
using Enrollment.Models;
using System.ComponentModel.DataAnnotations;

namespace Enrollment.ViewModels
{
    public class AddChildViewModel
    {
        public AddChildViewModel(EnrollmentContext db)
        {
            PersonAddressViewModel = new PersonAddressViewModel(db);
        }
        
        public PersonAddressViewModel PersonAddressViewModel { get; set; }

        public Address ParticipantAddress { get; set; }

        [Display(Name = "Health Insurance Company")]
        public string InsuranceCompany { get; set; }

        [Display(Name = "Company's Phone Number")]
        [Phone(ErrorMessage = "Please enter a valid phone number")]
        public string InsurancePhone { get; set; }

        [Display(Name = "Insurance ID Number")]
        public string InsuranceID { get; set; }

        [Display(Name = "Do you have any additional children?")]
        public bool AdditionalChildren { get; set; }
    }
}