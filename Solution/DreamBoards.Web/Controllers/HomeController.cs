using System.Collections.Generic;
using System.Web.Mvc;
using CommonGround.MvcInvocation;
using DreamBoards.Domain.Products;
using DreamBoards.Domain.Settings;
using DreamBoards.Web.Services;
using DreamBoards.Web.ViewModels;
using PlatformClient.Platform;

namespace DreamBoards.Web.Controllers
{
    public class HomeController : Controller
    {
		private readonly IPlatformProxy _platformProxy;
    	private readonly IApplicationSettings _applicationSettings;
    	private readonly IPlatformSettings _platformSettings;
    	private readonly IApiProductsService _apiProductsService;
    	private readonly IImageService _imageService;

		public HomeController(IPlatformProxy platformProxy, IApplicationSettings applicationSettings, IPlatformSettings platformSettings, IApiProductsService apiProductsService, IImageService imageService)
    	{
    		_platformProxy = platformProxy;
			_applicationSettings = applicationSettings;
			_platformSettings = platformSettings;
			_apiProductsService = apiProductsService;
			_imageService = imageService;
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

			var productsParams = new[] {
			    new KeyValuePair<string, object>("categoryIds", new[] { 1005L })
			};

			var products = _apiProductsService.DiscoverByCategoryId(1005);
			
            return View(viewModel);
        }

		[PatternRoute("/test")]
		public ActionResult Test()
		{


			return View();
		}

		[PatternRoute("/test2")]
		public ActionResult GetImageTest(string url)
		{
			var newImageFile = _imageService.MakeImageTransparent(url);
			return Content(newImageFile);
		}

		[PatternRoute("/publish-story")]
		[HttpPost]
		public ActionResult PublishStory(string postTitle,string postContent)
		{

			var postStoryParams = new[] {
				new KeyValuePair<string, object>("userAction", "shared this"),
				new KeyValuePair<string, object>("title", postTitle),
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
