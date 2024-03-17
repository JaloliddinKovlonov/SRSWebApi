using System;
using System.Collections.Generic;

namespace SRSWebApi.Models;

public partial class User
{
    public int UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? Email { get; set; }

    public DateTime? LastLogin { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? CreatedById { get; set; }

    public DateTime ModifiedOn { get; set; }

    public bool? IsDeleted { get; set; }

    public bool IsActive { get; set; }

    public int? DeletedById { get; set; }

    public int RoleId { get; set; }

    public virtual ICollection<Professor> Professors { get; } = new List<Professor>();

    public virtual Role Role { get; set; } = null!;

    public virtual ICollection<Student> Students { get; } = new List<Student>();
}
