using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SRSWebApi.Models;

public partial class Advisor
{
    [Key]
    public int AdvisorId { get; set; }

    public int? ProfessorId { get; set; }

    public int? UserId { get; set; }

    [ForeignKey("ProfessorId")]
    [InverseProperty("Advisors")]
    public virtual Professor? Professor { get; set; }

    [InverseProperty("Advisor")]
    public virtual ICollection<Student> Students { get; set; } = new List<Student>();

    [ForeignKey("UserId")]
    [InverseProperty("Advisors")]
    public virtual User? User { get; set; }
}
