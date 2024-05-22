namespace SRSWebApi.DTO
{
	public class CourseDTO
	{
		public string? AcademicYear { get; set; }

		public int? SemesterId { get; set; }

		public int? ProfessorId { get; set; }

		public string CourseCode { get; set; } = null!;

		public string CourseName { get; set; } = null!;

		public string? CourseDescription { get; set; }

		public int? CreditHours { get; set; }

		public int DepartmentId { get; set; }

		public int? PrerequisiteCourseId { get; set; }
	}

	public class AvailableCourseDTO
	{
		public int CourseId { get; set; }
		public string CourseCode { get; set; } = null!;
		public string CourseName { get; set; } = null!;
		public string? CourseDescription { get; set; }
		public string? AcademicYear { get; set; }
		public int? SemesterId { get; set; }
		public int? ProfessorId { get; set; }
		public int? CreditHours { get; set; }
		public int DepartmentId { get; set; }
		public string ProfessorName { get; set; } = null!;
	}
}
