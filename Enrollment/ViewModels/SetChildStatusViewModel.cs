using Enrollment.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Enrollment.ViewModels
{
    public class SetChildStatusViewModel
    {
        [Display(Name = "Do you have any children?")]

        public bool hasChildren { get; set; }
    }
}