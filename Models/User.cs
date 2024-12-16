using System;
using System.Collections.Generic;

namespace demo1212shubenok.Models;

public partial class User
{
    public int IdUser { get; set; }

    public int? IdRole { get; set; }

    public string? Login { get; set; }

    public string? Passwd { get; set; }

    public virtual Role? IdRoleNavigation { get; set; }

    public virtual ICollection<Prodazha> Prodazhas { get; set; } = new List<Prodazha>();
}
