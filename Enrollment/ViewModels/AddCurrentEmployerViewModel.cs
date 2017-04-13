using Enrollment.DAL;
using Enrollment.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Enrollment.ViewModels
{
    public class AddCurrentEmployerViewModel
    {
        public AddCurrentEmployerViewModel(EnrollmentContext db)
        {
            EmployerList = new SelectList(db.Employers.OrderBy(x => x.EmployerName), "EmployerID", "EmployerName");
            WorkStatusList = new SelectList(db.WorkStatuses, "WorkStatusID", "WorkStatusName");
            OccupationCodeList = new SelectList(db.OccupationCodes.Where(x => x.OccupationCodeID == 0), "OccupationCodeID", "OccupationCodeName"); //Empty list initially
            Participant = new Participant();
            EmploymentInformation = new EmploymentInformation();
        }

        public AddCurrentEmployerViewModel(EnrollmentContext db, EmploymentInformation ei)
        {
            EmployerList = new SelectList(db.Employers.OrderBy(x => x.EmployerName), "EmployerID", "EmployerName");
            WorkStatusList = new SelectList(db.WorkStatuses, "WorkStatusID", "WorkStatusName");
            if (ei.EmployerID==0)
                OccupationCodeList = new SelectList(db.OccupationCodes.Where(x => x.OccupationCodeID == 0), "OccupationCodeID", "OccupationCodeName"); //Empty list initially
            else
                OccupationCodeList = new SelectList(db.Employers.Find(ei.EmployerID).EmployerOccupationCodes.OrderBy(x => x.OccupationCodeName), "OccupationCodeID", "OccupationCodeName"); //Empty list initially
            Participant = new Participant();
            EmploymentInformation = new EmploymentInformation();
        }

        public Participant Participant { get; set; }

        public EmploymentInformation EmploymentInformation { get; set; }

        [Display(Name="Employer")]
        public SelectList EmployerList { get; set; }

        public SelectList WorkStatusList { get; set; }

        public SelectList OccupationCodeList { get; set; }

        [Required]
        [Display(Name = "Were you previously employed by a pension facility?")]
        public bool PreviousPensionEmployers { get; set; }

    }
}