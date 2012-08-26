namespace DreamBoards.Domain.Settings
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
		// Production
		//public string PlatformApiBaseUrl { get { return "http://platform.shopyourway.com"; } }
		//public string PlatformSecureApiBaseUrl { get { return "http://platform.shopyourway.com"; } }
		//public string PlatformPagesBaseUrl { get { return "//platform.shopyourway.com/app"; } }
		//public string PlatformHomePage { get { return "http://platform.shopyourway.com"; } }

		// Galileo
		public string PlatformApiBaseUrl { get { return "http://galileoplatform.shopyourway.com"; } }
		public string PlatformSecureApiBaseUrl { get { return "http://galileoplatform.shopyourway.com"; } }
		public string PlatformPagesBaseUrl { get { return "//galileo.shopyourway.com/app"; } }
		public string PlatformHomePage { get { return "http://galileo.shopyourway.com"; } }

		// Shavrir
		//public string PlatformApiBaseUrl { get { return "http://shavrirps.shopyourway.com:88"; } }
		//public string PlatformSecureApiBaseUrl { get { return "http://shavrirps.shopyourway.com:88"; } }
		//public string PlatformPagesBaseUrl { get { return "//ohio.local/app/"; } }
		//public string PlatformHomePage { get { return "http://ohio.local"; } }

		// Local
		//public string PlatformApiBaseUrl { get { return "http://ohio.platform:88"; } }
		//public string PlatformSecureApiBaseUrl { get { return "http://ohio.platform:88"; } }
		//public string PlatformPagesBaseUrl { get { return "//ohio.local/app/"; } }
		//public string PlatformHomePage { get { return "http://ohio.local"; } }
	}
}