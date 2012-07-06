using System.Collections.Generic;
using System.Web.Mvc;
using CommonGround.MvcInvocation;
using MiamiApp.Domain.Settings;
using MiamiApp.Web.ViewModels;
using PlatformClient.Platform;

namespace MiamiApp.Web.Controllers
{
    public class HomeController : Controller
    {
        
		private readonly IPlatformProxy _platformProxy;
    	private readonly IApplicationSettings _applicationSettings;
    	private readonly IPlatformSettings _platformSettings;

		public HomeController(IPlatformProxy platformProxy, IApplicationSettings applicationSettings, IPlatformSettings platformSettings)
    	{
    		_platformProxy = platformProxy;
			_applicationSettings = applicationSettings;
			_platformSettings = platformSettings;
    	}

		[PatternRoute("/")]
        public ActionResult RegularCanvas()
		{
			HomeViewModel viewModel = new HomeViewModel
			                          	{
			                          		FullCanvasURL =
			                          			string.Format("{0}{1}/f/", _platformSettings.PlatformPagesBaseUrl,
			                          			              _applicationSettings.AppId),
			                          		RegularCanvasURL =
			                          			string.Format("{0}{1}/r/", _platformSettings.PlatformPagesBaseUrl,
			                          			              _applicationSettings.AppId),
											PlatformHomePage = _platformSettings.PlatformHomePage
			                          	};
			
            return View(viewModel);
        }

		[PatternRoute("/test")]
		public ActionResult Test()
		{
			return Content("OK");
		}

		[PatternRoute("/publish-story")]
		[HttpPost]
		public ActionResult PublishStory(string postTitle,string postContent)
		{

			var postStoryParams = new[]
            {
                new KeyValuePair<string, object>("userAction", "shared this"),
                new KeyValuePair<string, object>("title", postTitle ),
                new KeyValuePair<string, object>("content", postContent)
            };

			_platformProxy.Get<string>("/wall/publish-user-action", postStoryParams);
			
			return JsonVoid();
		}

    	private ActionResult JsonVoid()
    	{
    		return Json("OK");
    	}
    }
}
