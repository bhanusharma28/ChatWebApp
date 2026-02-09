using Microsoft.EntityFrameworkCore;
using SignalRChatWebAPI.Data;
using SignalRChatWebAPI.Interfaces.IRepository;
using SignalRChatWebAPI.Models;

namespace SignalRChatWebAPI.Repository
{
	public class ChatRepository : IChatRepository
	{
		private readonly ChatAppDbContext chatAppDbContext;

		public ChatRepository(ChatAppDbContext chatAppDbContext)
		{
			this.chatAppDbContext = chatAppDbContext;
		}
		public async Task<Guid> GetOrCreateChatRelation(Guid senderId, Guid receiverId)
		{
			var relation = await chatAppDbContext.ChatRelations
			.FirstOrDefaultAsync(r =>
				(r.User1Id == senderId && r.User2Id == receiverId) ||
				(r.User1Id == receiverId && r.User2Id == senderId));

			if (relation == null)
			{
				relation = new ChatRelation
				{
					ChatRelationId = Guid.NewGuid(),
					User1Id = senderId,
					User2Id = receiverId,
					CreatedAt = DateTime.Now
				};

				chatAppDbContext.ChatRelations.Add(relation);
				await chatAppDbContext.SaveChangesAsync();
			}

			return relation.ChatRelationId;
		}

		public async Task SaveMessage(Guid chatRelationId, Guid senderId, Guid receiverId, string encryptedMessage)
		{
			var message = new Message
			{
				MessageId = Guid.NewGuid(),
				ChatRelationId = chatRelationId,
				SenderId = senderId,
				ReceiverId = receiverId,
				EncryptedMessage = encryptedMessage,
				SentAt = DateTime.UtcNow
			};

			chatAppDbContext.Messages.Add(message);
			await chatAppDbContext.SaveChangesAsync();
		}

		public async Task SetUserOnline(Guid userId, bool isOnline)
		{
			var user = await chatAppDbContext.Users.FindAsync(userId);
			if (user != null)
			{
				user.IsOnline = isOnline;
				await chatAppDbContext.SaveChangesAsync();
			}
		}

		public async Task<List<User>> GetAllUsersAsync()
		{
			return await chatAppDbContext.Users.ToListAsync();
		}

		public async Task<Guid?> GetChatRelationIdAsync(Guid senderId, Guid receiverId)
		{
			var relation = await chatAppDbContext.ChatRelations
				.FirstOrDefaultAsync(r =>
					(r.User1Id == senderId && r.User2Id == receiverId) ||
					(r.User1Id == receiverId && r.User2Id == senderId));

			return relation?.ChatRelationId;
		}

		public async Task<List<Message>> GetMessagesByRelationAsync(Guid chatRelationId)
		{
			var messages = await chatAppDbContext.Messages
			.Where(m => m.ChatRelationId == chatRelationId)
			.OrderBy(m => m.SentAt)
			.ToListAsync();

			// ✅ Decrypt messages here (repository layer)
			foreach (var m in messages)
			{
				m.EncryptedMessage = DecodeBase64(m.EncryptedMessage);
			}

			return messages;
		}

		private string DecodeBase64(string encodedText)
		{
			if (string.IsNullOrEmpty(encodedText))
				return encodedText;

			try
			{
				var bytes = Convert.FromBase64String(encodedText);
				return System.Text.Encoding.UTF8.GetString(bytes);
			}
			catch
			{
				// In case something is already plain text
				return encodedText;
			}
		}
	}
}
