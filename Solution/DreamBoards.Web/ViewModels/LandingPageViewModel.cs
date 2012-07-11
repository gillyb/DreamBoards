using System.Collections.Generic;
using DreamBoards.DataAccess.DataObjects;
using DreamBoards.Domain.User;

namespace DreamBoards.Web.ViewModels
{
	public class LandingPageViewModel
	{
		public User User { get; set; }
		public List<BoardDto> UsersBoards { get; set; }
		public List<BoardDto> PopularBoards { get; set; }
	}
}