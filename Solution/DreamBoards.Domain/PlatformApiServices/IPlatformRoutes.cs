using System;
using DreamBoards.Domain.Settings;

namespace DreamBoards.Domain.PlatformApiServices
{
	public interface IPlatformRoutes
	{
		string RewardsLogin { get; set; }
		string Login { get; set; }
		string RegularCanvas { get; set; }
	}

	public class PlatformRoutes : IPlatformRoutes
	{
		public string RewardsLogin { get; set; }
		public string Login { get; set; }
		public string RegularCanvas { get; set; }

		public PlatformRoutes(IApplicationSettings applicationSettings,IPlatformSettings platformSettings)
		{
			RewardsLogin = platformSettings.PlatformHomePage + String.Format("/secured/app/{0}/rewards-sign-up", applicationSettings.AppId);
			Login = platformSettings.PlatformHomePage + String.Format("/secured/app/{0}/login", applicationSettings.AppId);
			RegularCanvas = String.Format("/app/{0}/r/", applicationSettings.AppId);
		}
	}
}
