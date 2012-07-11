using System.Collections.Generic;
using DreamBoards.DataAccess.DataObjects;
using DreamBoards.Domain.Tags;
using DreamBoards.Domain.User;

namespace DreamBoards.Web.ViewModels
{
	public class LandingPageViewModel
	{
		public IEnumerable<Tag> Tags { get; set; }
		public string UserName { get; set; }
		public UserState UserState { get; set; }

		public int BoardId { get; set; }
		public List<BoardItemDto> BoardItems { get; set; } 
	}
}