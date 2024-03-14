using System;
using System.Collections.Generic;

namespace SRSWebApi.Models;

public partial class Faculty
{
    public int FacultyId { get; set; }

    public string FacultyName { get; set; } = null!;

    public string FacultyCode { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Department> Departments { get; } = new List<Department>();
}
