using System.Web.Mvc;
using CommonGround.MvcInvocation;
using DreamBoards.Domain.AppWalls;
using DreamBoards.Domain.Settings;
using DreamBoards.Web.ViewModels;

namespace DreamBoards.Web.Controllers
{
    public class AppWallController : Controller
    {
    	private readonly IAppWallServices _appWallServices;
    	private readonly IPlatformSettings _platformSettings;
		
		public AppWallController(IAppWallServices appWallServices, IPlatformSettings platformSettings)
		{
			_appWallServices = appWallServices;
			_platformSettings = platformSettings;
		}
		
		[PatternRoute("/appwall/header")]
        public ActionResult HeaderContent(int height)
        {
			var model = new AppWallViewModel();
			model.HeaderCanvasHeight = 80;
			model.HeaderCanvasHeight = height;
			return View(model);
        }

		[PatternRoute("/appwall/right")]
		public ActionResult RightContent()
		{
			return View();
		}

		[PatternRoute("/create-app-wall")]
		public ActionResult
			CreateAppWall(string name, string description, string headerContentPath, string rightContentPath, string headerContentHeight)
		{
			if (null == name) name = "Default App Wall Name";
			if (null == description) description = "";
			if (null == headerContentPath) headerContentPath = "/appwall/header";
			if (null == rightContentPath) rightContentPath = "/appwall/right";
			if (null == headerContentHeight) headerContentHeight = "";

 			var newAppWall = _appWallServices.AddAppWall(name, description, headerContentPath, rightContentPath, headerContentHeight);

			var viewModel = new InstallAppViewModel
			{
				AppWallUrl = _platformSettings.PlatformHomePage + newAppWall.Url
			};

			return View(viewModel);
		}

		[PatternRoute("/new-app-wall-form")]
		public ActionResult NewAppWallForm()
		{
			return View();
		}
    }
}
