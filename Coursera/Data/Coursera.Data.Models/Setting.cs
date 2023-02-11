using Coursera.Data.Common.Models;
using System;
using System.Collections.Generic;

namespace Coursera.Data.Models;

public class Setting : BaseDeletableModel<int>
{
    public string Name { get; set; }

    public string Value { get; set; }
}
