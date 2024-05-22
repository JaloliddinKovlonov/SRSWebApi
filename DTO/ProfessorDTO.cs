namespace SRSWebApi.DTO
{
	public class ProfessorDTO
	{
		public int ProfessorId { get; set; }
		public string FirstName { get; set; } = null!;
		public string LastName { get; set; } = null!;
		public int DepartmentId { get; set; }
		public int UserId { get; set; }
	}

	public class ProfessorCreateDTO
	{
		public string FirstName { get; set; } = null!;
		public string LastName { get; set; } = null!;
		public int DepartmentId { get; set; }
		public int UserId { get; set; }
	}

	public class ProfessorUpdateDTO
	{
		public string FirstName { get; set; } = null!;
		public string LastName { get; set; } = null!;
		public int DepartmentId { get; set; }
		public int UserId { get; set; }
	}
}
