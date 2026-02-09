using SignalRChatWebAPI.Interfaces.IRepository;
using SignalRChatWebAPI.Interfaces.IService;
using SignalRChatWebAPI.Models;

namespace SignalRChatWebAPI.Services
{
	public class ChatService : IChatService
	{
		private readonly IChatRepository chatRepository;

		public ChatService(IChatRepository chatRepository)
		{
			this.chatRepository = chatRepository;
		}
		public async Task<Guid> ProcessMessage(Guid senderId, Guid receiverId, string message)
		{
			var encryptedMsg = Convert.ToBase64String(
			System.Text.Encoding.UTF8.GetBytes(message));

			var chatRelationId = await chatRepository.GetOrCreateChatRelation(senderId, receiverId);

			await chatRepository.SaveMessage(chatRelationId, senderId, receiverId, encryptedMsg);

			return chatRelationId;
		}

		public async Task SetUserOnline(Guid userId, bool isOnline)
		{
			await chatRepository.SetUserOnline(userId, isOnline);
		}

		public async Task<List<User>> GetAllUsersAsync()
		{
			return await chatRepository.GetAllUsersAsync();
		}

		public async Task<List<Message>> GetChatHistoryAsync(Guid senderId, Guid receiverId)
		{
			var relationId = await chatRepository.GetChatRelationIdAsync(senderId, receiverId);

			if (relationId == null)
				return new List<Message>();

			return await chatRepository.GetMessagesByRelationAsync(relationId.Value);
		}
	}
}
