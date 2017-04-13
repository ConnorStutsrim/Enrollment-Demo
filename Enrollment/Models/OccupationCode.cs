using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Enrollment.Models
{
    public class OccupationCode
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OccupationCodeID { get; set; }

        public string OccupationCodeName { get; set; }

        public virtual ICollection<EmployerOccupationCode> EmployerOccupationCodes { get; set; }
    }
}