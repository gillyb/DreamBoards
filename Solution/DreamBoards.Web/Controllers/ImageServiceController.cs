using System.Configuration;
using System.Web.Mvc;
using CommonGround.MvcInvocation;
using DreamBoards.Web.Services;

namespace DreamBoards.Web.Controllers
{
	public class ImageServiceController : Controller
	{
		private readonly IImageService _imageService;

		public ImageServiceController(IImageService imageService)
		{
			_imageService = imageService;
		}

		[PatternRoute("/-/images/make-transparent")]
		public ActionResult MakeTransparent(string imageUrl)
		{
			var newImageFileName = _imageService.MakeImageTransparent(imageUrl);
			var newImageUrl = string.Format("{0}/{1}/{2}",
			    ConfigurationManager.AppSettings["DreamBoardsDomain"],
			    ConfigurationManager.AppSettings["ItemImagesLibrary"],
			    newImageFileName);

			return Content(newImageUrl);
		}
	}
}