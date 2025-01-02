using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Role
{
    public int RoleID { get; set; }

    public string? Name { get; set; }

    public int Code { get; set; }

    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
