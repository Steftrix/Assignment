using System;
using System.Collections.Generic;

namespace Backend.Models;

public partial class Voyage
{
    public int VoyageId { get; set; }

    public int? ShipId { get; set; }

    public int? DeparturePort { get; set; }

    public int? ArrivalPort { get; set; }

    public DateTime VoyageStart { get; set; }

    public DateTime VoyageEnd { get; set; }

    public virtual Port? ArrivalPortNavigation { get; set; }

    public virtual Port? DeparturePortNavigation { get; set; }

    public virtual Ship? Ship { get; set; }
}
