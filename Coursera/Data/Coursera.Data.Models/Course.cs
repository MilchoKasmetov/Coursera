using Coursera.Data.Common.Models;
using System;
using System.Collections.Generic;

namespace Coursera.Data.Models;

public partial class Course : BaseDeletableModel<int>
{
    public int Id { get; set; }

    public string Name { get; set; }

    public int InstructorId { get; set; }

    public byte TotalTime { get; set; }

    public byte Credit { get; set; }

    public DateTime TimeCreated { get; set; }

    public virtual Instructor Instructor { get; set; }

    public virtual ICollection<StudentsCoursesXref> StudentsCoursesXrefs { get; } = new List<StudentsCoursesXref>();
}
