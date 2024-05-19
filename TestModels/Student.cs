using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SRSWebApi.TestModels;

[Index("UserId", Name = "IX_Students_UserId")]
public partial class Student
{
    [Key]
    public int StudentId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public int DepartmentId { get; set; }

    public int GenderId { get; set; }

    public int? MajorId { get; set; }

    public int? MinorId { get; set; }

    public int NationalityId { get; set; }

    public DateTime AdmissionDate { get; set; }

    public int AdvisorId { get; set; }

    public DateTime? GraduateDate { get; set; }

    public int UserId { get; set; }

    [InverseProperty("Student")]
    public virtual ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();

    [ForeignKey("UserId")]
    [InverseProperty("Students")]
    public virtual User User { get; set; } = null!;
}
