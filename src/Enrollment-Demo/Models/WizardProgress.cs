using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

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