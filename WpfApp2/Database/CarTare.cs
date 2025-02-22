using System;
using System.Collections.Generic;

namespace WpfApp2.Database;

public partial class CarTare
{
    public long Id { get; set; }

    public long IdCar { get; set; }

    public long IdTare { get; set; }

    public virtual Car IdCarNavigation { get; set; } = null!;

    public virtual Tare IdTareNavigation { get; set; } = null!;
}
