using System;
using System.Collections.Generic;

namespace SRSWebApi.Models;

public partial class Gender
{
    public int GenderId { get; set; }

    public string GenderName { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public bool IsActive { get; set; }
}
