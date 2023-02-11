using System;
using System.Collections.Generic;

namespace Coursera.Data.Models;

public partial class Instructor
{
    public int Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public DateTime TimeCreated { get; set; }

    public virtual ICollection<Course> Courses { get; } = new List<Course>();
}
