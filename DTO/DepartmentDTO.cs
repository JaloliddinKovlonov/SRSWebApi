namespace SRSWebApi.DTO
{
	public class DepartmentDTO
	{
		public string DepartmentName { get; set; } = null!;

		public string DepartmentCode { get; set; } = null!;

		public string? Description { get; set; }

		public int FacultyId { get; set; }
	}
}
