using PlatformClient.Platform;

namespace DreamBoards.Domain.User
{
	public interface IApiUsersService
	{
		User GetCurrentUser();
	}

	public class ApiUsersService : IApiUsersService
	{
		private readonly IPlatformProxy _platformProxy;

		public ApiUsersService(IPlatformProxy platformProxy)
		{
			_platformProxy = platformProxy;
		}

		public User GetCurrentUser()
		{
			return _platformProxy.Get<User>("/users/current");
		}
	}
}