using System;
using System.Collections.Generic;

namespace PETest1.Models;

public partial class Table
{
    public int Id { get; set; }

    public string? FlightNumber { get; set; }

    public string? PassengerName { get; set; }

    public string? Departure { get; set; }

    public string? Destination { get; set; }

    public string? DepartureDate { get; set; }

    public string? DepartureTime { get; set; }

    public string? SeatClass { get; set; }
}
