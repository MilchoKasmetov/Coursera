
using Coursera.Web.ViewModels.Courses;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

namespace Coursera.Web.ViewModels.Students
{
    public class StudentViewModel
    {
        public string FullName { get; set; }

        public ICollection<CoursesViewModel> Courses { get; set; }

        public int TotalCreadits => this.Courses.Select(x => x.Credit).Sum();

    }
}
