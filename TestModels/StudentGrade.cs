using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SRSWebApi.TestModels;

public partial class StudentGrade
{
    [Key]
    public int GradeId { get; set; }

    public int? StudentCourseId { get; set; }

    public string? Grade { get; set; }

    public int? AssignedBy { get; set; }

    public string? AssignedOn { get; set; }

    public int? ModifiedBy { get; set; }

    public string? ModifiedOn { get; set; }

    public int? IsActive { get; set; }

    [ForeignKey("AssignedBy")]
    [InverseProperty("StudentGradeAssignedByNavigations")]
    public virtual Professor? AssignedByNavigation { get; set; }

    [ForeignKey("ModifiedBy")]
    [InverseProperty("StudentGradeModifiedByNavigations")]
    public virtual Professor? ModifiedByNavigation { get; set; }

    [ForeignKey("StudentCourseId")]
    [InverseProperty("StudentGrades")]
    public virtual StudentCourse? StudentCourse { get; set; }
}
