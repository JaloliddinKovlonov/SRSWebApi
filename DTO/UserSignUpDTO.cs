namespace SRSWebApi.DTO
{
	public class UserSignUpDTO
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public int RoleId { get; set; }
	}

	public class UserSignInDTO
	{
		public string Email { get; set; }
		public string Password { get; set; }
	}
}
