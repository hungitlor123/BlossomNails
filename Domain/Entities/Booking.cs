﻿using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Booking
{
    public int BookingID { get; set; }

    public Guid? CustomerID { get; set; }

    public string? CustomerName { get; set; }

    public string? CustomerPhone { get; set; }

    public string? CustomerEmail { get; set; }

    public Guid? NailTechnicianID { get; set; }

    public string? BookingDate { get; set; }

    public string? StartTime { get; set; }

    public string? EndTime { get; set; }

    public string Status { get; set; } = null!;

    public decimal TotalCost { get; set; }

    public string? Notes { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAT { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual User? Customer { get; set; }

    public virtual User? NailTechnician { get; set; }

    public virtual ICollection<ServiceBooking> ServiceBookings { get; set; } = new List<ServiceBooking>();
}
