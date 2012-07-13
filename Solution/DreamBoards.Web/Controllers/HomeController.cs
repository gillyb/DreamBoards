using System.Collections.Generic;
using System.Linq;
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
        public ActionResult Boards()
		{
			var model = new BoardsPageViewModel();
			var userState = _platformProxy.Get<UserState>("auth/user-state");
			if (userState == UserState.Authenticated || userState == UserState.Authorized)
			{
				model.User = _platformProxy.Get<User>("/users/current");
				model.UsersBoards = _boardsRepository.GetUsersBoards(model.User.Id);
			}

			model.PopularBoards = _boardsRepository.GetPopularBoards()
				.Where(x => !string.IsNullOrEmpty(x.BoardImage)).ToList();

			return View(model);
        }

		[PatternRoute("/canvas-page")]
		public ActionResult CanvasPage(int boardId)
		{
			var userState = _platformProxy.Get<UserState>("auth/user-state");

			var viewModel = new CanvasPageViewModel();
			viewModel.HostDomain = _platformSettings.PlatformHomePage;

			BoardDto board = null;
			if (userState == UserState.Authenticated || userState == UserState.Authorized)
			{
				viewModel.User = _platformProxy.Get<User>("/users/current");

				if (viewModel.User != null)
				{
					board = _boardsRepository.GetBoard(boardId);
					if (board != null)
					{
						viewModel.Board = board;
						viewModel.BoardItems = _boardItemsRepository.GetBoardItems(board.Id);
					}
				}
			}
			viewModel.ReadOnly = (board != null && board.UserId != viewModel.User.Id);

			if (viewModel.BoardItems == null)
				viewModel.BoardItems = new List<BoardItemDto>();

			viewModel.Cateogories = GetCategoryList();

			return View(viewModel);
		}

    	private static List<KeyValuePair<string, int>> GetCategoryList()
    	{
			// Prod Categories
			return new List<KeyValuePair<string, int>> {
			    new KeyValuePair<string, int>("Sunglasses", 1702),
			    new KeyValuePair<string, int>("Dresses", 1250),
			    new KeyValuePair<string, int>("Jackets & Blazers", 1283),
			    new KeyValuePair<string, int>("Jeans", 1310),
			    new KeyValuePair<string, int>("Skirts", 1405),
			    new KeyValuePair<string, int>("Swimsuits", 1478),
			    new KeyValuePair<string, int>("Tops", 1352),
			    new KeyValuePair<string, int>("Necklaces", 11565),
			    new KeyValuePair<string, int>("Rings", 11762),
			    new KeyValuePair<string, int>("Sweaters", 1294),
				new KeyValuePair<string, int>("Jewelery", 1527),
				new KeyValuePair<string, int>("Intimates", 1424),
				new KeyValuePair<string, int>("Pajamas", 1435),
				new KeyValuePair<string, int>("Gold Jewelery", 1528),
				new KeyValuePair<string, int>("T-Shirts", 1316),
				new KeyValuePair<string, int>("Handbags", 1508),
				new KeyValuePair<string, int>("Silver Jewelery", 1544),
				new KeyValuePair<string, int>("Jewelery", 1512) };

			// Shavrir Categories
			//return new List<KeyValuePair<string, int>> {
			//    new KeyValuePair<string, int>("Dresses", 1002),
			//    new KeyValuePair<string, int>("Swimwear", 1005),
			//    new KeyValuePair<string, int>("Tops", 1010),
			//    new KeyValuePair<string, int>("Jeans", 1014),
			//    new KeyValuePair<string, int>("Skirts", 1015),
			//    new KeyValuePair<string, int>("Necklaces", 1016),
			//    new KeyValuePair<string, int>("Earrings", 1018),
			//    new KeyValuePair<string, int>("Jackets", 1019),
			//    new KeyValuePair<string, int>("Various", 1020),
			//    new KeyValuePair<string, int>("Sunglasses", 1021) };
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
