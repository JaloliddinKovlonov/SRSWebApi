using System;
using System.Collections.Generic;

namespace SRSWebApi.Models;

public partial class Professor
{
    public int ProfessorId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public int DepartmentId { get; set; }

    public int UserId { get; set; }

    public virtual User User { get; set; } = null!;
}
