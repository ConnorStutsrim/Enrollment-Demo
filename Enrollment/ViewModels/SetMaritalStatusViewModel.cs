using Enrollment.DAL;
using Enrollment.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

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