namespace SRSWebApi.Models
{
	public class RefreshToken
	{
		public int TokenId { get; set; }
		public string Token { get; set; }
		public DateTime Created { get; set; } = DateTime.UtcNow;
		public DateTime Expires { get; set; }
		public bool IsUsed { get; set; } = false;
		public bool IsRevoked { get; set; } = false;
		public int UserId { get; set; }
		public string LastLoggedInIP { get; set; }
		public virtual User User { get; set; }
	}
}
