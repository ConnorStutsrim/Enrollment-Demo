using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Enrollment.Models
{
    public class MaritalStatus
    {
        [Key]
        [Display(Name = "Marital Status")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaritalStatusID { get; set; }

        [Display(Name = "Marital Status")]
        public string MaritalStatusName { get; set; }
    }
}