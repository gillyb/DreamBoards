﻿using System.Collections.Generic;
using System.Web.Mvc;
using CommonGround.MvcInvocation;
using DreamBoards.Domain.PlatformApiServices;
using DreamBoards.Domain.Tags;
using DreamBoards.Domain.User;
using DreamBoards.Web.ViewModels;
using PlatformClient.Platform;
using ControllerBase = CommonGround.MvcInvocation.ControllerBase;

namespace DreamBoards.Web.Controllers
{
	public class LandingController : ControllerBase
    {
    	private readonly IPlatformProxy _platformProxy;
		private readonly IPlatformRoutes _platformRoutes;

		public LandingController(IPlatformProxy platformProxy,IPlatformRoutes platformRoutes)
    	{
    		_platformProxy = platformProxy;
    		_platformRoutes = platformRoutes;
    	}

		[PatternRoute("/landing")]
		public ActionResult Landing()
		{
			var s = _platformProxy.Get<List<long>>("/products/discover/popular");

			var userState = _platformProxy.Get<UserState>("auth/user-state");

			var viewModel = new LandingPageViewModel();

			if (userState == UserState.Authenticated || userState == UserState.Authorized)
			{
				var currentUser = _platformProxy.Get<User>("/users/current");
				viewModel.UserName = currentUser.Name;
			}

			var wantedTagsIds = new List<long>
			           	{
			           		220057L,
			           		220015L
			           	};

			var tagList = new KeyValuePair<string, object>("ids",wantedTagsIds);
			var followedTags = _platformProxy.Get<List<Tag>>("/tags/get", tagList);

			viewModel.LoginRedirectUrl = _platformRoutes.Login;
			viewModel.Tags = followedTags;
			viewModel.UserState = userState;

			return View(viewModel);
		}
    }
}
