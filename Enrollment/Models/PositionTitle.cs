using System.ComponentModel.DataAnnotations;

namespace Enrollment.Models
{
    public class PositionTitle
    {
        [Key]
        public int PositionTitleID { get; set; }

        public string PositionTitleName { get; set; }
    }
}