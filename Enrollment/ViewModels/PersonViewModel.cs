using Enrollment.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Enrollment.ViewModels
{
    public class PersonViewModel
    {
        public Person Person { get; set; }

        public SelectList GenderList { get; set; }
    }
}