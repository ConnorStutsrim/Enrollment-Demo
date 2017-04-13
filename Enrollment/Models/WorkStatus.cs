using System.ComponentModel.DataAnnotations;

namespace Enrollment.Models
{
    public class WorkStatus
    {
        [Key]
        public int WorkStatusID { get; set; }

        [Display(Name = "Work Status")]
        public string WorkStatusName { get; set; }
    }
}