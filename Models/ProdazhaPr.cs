using System;
using System.Collections.Generic;

namespace demo1212shubenok.Models;

public partial class ProdazhaPr
{
    public int IdProdazha { get; set; }

    public int IdPr { get; set; }

    public long? Amount { get; set; }

    public virtual Prod IdPrNavigation { get; set; } = null!;

    public virtual Prodazha IdProdazhaNavigation { get; set; } = null!;
}
