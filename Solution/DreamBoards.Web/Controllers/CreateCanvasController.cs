using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using CommonGround.MvcInvocation;
using DreamBoards.Domain.Products;
using ControllerBase = CommonGround.MvcInvocation.ControllerBase;

namespace DreamBoards.Web.Controllers
{
	public class CreateCanvasController : ControllerBase
	{
		private readonly IApiProductsService _apiProductsService;

		public CreateCanvasController(IApiProductsService apiProductsService)
		{
			_apiProductsService = apiProductsService;
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