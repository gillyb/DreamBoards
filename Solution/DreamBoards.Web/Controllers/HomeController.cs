using System.Collections.Generic;
using System.Web.Mvc;
using CommonGround.MvcInvocation;
using DreamBoards.DataAccess.DataObjects;
using DreamBoards.DataAccess.Repositories;
using DreamBoards.Domain.Products;
using DreamBoards.Domain.Settings;
using DreamBoards.Domain.User;
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
    	private readonly IBoardsRepository _boardsRepository;
    	private readonly IBoardItemsRepository _boardItemsRepository;

		public HomeController(IPlatformProxy platformProxy, IApplicationSettings applicationSettings, IPlatformSettings platformSettings, IApiProductsService apiProductsService, IImageService imageService, IBoardsRepository boardsRepository, IBoardItemsRepository boardItemsRepository)
    	{
    		_platformProxy = platformProxy;
			_applicationSettings = applicationSettings;
			_platformSettings = platformSettings;
			_apiProductsService = apiProductsService;
			_imageService = imageService;
			_boardsRepository = boardsRepository;
			_boardItemsRepository = boardItemsRepository;
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
		public ActionResult Test(int boardId)
		{
			var userState = _platformProxy.Get<UserState>("auth/user-state");

			var viewModel = new CanvasPageViewModel();

			if (userState == UserState.Authenticated || userState == UserState.Authorized)
			{
				viewModel.User = _platformProxy.Get<User>("/users/current");

				if (viewModel.User != null)
				{
					var board = _boardsRepository.GetBoard(boardId);
					if (board.UserId == viewModel.User.Id)
					{
						viewModel.Board = board;
						viewModel.BoardItems = _boardItemsRepository.GetBoardItems(board.Id);
					}
				}
			}

			if (viewModel.BoardItems == null)
				viewModel.BoardItems = new List<BoardItemDto>();

			viewModel.Cateogories = GetCategoryList();

			return View(viewModel);
		}

    	private static List<KeyValuePair<string, int>> GetCategoryList()
    	{
			// TagId's from production - i need to find the matching category id's...
			//return new List<KeyValuePair<string, int>> { // TODO: these are actually tagid's - i need to find there matching category id
			//    new KeyValuePair<string, int>("Dresses", 1077153),
			//    new KeyValuePair<string, int>("Swimwear", 1077162),
			//    new KeyValuePair<string, int>("Tops", 1077163),
			//    new KeyValuePair<string, int>("Jeans", 1077155),
			//    new KeyValuePair<string, int>("Skirts", 1077159),
			//    new KeyValuePair<string, int>("Necklaces", 1079382),
			//    new KeyValuePair<string, int>("Earrings", 1079383),
			//    new KeyValuePair<string, int>("Jackets", 1077154),
			//    new KeyValuePair<string, int>("Various", 2658148),
			//    new KeyValuePair<string, int>("Sunglasses", 1077029) };

			// Shavrir Categories
			return new List<KeyValuePair<string, int>> {
    			new KeyValuePair<string, int>("Dresses", 1002),
    			new KeyValuePair<string, int>("Swimwear", 1005),
    			new KeyValuePair<string, int>("Tops", 1010),
    			new KeyValuePair<string, int>("Jeans", 1014),
    			new KeyValuePair<string, int>("Skirts", 1015),
    			new KeyValuePair<string, int>("Necklaces", 1016),
    			new KeyValuePair<string, int>("Earrings", 1018),
    			new KeyValuePair<string, int>("Jackets", 1019),
    			new KeyValuePair<string, int>("Various", 1020),
    			new KeyValuePair<string, int>("Sunglasses", 1021) };
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
