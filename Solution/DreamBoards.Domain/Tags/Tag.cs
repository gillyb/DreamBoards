using System;

namespace DreamBoards.Domain.Tags
{
	public class Tag
	{
		public long Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public DateTime CreationDate { get; set; }
		public string Url { get; set; }
		public string ImageUrl { get; set; }
		public bool IsCurrentUserFollow { get; set; }
	}
}
