using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PlatformClient.Platform;

namespace MiamiApp.Domain.AppWalls
{
	public interface IAppWallServices
	{
		AppWall AddAppWall(string name, string description, string headerContentPath, string rightContentPath, string headerContentHeight);
	}

	public class AppWallServices : IAppWallServices
	{
		private readonly IPlatformProxy _platformProxy;
		
		public AppWallServices(IPlatformProxy platformProxy)
		{
			_platformProxy = platformProxy;
		}

		public AppWall AddAppWall(string name, string description, string headerContentPath, string rightContentPath, string headerContentHeight)
		{
			var addAppWallParams = new[]
			                       	{
			                       		new KeyValuePair<string, object>("name", name),
			                       		new KeyValuePair<string, object>("description", description),
			                       		new KeyValuePair<string, object>("headerContentPath", headerContentPath),
			                       		new KeyValuePair<string, object>("rightContentPath", rightContentPath),
										new KeyValuePair<string, object>("headerContentHeight", headerContentHeight)
			                       	};

			var newAppWall = _platformProxy.Get<AppWall>("/app-walls/create", addAppWallParams);
			return newAppWall;
		}
	}
}
