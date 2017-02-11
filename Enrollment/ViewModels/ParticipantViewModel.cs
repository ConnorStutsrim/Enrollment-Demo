using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Enrollment.Models;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Enrollment.ViewModels
{
    public class ParticipantViewModel
    {
        public Participant Participant { get; set; }

        public SelectList EmployerList { get; set; }

        public SelectList WorkStatusList { get; set; }

        [Display(Name = "Were you previously employed by a pension facility?")]
        public bool PreviousPensionEmployers { get; set; }

    }
}