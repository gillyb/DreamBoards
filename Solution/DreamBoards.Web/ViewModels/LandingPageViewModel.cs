using System.Collections.Generic;
using MiamiApp.Domain.Tags;
using MiamiApp.Domain.User;

namespace MiamiApp.Web.ViewModels
{
	public class LandingPageViewModel
	{
		public IEnumerable<Tag> Tags { get; set; }
		public string UserName { get; set; }
		public UserState UserState { get; set; }
		public string LoginRedirectUrl { get; set; }
	}
}