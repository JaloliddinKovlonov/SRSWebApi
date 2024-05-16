using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SRSWebApi.Models;

[Index("CourseId", IsUnique = true)]
public partial class Course
{
    [Key]
    public int CourseId { get; set; }

    public int CourseDetailsId { get; set; }

    public string CreatedOn { get; set; } = null!;

    public int CreatedById { get; set; }

    public string? ModifiedOn { get; set; }

    public int? ModifiedById { get; set; }

    public int IsDeleted { get; set; }

    public int IsActive { get; set; }

    public string? DeletedOn { get; set; }

    public int? DeletedById { get; set; }

    public string? StartTime { get; set; }

    public string? EndTime { get; set; }

    public int? DaysOfWeek { get; set; }

    public string? ClassRoomNo { get; set; }

    public string? AcademicYear { get; set; }

    public int? SemesterId { get; set; }

    public int? ProfessorId { get; set; }

    [ForeignKey("ProfessorId")]
    [InverseProperty("Courses")]
    public virtual Professor? Professor { get; set; }

    [ForeignKey("SemesterId")]
    [InverseProperty("Courses")]
    public virtual Semester? Semester { get; set; }

    [InverseProperty("Course")]
    public virtual ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();
}
