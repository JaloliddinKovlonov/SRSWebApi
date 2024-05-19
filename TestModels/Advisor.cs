using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SRSWebApi.TestModels;

public partial class Advisor
{
    [Key]
    public byte[] AdvisorId { get; set; } = null!;

    public int? ProfessorId { get; set; }

    [ForeignKey("ProfessorId")]
    [InverseProperty("Advisors")]
    public virtual Professor? Professor { get; set; }
}
