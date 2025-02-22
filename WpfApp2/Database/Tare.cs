using System;
using System.Collections.Generic;

namespace WpfApp2.Database;

public partial class Tare
{
    public long Id { get; set; }

    public long? IdCar { get; set; }

    public string Number { get; set; } = null!;

    public double GrossWeight { get; set; }

    public double TareWeight { get; set; }

    public double NetWeight { get; set; }

    public DateTime TareDate { get; set; }

    public DateTime DateGross { get; set; }

    public virtual Car? IdCarNavigation { get; set; }
}
