using System;
using System.Web;
using System.Web.Mvc;
using DreamBoards.Domain.Settings;

namespace DreamBoards.Web.Filters
{
	public interface ITokenPersistenceFilter : IActionFilter
	{
	}

	public class TokenPersistenceFilter : ITokenPersistenceFilter
	{
		private readonly IApplicationSettings _applicationSettings;

		public TokenPersistenceFilter(IApplicationSettings applicationSettings)
		{
			_applicationSettings = applicationSettings;
		}

		public void OnActionExecuting(ActionExecutingContext filterContext)
		{
			CreateOrUpdateCookie(filterContext);
		}

		private void CreateOrUpdateCookie(ActionExecutingContext filterContext)
		{

			var token = filterContext.HttpContext.Request["token"];

			if (String.IsNullOrEmpty(token)) return;

			var cookie = filterContext.HttpContext.Response.Cookies[_applicationSettings.CookieName];

			if (cookie == null)
			{
				cookie = new HttpCookie(_applicationSettings.CookieName);
				filterContext.HttpContext.Response.Cookies.Add(cookie);
			}
			cookie["token"] = token;
		}

		public void OnActionExecuted(ActionExecutedContext filterContext)
		{
			
		}
	}
}