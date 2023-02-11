using System;
using System.Collections.Generic;

namespace Coursera.Data.Models;

public partial class Setting
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Value { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime? DeletedOn { get; set; }
}
