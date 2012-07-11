using System.Collections.Generic;
using DreamBoards.DataAccess.DataObjects;
using DreamBoards.Domain.User;

namespace DreamBoards.Web.ViewModels
{
	public class CanvasPageViewModel
	{
		public User User { get; set; }
		public List<KeyValuePair<string, int>> Cateogories { get; set; } 
		public BoardDto Board { get; set; }
		public List<BoardItemDto> BoardItems { get; set; }
	}
}