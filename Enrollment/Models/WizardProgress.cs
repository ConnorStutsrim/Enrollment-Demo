using System.ComponentModel.DataAnnotations;

namespace Enrollment.Models
{
    public class WizardProgress
    {
        [Key]
        public int WizardProgressID { get; set; }

        public int Sequence { get; set; }

        public string ActionResult { get; set; }
    }
}