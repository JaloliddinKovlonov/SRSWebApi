using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SRSWebApi.Models;

[Index("UserId", Name = "IX_RefreshTokens_UserId")]
public partial class RefreshToken
{
    [Key]
    public int TokenId { get; set; }

    public string Token { get; set; } = null!;

    public DateTime Created { get; set; }

    public DateTime Expires { get; set; }

    public int IsUsed { get; set; }

    public int IsRevoked { get; set; }

    public int UserId { get; set; }

    [Column("LastLoggedInIP")]
    public string LastLoggedInIp { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("RefreshTokens")]
    public virtual User User { get; set; } = null!;
}
