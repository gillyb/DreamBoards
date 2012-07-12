using System.Linq;
using System.Web.Mvc;
using CommonGround.MvcInvocation;
using DreamBoards.DataAccess.Repositories;
using DreamBoards.Domain.PlatformApiServices;
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
		private readonly IBoardsRepository _boardsRepository;
		private readonly IBoardItemsRepository _boardItemsRepository;

		public LandingController(IPlatformProxy platformProxy,IPlatformRoutes platformRoutes, IBoardsRepository boardsRepository, IBoardItemsRepository boardItemsRepository)
    	{
    		_platformProxy = platformProxy;
    		_platformRoutes = platformRoutes;
			_boardsRepository = boardsRepository;
			_boardItemsRepository = boardItemsRepository;
    	}

		[PatternRoute("/boards")]
		public ActionResult Boards(int boardId)
		{
			var model = new LandingPageViewModel();
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

		[PatternRoute("/landing")]
		public ActionResult Landing()
		{
			return View();
		}
    }
}
