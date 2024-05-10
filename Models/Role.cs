using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SRSWebApi.Models;

public partial class Role
{
    [Key]
    public int RoleId { get; set; }

    public string RoleName { get; set; } = null!;

    public int IsDeleted { get; set; }

    public int IsActive { get; set; }

    [InverseProperty("Role")]
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
