using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Enrollment.Models
{
    public class Gender
    {
        [Key]
        [Display(Name = "Gender")]
        public int GenderID { get; set; }

        public string GenderName { get; set; }

        public virtual ICollection<Person> People { get; set; }
    }
}