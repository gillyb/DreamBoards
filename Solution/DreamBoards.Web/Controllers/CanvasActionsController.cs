using System.Collections.Generic;
using System.Web.Mvc;
using CommonGround.MvcInvocation;
using DreamBoards.DataAccess.DataObjects;
using DreamBoards.DataAccess.Repositories;
using DreamBoards.Web.Services;
using ControllerBase = CommonGround.MvcInvocation.ControllerBase;

namespace DreamBoards.Web.Controllers
{
	public class CanvasActionsController : ControllerBase
	{
		private readonly IBoardItemsRepository _boardItemsRepository;
		private readonly IImageService _imageService;

		public CanvasActionsController(IBoardItemsRepository boardItemsRepository, IImageService imageService)
		{
			_boardItemsRepository = boardItemsRepository;
			_imageService = imageService;
		}

		[HttpPost]
		[PatternRoute("/-/canvas/save")]
		public ActionResult SaveBoard(int boardId, List<BoardItemDto> boardItems)
		{
			_boardItemsRepository.SaveBoardItems(boardId, boardItems);

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