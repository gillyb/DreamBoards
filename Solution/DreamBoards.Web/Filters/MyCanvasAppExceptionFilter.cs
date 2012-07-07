using System.Web.Mvc;
using Castle.Core.Logging;

namespace DreamBoards.Web.Filters
{
	public interface IMyCanvasAppExceptionFilter : IExceptionFilter
	{

	}
	public class MyCanvasAppExceptionFilter:IMyCanvasAppExceptionFilter
	{
		private readonly ILogger _logger;

		public MyCanvasAppExceptionFilter(ILogger logger)
		{
			_logger = logger;
		}

		public void OnException(ExceptionContext filterContext)
		{
			//here write to logs
			var ajax = string.IsNullOrEmpty(filterContext.HttpContext.Request.Headers["X-Requested-With"]) == false;
			_logger.Error(filterContext.Exception.Message, filterContext.Exception);

			if (ajax)
			{
				return;
			}

			//here call to view with error message
			filterContext.ExceptionHandled = true;
			filterContext.HttpContext.Response.StatusCode = 500;
			filterContext.Result = new ViewResult() { ViewName = "~/views/Errors/GeneralError.aspx" };
		}
	}
}