using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Enrollment.Models
{
    public class EmploymentInformation
    {
        [Key]
        public int EmploymentInformationID { get; set; }

        [ForeignKey("Employer")]
        [Display(Name="Employer")]
        public int EmployerID { get; set; }
        public virtual Employer Employer { get; set; }

        [Display(Name = "Employment Date")]
        [DataType(DataType.Date, ErrorMessage = "Employment date must be a valid date")]
        public DateTime HireDate { get; set; }

        [ForeignKey("OccupationCode")]
        [Display(Name="Occupation Code")]
        public int OccupationCodeID { get; set; }
        public virtual OccupationCode OccupationCode { get; set; }

        [ForeignKey("WorkStatus")]
        [Display(Name="Work Status")]
        public int? WorkStatusID { get; set; }
        public virtual WorkStatus WorkStatus { get; set; }

        [Display(Name="Weekly Hours")]
        public int WorkHours { get; set; }

        [Display(Name = "End Date")]
        [DataType(DataType.Date, ErrorMessage = "End date must be a valid date")]
        public DateTime? EndDate { get; set; }
    }
}