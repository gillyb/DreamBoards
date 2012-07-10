using System;

namespace DreamBoards.Domain.Products
{
	public class Product
	{
		public long Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public decimal Price { get; set; }
		public int Rating { get; set; }
		public string ImageUrl { get; set; }
		public string ProductUrl { get; set; }
		public long CategoryId { get; set; }
		public string CategoryName { get; set; }
		public string Source { get; set; }
		public string SourceProductId { get; set; }
		public int NumberOfBuyingOptions { get; set; }
		public DateTime LastUpdated { get; set; }
	}
}