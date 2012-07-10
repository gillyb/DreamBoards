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
		public CanvasActionsController()
		{
		}

		[HttpPost]
		[PatternRoute("/-/canvas/save")]
		public ActionResult SaveBoard(BoardItemDto[] boardItems)
		{
			return Content("OK");
		}
	}
}