namespace SignalRChatWebAPI.Models
{
	public class User
	{
		public Guid UserId { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public bool IsOnline { get; set; }
		public DateTime CreatedAt { get; set; }
	}
}
