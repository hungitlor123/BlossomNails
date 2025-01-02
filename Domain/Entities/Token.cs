using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Token
{
    public int TokenID { get; set; }

    public string? RefreshToken { get; set; }

    public Guid? UserID { get; set; }

    public bool IsActive { get; set; }

    public DateTime? RevokeAt { get; set; }

    public virtual User? User { get; set; }
}
