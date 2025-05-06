using System;
using System.Collections.Generic;

namespace Backend.Models;

public partial class Lastyearcountry
{
    public string? CountryName { get; set; }

    public long? VisitCount { get; set; }

    public string? PortsVisited { get; set; }
}
