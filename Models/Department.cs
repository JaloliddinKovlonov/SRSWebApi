using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SRSWebApi.Models;

[Index("FacultyId", Name = "IX_Departments_FacultyId")]
public partial class Department
{
    [Key]
    public int DepartmentId { get; set; }

    public string DepartmentName { get; set; } = null!;

    public string DepartmentCode { get; set; } = null!;

    public string? Description { get; set; }

    public int FacultyId { get; set; }

    [ForeignKey("FacultyId")]
    [InverseProperty("Departments")]
    public virtual Faculty Faculty { get; set; } = null!;
}
