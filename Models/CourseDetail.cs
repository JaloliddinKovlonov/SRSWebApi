using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SRSWebApi.Models;

public partial class CourseDetail
{
    [Key]
    public int CourseDetailsId { get; set; }

    public string CourseCode { get; set; } = null!;

    public string CourseName { get; set; } = null!;

    public string CourseDescription { get; set; } = null!;

    public int CreditHours { get; set; }

    public int DepartmentId { get; set; }

    public int PrerequisiteId { get; set; }
}
