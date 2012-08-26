using System.Linq;
using System.Web.Mvc;
using CommonGround.MvcInvocation;
using DreamBoards.DataAccess.Repositories;
using DreamBoards.Domain.Settings;
using DreamBoards.Domain.User;
using DreamBoards.Web.ViewModels;
using PlatformClient.Platform;
using ControllerBase = CommonGround.MvcInvocation.ControllerBase;

namespace DreamBoards.Web.Controllers
{
	public class LandingController : ControllerBase
    {
    	private readonly IPlatformProxy _platformProxy;
		private readonly IBoardsRepository _boardsRepository;
		private readonly IPlatformSettings _platformSettings;
		private readonly IApplicationSettings _applicationSettings;

		public LandingController(IPlatformProxy platformProxy, IBoardsRepository boardsRepository, IPlatformSettings platformSettings, IApplicationSettings applicationSettings)
    	{
    		_platformProxy = platformProxy;
			_boardsRepository = boardsRepository;
			_platformSettings = platformSettings;
			_applicationSettings = applicationSettings;
    	}

		[PatternRoute("/boards")]
		public ActionResult Boards(int boardId)
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

		[PatternRoute("/landing")]
		public ActionResult Landing()
		{
			var landingPageViewModel = new LandingPageViewModel {
				SignInUrl = string.Format("https:{0}/{1}/login", _platformSettings.PlatformPagesBaseUrl, _applicationSettings.AppId)
			};
			return View(landingPageViewModel);
		}
    }
}
