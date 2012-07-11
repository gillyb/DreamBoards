using System.Collections.Generic;
using System.Web.Mvc;
using CommonGround.MvcInvocation;
using DreamBoards.DataAccess.Repositories;
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
		private readonly IBoardsRepository _boardsRepository;
		private readonly IBoardItemsRepository _boardItemsRepository;

		public LandingController(IPlatformProxy platformProxy,IPlatformRoutes platformRoutes, IBoardsRepository boardsRepository, IBoardItemsRepository boardItemsRepository)
    	{
    		_platformProxy = platformProxy;
    		_platformRoutes = platformRoutes;
			_boardsRepository = boardsRepository;
			_boardItemsRepository = boardItemsRepository;
    	}

		[PatternRoute("/landing")]
		public ActionResult Landing(int boardId)
		{
			var userState = _platformProxy.Get<UserState>("auth/user-state");

			var viewModel = new LandingPageViewModel();

			if (userState == UserState.Authenticated || userState == UserState.Authorized)
			{
				var currentUser = _platformProxy.Get<User>("/users/current");
				viewModel.UserName = currentUser.Name;

				var board = _boardsRepository.LoadBoard(boardId);
				if (board.UserId != currentUser.Id)
				{
					viewModel.BoardId = board.Id;
					viewModel.BoardItems = _boardItemsRepository.GetBoardItems(board.Id);
				}
			}

			return View(viewModel);
		}
    }
}
