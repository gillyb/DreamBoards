using CommonGround.Settings;

namespace MiamiApp.Domain.Settings
{
    public interface IPlatformSettings
    {
        string PlatformApiBaseUrl { get;  }

		string PlatformSecureApiBaseUrl { get;  }

        string PlatformPagesBaseUrl { get;  }

		string PlatformHomePage { get; }
    }

	public class PlatformSettings : IPlatformSettings
	{
		public string PlatformApiBaseUrl { get { return "http://ohio.platform:88"; }}
		public string PlatformSecureApiBaseUrl { get { return "http://ohio.platform:88"; }}
		public string PlatformPagesBaseUrl { get { return "//ohio.local/app/"; }}
		public string PlatformHomePage { get { return "http://ohio.local"; }}
	}
}