using Enrollment.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Enrollment.ViewModels
{
    public class AddPreviousEmployerViewModel
    {
        public AddPreviousEmployerViewModel(EnrollmentContext db)
        {
            EmployerList = new SelectList(db.Employers.OrderBy(x => x.EmployerName), "EmployerID", "EmployerName");
            OccupationCodeList = new SelectList(db.OccupationCodes.Where(x => x.OccupationCodeID == 0), "OccupationCodeID", "OccupationCodeName"); //Empty list initially
            //OccupationCodeList = new SelectList(db.OccupationCodes.OrderBy(x => x.OccupationCodeName), "OccupationCodeID", "OccupationCodeName");
            Participant = new Participant();
            EmploymentInformation = new EmploymentInformation();
            EmploymentInformation.OccupationCode = new OccupationCode();
        }

        public AddPreviousEmployerViewModel(EnrollmentContext db, EmploymentInformation ei)
        {
            EmployerList = new SelectList(db.Employers.OrderBy(x => x.EmployerName), "EmployerID", "EmployerName");
            if (ei.EmployerID == 0)
                OccupationCodeList = new SelectList(db.OccupationCodes.Where(x => x.OccupationCodeID == 0), "OccupationCodeID", "OccupationCodeName"); //Empty list initially
            else
                OccupationCodeList = new SelectList(db.Employers.Find(ei.EmployerID).EmployerOccupationCodes.OrderBy(x => x.OccupationCodeName), "OccupationCodeID", "OccupationCodeName"); //Empty list initially
            Participant = new Participant();
            EmploymentInformation = new EmploymentInformation();
            EmploymentInformation.OccupationCode = new OccupationCode();
        }

        public Participant Participant { get; set; }

        public EmploymentInformation EmploymentInformation { get; set; }

        public SelectList EmployerList { get; set; }

        public SelectList OccupationCodeList { get; set; }

        [Display(Name = "Were you previously employed by additional pension facilities?")]
        public bool PreviousPensionEmployers { get; set; }
    }
}