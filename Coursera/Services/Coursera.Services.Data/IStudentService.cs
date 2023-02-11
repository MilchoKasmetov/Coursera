using Coursera.Web.ViewModels.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursera.Services.Data
{
    public interface IStudentService
    {
            Task<ICollection<StudentViewModel>> ShowAllStudents();

    }
}
