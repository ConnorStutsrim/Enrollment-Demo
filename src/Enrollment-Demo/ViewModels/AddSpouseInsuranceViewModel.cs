using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Enrollment.ViewModels
{
    public class AddSpouseInsuranceViewModel
    {
        [Display(Name = "Spouse's Health Insurance Company")]
        public string InsuranceCompany { get; set; }

        [Display(Name = "Company's Telephone Number")]
        [Phone(ErrorMessage = "Please enter a valid phone number")]
        public string InsurancePhone { get; set; }

        [Display(Name = "Spouse's Insurance ID Number")]
        public string InsuranceID { get; set; }
    }
}