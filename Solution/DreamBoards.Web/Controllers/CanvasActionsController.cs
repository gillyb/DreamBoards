using System.Collections.Generic;
using System.Web.Mvc;
using CommonGround.MvcInvocation;
using DreamBoards.DataAccess.DataObjects;
using DreamBoards.DataAccess.Repositories;
using ControllerBase = CommonGround.MvcInvocation.ControllerBase;

namespace DreamBoards.Web.Controllers
{
	public class CanvasActionsController : ControllerBase
	{
		private readonly IBoardItemsRepository _boardItemsRepository;

		public CanvasActionsController(IBoardItemsRepository boardItemsRepository)
		{
			_boardItemsRepository = boardItemsRepository;
		}

		[HttpPost]
		[PatternRoute("/-/canvas/save")]
		public ActionResult SaveBoard(int boardId, List<BoardItemDto> boardItems)
		{
			_boardItemsRepository.SaveBoardItems(boardId, boardItems);

			return Json("OK");
		}
	}
}