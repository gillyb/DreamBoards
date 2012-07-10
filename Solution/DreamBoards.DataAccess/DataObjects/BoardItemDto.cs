namespace DreamBoards.DataAccess.DataObjects
{
	public class BoardItemDto
	{
		public int ItemId { get; set; }
		public int BoardId { get; set; }
		public int ProductId { get; set; }
		public int CatalogId { get; set; }

		public string ImageUrl { get; set; }
		public int PosX { get; set; }
		public int PosY { get; set; }
		public int Width { get; set; }
		public int Height { get; set; }
		public int Rotation { get; set; }
		public int Layer { get; set; }
	}
}