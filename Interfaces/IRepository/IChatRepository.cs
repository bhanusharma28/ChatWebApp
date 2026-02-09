using SignalRChatWebAPI.Models;

namespace SignalRChatWebAPI.Interfaces.IRepository
{
	public interface IChatRepository
	{
		Task<Guid> GetOrCreateChatRelation(Guid senderId, Guid receiverId);
		Task SaveMessage(Guid chatRelationId, Guid senderId, Guid receiverId, string encryptedMessage);
		Task SetUserOnline(Guid userId, bool isOnline);
		Task<List<User>> GetAllUsersAsync();
		Task<Guid?> GetChatRelationIdAsync(Guid senderId, Guid receiverId);
		Task<List<Message>> GetMessagesByRelationAsync(Guid chatRelationId);
	}
}
