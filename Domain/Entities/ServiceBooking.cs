using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class ServiceBooking
{
    public int ServiceBookingID { get; set; }

    public int BookingID { get; set; }

    public int ServiceID { get; set; }

    public decimal ServiceCost { get; set; }

    public virtual Booking Booking { get; set; } = null!;

    public virtual Service Service { get; set; } = null!;
}
