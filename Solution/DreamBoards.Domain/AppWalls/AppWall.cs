using System;

namespace MiamiApp.Domain.AppWalls
{
	public class AppWall
	{
		public Int64 Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public DateTime CreationDate { get; set; }
		public string HeaderContentPath { get; set; }
		public string RightContentPath { get; set; }
		public string Url { get; set; }
	}
}
