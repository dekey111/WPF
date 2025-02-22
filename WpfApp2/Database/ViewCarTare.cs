using System;
using System.Collections.Generic;

namespace WpfApp2.Database;

public partial class ViewCarTare
{
    public long Id { get; set; }

    public long IdCar { get; set; }

    public string? Name { get; set; }

    public string? Number { get; set; }

    public long IdTare { get; set; }

    public string? TareNumbers { get; set; }

    public double GrossWeight { get; set; }

    public double TareWeight { get; set; }

    public double NetWeight { get; set; }

    public DateTime TareDate { get; set; }

    public DateTime DateGross { get; set; }
}
