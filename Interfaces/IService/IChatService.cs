using SignalRChatWebAPI.Models;

namespace SignalRChatWebAPI.Interfaces.IService
{
	public interface IChatService
	{
		Task<Guid> ProcessMessage(Guid senderId, Guid receiverId, string message);
		Task SetUserOnline(Guid userId, bool isOnline);
		Task<List<User>> GetAllUsersAsync();
		Task<List<Message>> GetChatHistoryAsync(Guid senderId, Guid receiverId);
	}
}
