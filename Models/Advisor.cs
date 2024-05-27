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

    public int? DepartmentId { get; set; }

    [ForeignKey("DepartmentId")]
    [InverseProperty("Advisors")]
    public virtual Department? Department { get; set; }

    [ForeignKey("ProfessorId")]
    [InverseProperty("Advisors")]
    public virtual Professor? Professor { get; set; }

    [InverseProperty("Advisor")]
    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
