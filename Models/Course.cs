using System;
using System.Collections.Generic;

namespace SRSWebApi.Models;

public partial class Course
{
    public int CourseId { get; set; }

    public int CourseDetailsId { get; set; }

    public DateTime CreatedOn { get; set; }

    public int CreatedById { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public int? ModifiedById { get; set; }

    public bool IsDeleted { get; set; }

    public bool IsActive { get; set; }

    public DateTime? DeletedOn { get; set; }

    public int? DeletedById { get; set; }
}
