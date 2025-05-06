using System;
using System.Collections.Generic;

namespace Backend.Models;

public partial class Port
{
    public int PortId { get; set; }

    public string PortName { get; set; } = null!;

    public int? CountryId { get; set; }

    public virtual Country? Country { get; set; }

    public virtual ICollection<Voyage> VoyageArrivalPortNavigations { get; set; } = new List<Voyage>();

    public virtual ICollection<Voyage> VoyageDeparturePortNavigations { get; set; } = new List<Voyage>();
}
