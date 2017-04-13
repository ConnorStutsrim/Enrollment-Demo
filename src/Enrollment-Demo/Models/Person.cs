using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Enrollment.Models
{
    public class Person
    {
        [Key]
        public int PersonID { get; set; }

        [Required]
        [Display(Name = "First")]
        [StringLength(15, ErrorMessage = "First name must be 15 characters or less")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Middle")]
        [StringLength(1)]
        public string MiddleInitial { get; set; }

        [Required]
        [Display(Name = "Last")]
        [StringLength(20, ErrorMessage = "Last name must be 20 characters or less")]
        public string LastName { get; set; }

        [Display(Name = "Name")]
        public string FullName { get; set; }

        [Required]
        [Display(Name = "Birth Date")]
        [DataType(DataType.Date, ErrorMessage = "Birth date must be a valid date")]
        public DateTime BirthDate { get; set; }

        [Required]
        [Display(Name = "SSN")]
        [RegularExpression(@"^\d{9}|\d{3}-\d{2}-\d{4}$", ErrorMessage = "Invalid Social Security Number")]
        public string SSN { get; set; }

        [Required]
        [Display(Name="Gender")]
        public int GenderID { get; set; }
        public virtual Gender Gender { get; set; }

        [Required]
        [Display(Name = "Home")]
        [RegularExpression(@"^\d{10}|\d{3}-\d{3}-\d{4}$", ErrorMessage = "Please enter a valid phone number")]
        public string HomePhone { get; set; }

        [Required]
        [Display(Name = "Cell")]
        [RegularExpression(@"^\d{10}|\d{3}-\d{3}-\d{4}$", ErrorMessage = "Please enter a valid phone number")]
        public string CellPhone { get; set; }

        [Required]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Address")]
        [ForeignKey("Address")]
        public int AddressID { get; set; }
        public virtual Address Address { get; set; }

        public virtual Participant Participant { get; set; }

        public ICollection<Dependent> Dependents { get; set; }

        public ICollection<Beneficiary> Beneficiary { get; set; }
    }
}