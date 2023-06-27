using System;
using System.Collections.Generic;

namespace ASPLab05.Dbcontext;

public partial class Employee
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? City { get; set; }

    public decimal? Salary { get; set; }
}
