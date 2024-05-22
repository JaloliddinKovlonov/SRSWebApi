namespace SRSWebApi.DTO
{
	public class ScheduleDTO
	{ 
		public string? StartTime { get; set; }
		public string? EndTime { get; set; }
		public string? DayOfWeek { get; set; }
		public string? RoomNo { get; set; }
		public int? CourseId { get; set; }
	}
}
