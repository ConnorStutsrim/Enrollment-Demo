using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Enrollment.Models;
using System.Web.Mvc;

namespace Enrollment.ViewModels
{
    public class PersonViewModel
    {
        public Person Person { get; set; }

        public SelectList GenderList { get; set; }
    }
}