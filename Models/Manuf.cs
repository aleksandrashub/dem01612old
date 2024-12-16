using System;
using System.Collections.Generic;

namespace demo1212shubenok.Models;

public partial class Manuf
{
    public int IdManuf { get; set; }

    public string? Name { get; set; }

    public DateOnly? DateNach { get; set; }

    public virtual ICollection<Prod> Prods { get; set; } = new List<Prod>();
}
