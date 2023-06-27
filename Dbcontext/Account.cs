using System;
using System.Collections.Generic;

namespace ASPLab05.Dbcontext;

public partial class Account
{
    public int Id { get; set; }

    public string UserName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string AccountType { get; set; } = null!;
}
