using System;
using System.Collections.Generic;

namespace SRSWebApi.Models;

public partial class Nationality
{
    public int NationalityId { get; set; }

    public string NationalityName { get; set; } = null!;

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime? DeletedOn { get; set; }
}
