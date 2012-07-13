using System;

namespace DreamBoards.DataAccess.DataObjects
{
	public class BoardDto
	{
		public int Id { get; set; }
		public long UserId { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public string BoardImage { get; set; }
		public string BoardTemplate { get; set; }
		public DateTime CreatedDate { get; set; }
	}
}