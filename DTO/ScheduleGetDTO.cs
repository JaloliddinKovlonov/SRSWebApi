namespace SRSWebApi.DTO
{
	public class ScheduleGetDTO
	{
		public string? StartTime { get; set; }
		public string? EndTime { get; set; }
		public string? DayOfWeek { get; set; }
		public string? RoomNo { get; set; }
		public int? CourseId { get; set; }
		public string? CourseName { get; set; }
		public string? CourseCode { get; set; }
		public string? ProfessorName { get; set; }
	}
}
