namespace Eticaret12.Models
{
	public class UserPasswordHistory
	{
		public int UserPasswordHistoryId { get; set; }
		public string Password { get; set; }
		public DateTime CreatedDate { get; set; }
		public string Mail { get; set; }
		public DateTime ExpireDate { get; set; } //skt: şifrenin girilebileceği son zmn bilgisi
		public bool IsActive { get; set; }
	}
}

