using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SRSWebApi.TestModels;

[Keyless]
public partial class Nationality
{
    public int NationalityId { get; set; }

    public string NationalityName { get; set; } = null!;

    public int IsActive { get; set; }

    public int IsDeleted { get; set; }

    public string? DeletedOn { get; set; }
}
