using Coursera.Web.ViewModels.Courses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursera.Web.ViewModels.Students
{
    public abstract class BaseStudentViewModel
    {
        public string FullName { get; set; }

        public ICollection<CoursesViewModel> Courses { get; set; }

        public int TotalCreadits => this.Courses.Select(x => x.Credit).Sum();
    }
}
