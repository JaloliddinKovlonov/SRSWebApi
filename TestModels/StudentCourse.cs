using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SRSWebApi.TestModels;

[Index("StudentCourseId", IsUnique = true)]
public partial class StudentCourse
{
    [Key]
    public int StudentCourseId { get; set; }

    public int StudentId { get; set; }

    public int CourseId { get; set; }

    public int IsApproved { get; set; }

    public int? IsCompleted { get; set; }

    [ForeignKey("CourseId")]
    [InverseProperty("StudentCourses")]
    public virtual Course Course { get; set; } = null!;

    [ForeignKey("StudentId")]
    [InverseProperty("StudentCourses")]
    public virtual Student Student { get; set; } = null!;

    [InverseProperty("StudentCourse")]
    public virtual ICollection<StudentGrade> StudentGrades { get; set; } = new List<StudentGrade>();
}
