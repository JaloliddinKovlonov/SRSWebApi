using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SRSWebApi.Models;

[Index("RoleId", Name = "IX_Users_RoleId")]
public partial class User
{
    [Key]
    public int UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? Email { get; set; }

    public DateTime? LastLogin { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? CreatedById { get; set; }

    public DateTime ModifiedOn { get; set; }

    public int? IsDeleted { get; set; }

    public int IsActive { get; set; }

    public int? DeletedById { get; set; }

    public int RoleId { get; set; }

    public string Salt { get; set; } = null!;

    [InverseProperty("User")]
    public virtual ICollection<Professor> Professors { get; set; } = new List<Professor>();

    [InverseProperty("User")]
    public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();

    [ForeignKey("RoleId")]
    [InverseProperty("Users")]
    public virtual Role Role { get; set; } = null!;

    [InverseProperty("User")]
    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
