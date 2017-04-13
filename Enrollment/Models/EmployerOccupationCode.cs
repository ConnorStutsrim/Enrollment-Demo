using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Enrollment.Models
{
    public class EmployerOccupationCode
    {
        [Key]
        public int EmployerOccupationCodeID { get; set; }

        [ForeignKey("Employer")]
        public int EmployerID { get; set; }
        public virtual Employer Employer { get; set; }

        [ForeignKey("OccupationCode")]
        public int OccupationCodeID { get; set; }
        public virtual OccupationCode OccupationCode { get; set; }

        public string OccupationCodeName {
            get
            {
                if (OccupationCode == null)
                    return string.Empty;
                else
                    return OccupationCode.OccupationCodeName;
            }
        }
    }
}