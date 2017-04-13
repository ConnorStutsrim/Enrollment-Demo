using Enrollment.DAL;
using Enrollment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Enrollment.ViewModels
{
    public class SetMaritalStatusViewModel
    {
        public SetMaritalStatusViewModel(EnrollmentContext db)
        {
            MaritalStatusList = new SelectList(db.MaritalStatuses, "MaritalStatusID", "MaritalStatusName");
        }
        
        public MaritalStatus MaritalStatus { get; set; }

        public SelectList MaritalStatusList { get; set; } 
    }
}