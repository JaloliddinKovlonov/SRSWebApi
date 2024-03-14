using System;
using System.Collections.Generic;

namespace SRSWebApi.Models;

public partial class Role
{
    public int RoleId { get; set; }

    public string RoleName { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<User> Users { get; } = new List<User>();
}
