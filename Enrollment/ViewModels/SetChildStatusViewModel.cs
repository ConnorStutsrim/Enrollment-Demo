using System.ComponentModel.DataAnnotations;

namespace Enrollment.ViewModels
{
    public class SetChildStatusViewModel
    {
        [Display(Name = "Do you have any children?")]

        public bool hasChildren { get; set; }
    }
}