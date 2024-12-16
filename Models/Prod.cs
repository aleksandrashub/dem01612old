using Avalonia.Media.Imaging;
using System;
using System.Collections.Generic;

namespace demo1212shubenok.Models;

public partial class Prod
{
    public int IdPr { get; set; }

    public string? Name { get; set; }

    public string? Image { get; set; }

    public int? IdManuf { get; set; }

    public int? IdAct { get; set; }

    public float? Cost { get; set; }

    public string? Descr { get; set; }

    public virtual ICollection<DopPr> DopPrs { get; set; } = new List<DopPr>();

    public virtual Active? IdActNavigation { get; set; }

    public virtual Manuf? IdManufNavigation { get; set; }

    public virtual ICollection<ProdazhaPr> ProdazhaPrs { get; set; } = new List<ProdazhaPr>();

    public Bitmap bitm => Image != null && Image != "" ? new Bitmap($@"Assets\\{Image}") : null;
    public string color => IdAct == 2 ?
        "Gray" : "White";
    public string activ => IdAct == 2 ?
        "неактивен" : " ";

}
