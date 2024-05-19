using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SRSWebApi.TestModels;

[Index("CourseId", IsUnique = true)]
public partial class Course
{
    [Key]
    public int CourseId { get; set; }

    public string CreatedOn { get; set; } = null!;

    public int CreatedById { get; set; }

    public string? ModifiedOn { get; set; }

    public int? ModifiedById { get; set; }

    public int IsDeleted { get; set; }

    public int IsActive { get; set; }

    public string? DeletedOn { get; set; }

    public int? DeletedById { get; set; }

    public string? AcademicYear { get; set; }

    public int? SemesterId { get; set; }

    public int? ProfessorId { get; set; }

    public string CourseCode { get; set; } = null!;

    public string CourseName { get; set; } = null!;

    public string? CourseDescription { get; set; }

    public int? CreditHours { get; set; }

    public int DepartmentId { get; set; }

    public int? PrerequisiteCourseId { get; set; }

    [ForeignKey("DepartmentId")]
    [InverseProperty("Courses")]
    public virtual Department Department { get; set; } = null!;

    [InverseProperty("PrerequisiteCourse")]
    public virtual ICollection<Course> InversePrerequisiteCourse { get; set; } = new List<Course>();

    [ForeignKey("PrerequisiteCourseId")]
    [InverseProperty("InversePrerequisiteCourse")]
    public virtual Course? PrerequisiteCourse { get; set; }

    [ForeignKey("ProfessorId")]
    [InverseProperty("Courses")]
    public virtual Professor? Professor { get; set; }

    [InverseProperty("Course")]
    public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();

    [ForeignKey("SemesterId")]
    [InverseProperty("Courses")]
    public virtual Semester? Semester { get; set; }

    [InverseProperty("Course")]
    public virtual ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();
}
