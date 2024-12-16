using System;
using System.Collections.Generic;

namespace demo1212shubenok.Models;

public partial class Role
{
    public int IdRole { get; set; }

    public string? Role1 { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
