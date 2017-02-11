using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Enrollment.Models
{
    public class State
    {
        [Key]
        public int StateID { get; set; }

        public string StateAbbreviation { get; set; }

        public string StateName { get; set; }
    }
}