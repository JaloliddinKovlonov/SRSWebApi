namespace SRSWebApi.DTO
{

	public class StudentCourseCreateDTO
	{
		public int StudentId { get; set; }
		public int CourseId { get; set; }
		public int IsApproved { get; set; }
	}

	public class StudentCourseUpdateDTO
	{
		public int IsApproved { get; set; }
	}

	public class StudentCourseGetDTO
	{
		public int StudentCourseId { get; set; }
		public int StudentId { get; set; }
		public int CourseId { get; set; }
		public string CourseName { get; set; }
		public string StudentName { get; set; }
		public string ProfessorName { get; set; }
		public int IsApproved { get; set; } 
		public int? IsCompleted { get; set; } 
	}
}
