using System.Collections.Generic;
using PlatformClient.Platform;

namespace DreamBoards.Domain.Newsfeed
{
	public interface IApiNewsfeedService
	{
		void PublishStoryOnWall(long userId, string title, string content, string imageUrl, string targetUrl);
	}

	public class ApiNewsfeedService : IApiNewsfeedService
	{
		private readonly IPlatformProxy _platformProxy;

		public ApiNewsfeedService(IPlatformProxy platformProxy)
		{
			_platformProxy = platformProxy;
		}

		public void PublishStoryOnWall(long userId, string title, string content, string imageUrl, string targetUrl)
		{
			var publishParams = new[] {
				//new KeyValuePair<string, object>("ids", new[] { userId }),
				new KeyValuePair<string, object>("userAction", "shared this"),
				new KeyValuePair<string, object>("title", title),
				new KeyValuePair<string, object>("content", content),
				new KeyValuePair<string, object>("imageUrl", imageUrl),
				new KeyValuePair<string, object>("targetUrl", targetUrl)
			};
			_platformProxy.Get<string>("/wall/publish-user-action", publishParams);
		}
	}
}