namespace DreamBoards.DataAccess.DataObjects
{
	public class BoardItemDto
	{
		public int BoardId { get; set; }
		public int ProductId { get; set; }
		public int CatalogId { get; set; }

		public string ImageUrl { get; set; }
		public int ImagePosX { get; set; }
		public int ImagePosY { get; set; }
		public int ImageWidth { get; set; }
		public int ImageHeight { get; set; }
		public int ImageRotation { get; set; }
		public int Layer { get; set; }
	}
}