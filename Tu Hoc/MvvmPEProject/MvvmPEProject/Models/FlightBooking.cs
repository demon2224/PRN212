using System;
using System.Collections.Generic;

namespace MvvmPEProject.Models;

public partial class FlightBooking
{
    public int Id { get; set; }

    public string PasserngerName { get; set; } = null!;

    public string FlightNumber { get; set; } = null!;

    public string Departure { get; set; } = null!;

    public string Destination { get; set; } = null!;

    public string DepartureDate { get; set; } = null!;

    public string DepartureTime { get; set; } = null!;

    public string SeatClass { get; set; } = null!;

    public double Price { get; set; }
}
