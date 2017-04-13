using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Enrollment.Models
{
    public class WorkStatus
    {
        [Key]
        public int WorkStatusID { get; set; }

        [Display(Name = "Work Status")]
        public string WorkStatusName { get; set; }
    }
}