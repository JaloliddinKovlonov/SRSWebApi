using System;
using System.Collections.Generic;

namespace SRSWebApi.Models;

public partial class Student
{
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

    public virtual User User { get; set; } = null!;
}
