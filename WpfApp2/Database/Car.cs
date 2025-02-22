using System;
using System.Collections.Generic;

namespace WpfApp2.Database;

public partial class Car
{
    public long Id { get; set; }

    public string? Name { get; set; }

    public string? Number { get; set; }

    public virtual ICollection<CarTare> CarTares { get; set; } = new List<CarTare>();
}
