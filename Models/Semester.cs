using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SRSWebApi.Models;

[Index("SemesterId", IsUnique = true)]
public partial class Semester
{
    [Key]
    public int SemesterId { get; set; }

    public string? SemesterName { get; set; }

    [InverseProperty("Semester")]
    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
}
