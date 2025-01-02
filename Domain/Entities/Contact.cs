using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Contact
{
    public int ContactID { get; set; }

    public string? Fullname { get; set; }

    public int Phone { get; set; }

    public string? Email { get; set; }

    public string? Notes { get; set; }

    public string Status { get; set; } = null!;

    public DateTime CreatedAt { get; set; }
}
