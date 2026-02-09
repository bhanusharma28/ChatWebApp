namespace SignalRChatWebAPI.Models
{
	public class ChatRelation
	{
		public Guid ChatRelationId { get; set; }
		public Guid User1Id { get; set; }
		public Guid User2Id { get; set; }
		public DateTime CreatedAt { get; set; }
	}
}
