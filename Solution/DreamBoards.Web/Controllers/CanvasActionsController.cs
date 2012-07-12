using System;
using System.Collections.Generic;
using System.Web.Mvc;
using CommonGround.MvcInvocation;
using DreamBoards.DataAccess.DataObjects;
using DreamBoards.DataAccess.Repositories;
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
		private readonly IBoardsRepository _boardsRepository;

		public CanvasActionsController(IBoardItemsRepository boardItemsRepository, IImageService imageService, IApiUsersService apiUsersService, IBoardsRepository boardsRepository)
		{
			_boardItemsRepository = boardItemsRepository;
			_imageService = imageService;
			_apiUsersService = apiUsersService;
			_boardsRepository = boardsRepository;
		}

		[HttpPost]
		[PatternRoute("/-/canvas/save")]
		public ActionResult SaveBoard(int boardId, List<BoardItemDto> boardItems)
		{
			var user = _apiUsersService.GetCurrentUser();
			var board = _boardsRepository.GetBoard(boardId);

			if (board.UserId != user.Id)
				throw new UnauthorizedAccessException("You can not save changes to a board that doesn't belong to you");

			_boardItemsRepository.SaveBoardItems(boardId, boardItems);
			_imageService.SaveBoardAsImage(boardItems);

			return Json("OK");
		}

		[HttpPost]
		[PatternRoute("/-/canvas/save-as-image")]
		public ActionResult SaveBoardAsImage(List<BoardItemDto> boardItems)
		{
			var result = new ContentResult();
			result.ContentType = "image/JPEG";
			result.Content = _imageService.SaveBoardAsImage(boardItems).ToString();
			return result;
		}
	}
}