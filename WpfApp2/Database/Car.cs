using System;
using System.Collections.Generic;

namespace WpfApp2.Database;

public partial class Car
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string Number { get; set; } = null!;

    public virtual ICollection<Tare> Tares { get; set; } = new List<Tare>();
}
