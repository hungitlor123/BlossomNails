using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class ResetPassword
{
    public int ResetPasswordID { get; set; }

    public string Username { get; set; } = null!;

    public string Code { get; set; } = null!;

    public DateTime ExpMinutes { get; set; }

    public bool IsActive { get; set; }
}
