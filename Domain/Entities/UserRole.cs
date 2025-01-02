using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class UserRole
{
    public int UserRoleID { get; set; }

    public Guid? UserID { get; set; }

    public int? RoleID { get; set; }

    public virtual Role? Role { get; set; }

    public virtual User? User { get; set; }
}
