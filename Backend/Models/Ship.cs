using System;
using System.Collections.Generic;

namespace Backend.Models;

public partial class Ship
{
    public int ShipId { get; set; }

    public string ShipName { get; set; } = null!;

    public decimal? MaxSpeed { get; set; }

    public string ShipType { get; set; } = null!;

    public virtual ICollection<Voyage> Voyages { get; set; } = new List<Voyage>();
}
