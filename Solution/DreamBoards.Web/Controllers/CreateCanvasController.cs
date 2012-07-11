using System;
using System.Web.Mvc;
using System.Linq;
using CommonGround.MvcInvocation;
using DreamBoards.DataAccess.DataObjects;
using DreamBoards.DataAccess.Repositories;
using DreamBoards.Domain.Products;
using DreamBoards.Domain.User;
using ControllerBase = CommonGround.MvcInvocation.ControllerBase;

namespace DreamBoards.Web.Controllers
{
	public class CreateCanvasController : ControllerBase
	{
		private readonly IApiProductsService _apiProductsService;
		private readonly IApiUsersService _apiUsersService;
		private readonly IBoardsRepository _boardsRepository;

		public CreateCanvasController(IApiProductsService apiProductsService, IApiUsersService apiUsersService, IBoardsRepository boardsRepository)
		{
			_apiProductsService = apiProductsService;
			_apiUsersService = apiUsersService;
			_boardsRepository = boardsRepository;
		}

		[HttpPost]
		[PatternRoute("/-/canvas/new")]
		public ActionResult CreateNewCanvas(string name, string description)
		{
			var currentUser = _apiUsersService.GetCurrentUser();
			_boardsRepository.CreateNewBoard(new BoardDto
			{
				UserId = currentUser.Id,
				Title = name,
				Description = description,
				CreatedDate = DateTime.Now
			});
			return Json("OK");
		}

		[HttpPost]
		[PatternRoute("/-/platform/get-products-for-category")]
		public ActionResult GetCategoryProducts(long categoryId)
		{
			var products = _apiProductsService.DiscoverByCategoryId(categoryId);

			var clientProductInfo = products
				.Where(x => !string.IsNullOrEmpty(x.ImageUrl))
				.Select(x => new {productId = x.Id, imageUrl = x.ImageUrl}).ToList();

			return Json(clientProductInfo);
		}
	}
}