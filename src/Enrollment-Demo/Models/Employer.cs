using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Enrollment.Models
{
    public enum PlanType
    {
        Benefits, Pension, Joint
    }
    public class Employer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EmployerID { get; set; }

        public string EmployerName { get; set; }

        public virtual ICollection<EmployerOccupationCode> EmployerOccupationCodes { get; set; }

        public PlanType PlanType { get; set; }

    }
}