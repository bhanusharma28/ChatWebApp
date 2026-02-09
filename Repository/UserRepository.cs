using Microsoft.EntityFrameworkCore;
using SignalRChatWebAPI.Data;
using SignalRChatWebAPI.Interfaces.IRepository;
using SignalRChatWebAPI.Models;

namespace SignalRChatWebAPI.Repository
{
	public class UserRepository : IUserRepository
	{
		private readonly ChatAppDbContext chatAppDbContext;

		public UserRepository(ChatAppDbContext chatAppDbContext)
		{
			this.chatAppDbContext = chatAppDbContext;
		}

		public async Task<User?> GetUserByNameAsync(string name)
		{
			return await chatAppDbContext.Users
				.Where(u => u.Name.ToLower() == name.ToLower())
				.FirstOrDefaultAsync();
		}

		public async Task<List<User>> GetAllUsersAsync()
		{
			return await chatAppDbContext.Users.ToListAsync();
		}

	}
}
