using System;
using System.Collections.Generic;

namespace demo1212shubenok.Models;

public partial class DopPr
{
    public int IdPr { get; set; }

    public int IdDop { get; set; }

    public virtual Prod IdPrNavigation { get; set; } = null!;
}
