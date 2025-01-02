using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class User
{
    public Guid UserID { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? Firstname { get; set; }

    public string? Lastname { get; set; }

    public int Phone { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Booking> BookingCustomers { get; set; } = new List<Booking>();

    public virtual ICollection<Booking> BookingNailTechnicians { get; set; } = new List<Booking>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual ICollection<Token> Tokens { get; set; } = new List<Token>();

    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
