using System.Collections.Generic;
using System.Web.Mvc;
using CommonGround.MvcInvocation;
using DreamBoards.Domain.Tags;
using PlatformClient.Platform;
using System.Linq;

namespace DreamBoards.Web.Controllers
{
    public class TagController : Controller
    {
        
		private readonly IPlatformProxy _platformProxy;

		public TagController(IPlatformProxy platformProxy)
    	{
    		_platformProxy = platformProxy;
    	}

		[PatternRoute("/tag")]
        public ActionResult TagCanvas(int tagId)
		{
			var tagList = new KeyValuePair<string, object>("ids", tagId.ToString());
			var tag = _platformProxy.Get<List<Tag>>("/tags/get", tagList).SingleOrDefault();

			var viewModels = new TagCanvasViewModel()
			             	{
			             		TagName = tag.Name
			             	};
			return View(viewModels);
		}


    }

	public class TagCanvasViewModel
	{
		public string TagName { get; set; }
	}
}
