using Coursera.Data;
using Coursera.Data.Common.Repositories;
using Coursera.Data.Models;
using Coursera.Web.ViewModels.Courses;
using Coursera.Web.ViewModels.Students;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursera.Services.Data
{
    public class StudentService : IStudentService
    {
        private readonly ApplicationDbContext studentRepository;

        public StudentService(ApplicationDbContext studentRepository)
        {
            this.studentRepository = studentRepository;
        }



        public async Task<ICollection<StudentViewModel>> ShowAllStudents()
        {
            var students = await this.studentRepository
                .Students
                .Include(x => x.StudentsCoursesXrefs)
                .ThenInclude(y => y.Course)
                .ThenInclude(r => r.Instructor)
                .ToListAsync();
          
            return students
                .Select(x => new StudentViewModel()
                {
                    FullName = $"{x.FirstName} {x.LastName}",
                    Courses = x.StudentsCoursesXrefs
                    .Select(n => new CoursesViewModel()
                    {
                        Name = n.Course.Name,
                        Credit = n.Course.Credit,
                        Time = n.Course.TotalTime,
                        InstructorFullName = $"{n.Course.Instructor.FirstName} {n.Course.Instructor.LastName}",
                    })
                    .ToList(),
                })
                .ToList();
        }
    }
}
