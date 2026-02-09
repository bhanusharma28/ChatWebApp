using SignalRChatWebAPI.Models;

namespace SignalRChatWebAPI.Interfaces.IRepository
{
	public interface IUserRepository
	{
		Task<User?> GetUserByNameAsync(string name);
		Task<List<User>> GetAllUsersAsync();
	}
}
