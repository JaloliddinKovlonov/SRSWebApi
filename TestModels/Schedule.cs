using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SRSWebApi.TestModels;

[Index("ScheduleId", IsUnique = true)]
public partial class Schedule
{
    [Key]
    public int ScheduleId { get; set; }

    public string? StartTime { get; set; }

    public string? EndTime { get; set; }

    public string? DayOfWeek { get; set; }

    public string? RoomNo { get; set; }

    public int? CourseId { get; set; }

    [ForeignKey("CourseId")]
    [InverseProperty("Schedules")]
    public virtual Course? Course { get; set; }
}
