using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SRSWebApi.Models;

[Index("UserId", Name = "IX_Professors_UserId")]
public partial class Professor
{
    [Key]
    public int ProfessorId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public int DepartmentId { get; set; }

    public int UserId { get; set; }

    [InverseProperty("Professor")]
    public virtual ICollection<Advisor> Advisors { get; set; } = new List<Advisor>();

    [InverseProperty("Professor")]
    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();

    [InverseProperty("AssignedByNavigation")]
    public virtual ICollection<StudentGrade> StudentGradeAssignedByNavigations { get; set; } = new List<StudentGrade>();

    [InverseProperty("ModifiedByNavigation")]
    public virtual ICollection<StudentGrade> StudentGradeModifiedByNavigations { get; set; } = new List<StudentGrade>();

    [ForeignKey("UserId")]
    [InverseProperty("Professors")]
    public virtual User User { get; set; } = null!;
}
