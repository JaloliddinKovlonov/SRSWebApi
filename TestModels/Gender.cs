using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SRSWebApi.TestModels;

[Keyless]
public partial class Gender
{
    public int GenderId { get; set; }

    public string GenderName { get; set; } = null!;

    public int IsDeleted { get; set; }

    public int IsActive { get; set; }
}
