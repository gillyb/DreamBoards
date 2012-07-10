using System;

namespace DreamBoards.DataAccess.DataObjects
{
	public class DreamBoardDto
	{
		public long Id { get; set; }
		public long UserId { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public DateTime CreatedDate { get; set; }
	}
}