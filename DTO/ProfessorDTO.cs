namespace SRSWebApi.DTO
{
	public class ProfessorCreateDTO
	{
		public string FirstName { get; set; } = null!;
		public string LastName { get; set; } = null!;
		public int DepartmentId { get; set; }
		public string Email { get; set; } = null!;
		public string Password { get; set; } = null!;
	}

	public class ProfessorUpdateDTO
	{
		public string FirstName { get; set; } = null!;
		public string LastName { get; set; } = null!;
		public int DepartmentId { get; set; }
		public int UserId { get; set; }
	}
}
