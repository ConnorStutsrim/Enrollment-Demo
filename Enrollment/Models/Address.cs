using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Enrollment.Models
{
    public class Address
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int AddressID { get; set; }

        [Display(Name = "Address")]
        public string FullAddress { get; set; }

        [Required]
        [Display(Name = "Address")]
        public string Address1 { get; set; }

        [Display(Name = "line 2")]
        public string Address2 { get; set; }

        [Display(Name = "City")]
        public string City { get; set; }

        [Display(Name = "State")]
        public int StateID { get; set; }
        public State State { get; set; }

        [Display(Name = "Province")]
        public string Province { get; set; }

        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; }

        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }

        [Display(Name = "Country")]
        public string Country { get; set; }

        public virtual ICollection<Person> People { get; set; }
    }
}