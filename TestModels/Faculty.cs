using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SRSWebApi.TestModels;

public partial class Faculty
{
    [Key]
    public int FacultyId { get; set; }

    public string FacultyName { get; set; } = null!;

    public string FacultyCode { get; set; } = null!;

    public string? Description { get; set; }

    [InverseProperty("Faculty")]
    public virtual ICollection<Department> Departments { get; set; } = new List<Department>();
}
