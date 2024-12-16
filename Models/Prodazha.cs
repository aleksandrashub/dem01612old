using System;
using System.Collections.Generic;

namespace demo1212shubenok.Models;

public partial class Prodazha
{
    public int IdProdazha { get; set; }

    public string? Date { get; set; }

    public int? IdUser { get; set; }

    public virtual User? IdUserNavigation { get; set; }

    public virtual ICollection<ProdazhaPr> ProdazhaPrs { get; set; } = new List<ProdazhaPr>();
}
