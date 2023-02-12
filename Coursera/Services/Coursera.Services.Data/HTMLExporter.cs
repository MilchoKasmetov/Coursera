using Coursera.Web.ViewModels.Students;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursera.Services.Data
{
    public class HTMLExporter : IExporter
    {
        public void Export(ICollection<StudentViewModel> studentViewModels, string directoryPathInput)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Student", typeof(string));
            dt.Columns.Add("Total Credit", typeof(int));
            dt.Columns.Add("Course Name", typeof(string));
            dt.Columns.Add("Course Time", typeof(int));
            dt.Columns.Add("Course Credit", typeof(int));
            dt.Columns.Add("Course Instructor", typeof(string));

            foreach (var student in studentViewModels)
            {
                dt.Rows.Add(student.FullName, student.TotalCreadits);
                foreach (var course in student.Courses)
                {
                    dt.Rows.Add(null, null, course.Name, course.Time, course.Credit, course.InstructorFullName);
                }
            }

            string html = "<table><thead><tr>";

            foreach (DataColumn column in dt.Columns)
            {
                html += "<th>" + column.ColumnName + "</th>";
            }

            html += "</tr></thead><tbody>";

            foreach (DataRow row in dt.Rows)
            {
                html += "<tr>";

                foreach (DataColumn column in dt.Columns)
                {
                    html += "<td>" + row[column].ToString() + "</td>";
                }

                html += "</tr>";
            }

            html += "</tbody></table>";

            string directoryPath = directoryPathInput;
            Directory.CreateDirectory(directoryPath);

            string filePath = Path.Combine(directoryPath, "report.html");
            File.WriteAllText(filePath, html);

        }
    }
}
