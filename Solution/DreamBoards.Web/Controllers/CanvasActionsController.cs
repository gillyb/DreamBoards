using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;
using CommonGround.MvcInvocation;
using DreamBoards.DataAccess.DataObjects;
using DreamBoards.DataAccess.Repositories;
using DreamBoards.Domain.Newsfeed;
using DreamBoards.Domain.Settings;
using DreamBoards.Domain.User;
using DreamBoards.Web.Services;
using ControllerBase = CommonGround.MvcInvocation.ControllerBase;

namespace DreamBoards.Web.Controllers
{
	public class CanvasActionsController : ControllerBase
	{
		private readonly IBoardItemsRepository _boardItemsRepository;
		private readonly IImageService _imageService;
		private readonly IApiUsersService _apiUsersService;
		private readonly IApiNewsfeedService _apiNewsfeedService;
		private readonly IBoardsRepository _boardsRepository;
		private readonly IPlatformSettings _platformSettings;
		private readonly IApplicationSettings _applicationSettings;

		public CanvasActionsController(IBoardItemsRepository boardItemsRepository, IImageService imageService, IApiUsersService apiUsersService, IBoardsRepository boardsRepository, IApiNewsfeedService apiNewsfeedService, IPlatformSettings platformSettings, IApplicationSettings applicationSettings)
		{
			_boardItemsRepository = boardItemsRepository;
			_imageService = imageService;
			_apiUsersService = apiUsersService;
			_boardsRepository = boardsRepository;
			_apiNewsfeedService = apiNewsfeedService;
			_platformSettings = platformSettings;
			_applicationSettings = applicationSettings;
		}

		[HttpPost]
		[PatternRoute("/-/canvas/save")]
		public ActionResult SaveBoard(int boardId, List<BoardItemDto> boardItems, string template)
		{
			var boardTemplate = (!string.IsNullOrEmpty(template)) ? template.Replace("url(", "").Replace(")", "").Replace("\"","") : "";
			var user = _apiUsersService.GetCurrentUser();
			
			var board = _boardsRepository.GetBoard(boardId);
			board.BoardTemplate = boardTemplate;
			_boardsRepository.UpdateBoard(board);

			if (board.UserId != user.Id)
				throw new UnauthorizedAccessException("You can not save changes to a board that doesn't belong to you");

			_boardItemsRepository.SaveBoardItems(boardId, boardItems);
			_imageService.SaveBoardAsImage(boardItems, board.BoardTemplate);

			return Json("OK");
		}

		[HttpPost]
		[PatternRoute("/-/canvas/save-as-image")]
		public ActionResult SaveBoardAsImage(List<BoardItemDto> boardItems, string boardTemplate)
		{
			_imageService.SaveBoardAsImage(boardItems, boardTemplate);
			return Content("OK");
		}

		[HttpPost]
		[PatternRoute("/-/canvas/brag")]
		public ActionResult PublishBoardOnNewsfeed(int boardId, string boardTitle, List<BoardItemDto> boardItems, string template)
		{
			var boardTemplate = (!string.IsNullOrEmpty(template)) ? template.Replace("url(", "").Replace(")", "").Replace("\"","") : "";
			_imageService.SaveBoardAsImage(boardItems, boardTemplate);
			var board = _boardsRepository.GetBoard(boardId);

			var user = _apiUsersService.GetCurrentUser();
			_apiNewsfeedService.PublishStoryOnWall(user.Id, boardTitle,
				"Check out this cool DreamBoard, created by " + user.Name,
				string.Format("{0}/{1}/{2}", ConfigurationManager.AppSettings["DreamBoardsDomain"], ConfigurationManager.AppSettings["BoardImagesLibrary"], board.BoardImage),
				string.Format("{0}/{1}/r?boardId={2}", _platformSettings.PlatformPagesBaseUrl, _applicationSettings.AppId, boardId));

			return Content("OK");
		}
	}
}