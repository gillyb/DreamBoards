using System;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using MiamiApp.Domain.Settings;
using PlatformClient.Platform;

namespace MiamiApp.Web.Filters
{
	public interface ITokenExtractorFilter : IAuthorizationFilter
	{
	}

	public class TokenExtractorFilter : ITokenExtractorFilter
	{
		private readonly IPlatformTokenProvider _platformTokenProvider;
		private readonly IApplicationSettings _applicationSettings;

		public TokenExtractorFilter(IPlatformTokenProvider platformTokenProvider,IApplicationSettings applicationSettings)
		{
			_platformTokenProvider = platformTokenProvider;
			_applicationSettings = applicationSettings;
		}

		public void OnAuthorization(AuthorizationContext filterContext)
		{
			var token = GetToken(filterContext);

			_platformTokenProvider.SetToken(token);

		}

		private string GetToken(AuthorizationContext filterContext)
		{
			var token = filterContext.HttpContext.Request.QueryString["token"];
			if (!String.IsNullOrEmpty(token))
				return token;
			
			token =  filterContext.HttpContext.Request.Form["token"];
			if (!String.IsNullOrEmpty(token))
				return token;
			
			var cookie = filterContext.HttpContext.Request.Cookies[_applicationSettings.CookieName];
			if (cookie != null)
				token = cookie["token"];

			return token;
		}


	}

	public class InvalidTokenException : Exception
	{
		public InvalidTokenException(string myMessage):base(myMessage)
		{
			
		}
	}
}