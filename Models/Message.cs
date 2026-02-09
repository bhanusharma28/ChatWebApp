namespace SignalRChatWebAPI.Models
{
	public class Message
	{
		public Guid MessageId { get; set; }
		public Guid ChatRelationId { get; set; }
		public Guid SenderId { get; set; }
		public Guid ReceiverId { get; set; }
		public string EncryptedMessage { get; set; }
		public DateTime SentAt { get; set; }
	}
}
