using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Enrollment.Models
{
    public class Dependent
    {
        [Key]
        public int DependentID { get; set; }

        public int PersonID { get; set; }
        public virtual Person Person { get; set; }

        [Display(Name = "Health Insurance Company")]
        public string InsuranceCompany { get; set; }

        [Display(Name = "Company's Phone Number")]
        [Phone(ErrorMessage = "Please enter a valid phone number")]
        public string InsurancePhone { get; set; }

        [Display(Name = "Insurance ID Number")]
        public string InsuranceID { get; set; }

        [Display(Name = "Date of Marriage")]
        [DataType(DataType.Date, ErrorMessage = "Date of Marriage must be a valid date")]
        public DateTime? MarriageDate { get; set; }

        [Display(Name = "Date of Divorce")]
        [DataType(DataType.Date, ErrorMessage = "Date of Divorce must be a valid date")]
        public DateTime? DivorceDate { get; set; }

        [Display(Name = "Spouse's Date of Death")]
        [DataType(DataType.Date, ErrorMessage = "Date of Death must be a valid date")]
        public DateTime? DeathDate { get; set; }

        [ForeignKey("Participant")]
        public int ParticipantID { get; set; }
        public virtual Participant Participant { get; set; }
    }
}