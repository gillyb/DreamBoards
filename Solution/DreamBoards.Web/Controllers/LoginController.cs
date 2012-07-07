﻿using System.Collections.Generic;
using System.Web.Mvc;
using CommonGround.MvcInvocation;
using DreamBoards.Domain.AppWalls;
using DreamBoards.Domain.PlatformApiServices;
using DreamBoards.Domain.Settings;
using MiamiApp.Web.ViewModels;
using PlatformClient.Platform;

namespace DreamBoards.Web.Controllers
{
	public class LoginController : Controller
	{
		private readonly IPlatformProxy _platformProxy;
		private readonly IPlatformRoutes _platformRoutes;
		private readonly IApplicationSettings _applicationSettings;
		private readonly IAppWallServices _appWallServices;

		public LoginController(IPlatformProxy platformProxy,IPlatformRoutes platformRoutes,IApplicationSettings applicationSettings, IAppWallServices appWallServices)
		{
			_platformProxy = platformProxy;
			_platformRoutes = platformRoutes;
			_applicationSettings = applicationSettings;
			_appWallServices = appWallServices;
		}

		[PatternRoute("/post-login")]
		public ActionResult PostLogin()
		{
			return View();
		}

		[PatternRoute("/login")]
		public ActionResult Login()
		{
			return View();
		}


		[PatternRoute("/install-app")]
		public ActionResult InstallApp()
		{
			InstallAppForUser();
			RegisterAppLink();
			AddAppToTag();
			var newAppWall = _appWallServices.AddAppWall("My Canvas", "My Canvas is the best!!", "/appwall/header", "/appwall/right", "80");

			var viewModel = new InstallAppViewModel
								{
									AppWallUrl = newAppWall.Url
								};

			return View(viewModel);
		}

		

		private void AddAppToTag()
		{
			var addToTagParams = new[]
			                     	{
			                     		new KeyValuePair<string, object>("title", "My Canvas"),
			                     		new KeyValuePair<string, object>("tagId", "220057") //Airflow Collectibles
			                     	};


			_platformProxy.Get<string>("/applinks/register/for-tag", addToTagParams);
		}

		private void RegisterAppLink()
		{
			var appLinkParams = new[]
			                    	{
			                    		new KeyValuePair<string, object>("title", "My Canvas"),
			                    		new KeyValuePair<string, object>("url", _platformRoutes.RegularCanvas)
			                    	};


			_platformProxy.Get<string>("/applinks/register", appLinkParams);
		}

		private void InstallAppForUser()
		{
			_platformProxy.Get<string>("app/install");
		}
	}
}
