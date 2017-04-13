using System.ComponentModel.DataAnnotations;

namespace Enrollment.Models
{
    public class State
    {
        [Key]
        public int StateID { get; set; }

        public string StateAbbreviation { get; set; }

        public string StateName { get; set; }
    }
}