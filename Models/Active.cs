using System;
using System.Collections.Generic;

namespace demo1212shubenok.Models;

public partial class Active
{
    public int IdAct { get; set; }

    public string? NameAct { get; set; }

    public virtual ICollection<Prod> Prods { get; set; } = new List<Prod>();
}
