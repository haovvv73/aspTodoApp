using System;
using System.Collections.Generic;

namespace ASPLab05.Dbcontext;

public partial class Product
{
    public int Id { get; set; }

    public string Productname { get; set; } = null!;

    public int Quality { get; set; }

    public double Price { get; set; }

    public double Total { get; set; }
}
