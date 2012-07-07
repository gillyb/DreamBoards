using System.Web.Mvc;
using CommonGround.MvcInvocation;

namespace DreamBoards.Web.Filters
{
	public class MyCanvasAppActionInvoker : ActionInvoker
	{
		private readonly ITokenExtractorFilter _tokenExtractorFilter;
		private readonly IMyCanvasAppExceptionFilter _myCanvasAppExceptionFilter;
		private readonly ITokenPersistenceFilter _tokenPersistenceFilter;

		public MyCanvasAppActionInvoker(ITokenExtractorFilter tokenExtractorFilter, IMyCanvasAppExceptionFilter myCanvasAppExceptionFilter,ITokenPersistenceFilter tokenPersistenceFilter)
		{
			_tokenExtractorFilter = tokenExtractorFilter;
			_myCanvasAppExceptionFilter = myCanvasAppExceptionFilter;
			_tokenPersistenceFilter = tokenPersistenceFilter;
		}

		public override bool InvokeAction(ControllerContext controllerContext, string actionName)
		{
			// Allows users to post HTML to our website, we XSS it upon rendering so we should be fine.
			// We do not want to fail requests that contain what seems to be HTML
			controllerContext.Controller.ValidateRequest = false;
			return base.InvokeAction(controllerContext, actionName);
		}

		protected override FilterInfo GetFilters(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
		{
			var filterInfo = base.GetFilters(controllerContext, actionDescriptor);

			filterInfo.AuthorizationFilters.Add(_tokenExtractorFilter);
			//filterInfo.ExceptionFilters.Add(_myCanvasAppExceptionFilter);
			filterInfo.ActionFilters.Add(_tokenPersistenceFilter);

			return filterInfo;
		}
	}
}