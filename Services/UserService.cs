using SignalRChatWebAPI.Interfaces.IRepository;
using SignalRChatWebAPI.Interfaces.IService;
using SignalRChatWebAPI.Models;

namespace SignalRChatWebAPI.Services
{
	public class UserService : IUserService
	{
		private readonly IUserRepository userRepository;

		public UserService(IUserRepository userRepository)
		{
			this.userRepository = userRepository;
		}

		public async Task<User?> GetUserByNameAsync(string name)
		{
			if (string.IsNullOrWhiteSpace(name))
				throw new ArgumentException("Name cannot be empty");

			return await userRepository.GetUserByNameAsync(name);
		}

		public async Task<List<User>> GetAllUsersAsync()
		{
			return await userRepository.GetAllUsersAsync();
		}
	}
}
