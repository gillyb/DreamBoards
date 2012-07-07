using System;
using DreamBoards.Domain.Settings;
using PlatformClient.Platform;

namespace DreamBoards.Domain.PlatformApiServices
{
    public class PlatformConfiguration : IPlatformConfiguration
    {
        private readonly IPlatformSettings _platformSettings;
        private readonly IApplicationSettings _applicationSettings;

        public PlatformConfiguration(IPlatformSettings platformSettings, IApplicationSettings applicationSettings)
        {
            _platformSettings = platformSettings;
            _applicationSettings = applicationSettings;
        }

        public string AppSecret
        {
            get { return _applicationSettings.AppSecret; }
        }

        public long AppId
        {
            get { return _applicationSettings.AppId; }
        }

        public Uri PlatformApiBaseUrl
        {
            get { return new Uri(_platformSettings.PlatformApiBaseUrl); }
        }

		public Uri PlatformSecureApiBaseUrl
		{
			get { return new Uri(_platformSettings.PlatformSecureApiBaseUrl); }
		}

        public string PlatformPagesBaseUrl
        {
            get { return _platformSettings.PlatformPagesBaseUrl; }
        }

    	public string PlatformHomePage
    	{
			get { return _platformSettings.PlatformHomePage; }
    	}

    }

   
}
