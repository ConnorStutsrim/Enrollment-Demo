using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Enrollment.Models
{
    public class Participant
    {
        [Key]
        [ForeignKey("Person")]
        public int PersonID { get; set; }
        public virtual Person Person { get; set; }

        public Guid IdentityID { get; set; }

        [ForeignKey("Employer")]
        public int? EmployerID { get; set; }
        public virtual Employer Employer { get; set; }

        [ForeignKey("WizardProgress")]
        public int WizardProgressID { get; set; }
        public virtual WizardProgress WizardProgress { get; set; }

        public virtual ICollection<EmploymentInformation> EmploymentInformation { get; set; }

        public virtual ICollection<Dependent> Dependents { get; set; }

        public virtual ICollection<Beneficiary> Beneficiaries { get; set; }
    }
}