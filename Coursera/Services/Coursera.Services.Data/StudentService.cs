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
        private readonly IDeletableEntityRepository<Student> studentRepository;

        public StudentService(IDeletableEntityRepository<Student> studentRepository)
        {
            this.studentRepository = studentRepository;
        }



        public async Task<ICollection<StudentViewModel>> ShowAllStudents()
        {
            return await this.studentRepository
                .All()
                .Select(x => new StudentViewModel()
                {
                    FullName = $"{x.FirstName} {x.LastName}",
                    Courses = x.StudentsCoursesXrefs
                    .Select(c => c.Course)
                    .ToList()
                    .Select(n => new CoursesViewModel()
                    {   Name = n.Name,
                        Credit = n.Credit,
                        Time = n.TotalTime,
                        InstructorFullName =$"{n.Instructor.FirstName} {n.Instructor.LastName}",
                    })
                    .ToList(),
                })
                .ToListAsync();
        }
    }
}
