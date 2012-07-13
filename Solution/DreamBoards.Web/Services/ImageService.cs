using System;
using System.Drawing.Imaging;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Web;
using DreamBoards.DataAccess.DataObjects;
using DreamBoards.DataAccess.Repositories;

namespace DreamBoards.Web.Services
{
	public interface IImageService
	{
		string BoardImagesLibrary { get; }

		string MakeImageTransparent(string imageUrl);
		void SaveBoardAsImage(List<BoardItemDto> boardItems, string boardTemplate);
	}

	public class ImageService : IImageService
	{
		private readonly IBoardsRepository _boardsRepository;

		public ImageService(IBoardsRepository boardsRepository)
		{
			_boardsRepository = boardsRepository;
		}

		public string BoardImagesLibrary
		{
			get { return HttpContext.Current.Server.MapPath("~") + "UGC\\board_images\\"; }
		}
		public string ItemImagesLibrary
		{
			get { return HttpContext.Current.Server.MapPath("~") + "UGC\\items\\"; }
		}

		public string MakeImageTransparent(string imageUrl)
		{
			var image = GetImageFromUrl(imageUrl);
			var bgColor = image.GetPixel(0, 0);

			// TODO: add some kind of variety of colors for this... (not just white, make it white -> light grey)
			for (var i = 0; i < image.Width; i++)
				for (var j = 0; j < image.Height; j++)
				{
					var pixelColor = image.GetPixel(i, j);
					if (ColorDistance(pixelColor, bgColor) <= 80)
						image.SetPixel(i, j, Color.Transparent);
					//if (image.GetPixel(i, j) == bgColor)
					//    image.SetPixel(i, j, Color.Transparent);
				}

			image.MakeTransparent(Color.Transparent);

			var fileName = Guid.NewGuid().ToString() + ".png";
			var filePath = ItemImagesLibrary + fileName;
			image.Save(filePath, ImageFormat.Png);

			return fileName;
		}

		private int ColorDistance(Color pixelColor, Color bgColor)
		{
			return Math.Abs(pixelColor.R - bgColor.R) +
			       Math.Abs(pixelColor.G - bgColor.G) +
			       Math.Abs(pixelColor.B - bgColor.B);
		}

		public void SaveBoardAsImage(List<BoardItemDto> boardItems, string boardTemplate)
		{
			using (var finalImage = new Bitmap(656, 600))
			{
				using (var finalGraphics = Graphics.FromImage(finalImage))
				{
					if (!string.IsNullOrEmpty(boardTemplate))
					{
						var image = GetImageFromUrl(boardTemplate);
						finalGraphics.DrawImage(image, 0, 0);
					}
					else
					{
						finalGraphics.Clear(Color.White);
					}

					foreach (var item in boardItems)
					{
						var itemImage = GetImageFromUrl(item.ImageUrl);
						finalGraphics.DrawImage(itemImage, (float) item.PosX, (float) item.PosY,
						                        (float) item.Width, (float) item.Height);
					}
				}

				var fileName = Guid.NewGuid().ToString() + ".png";
				var filePath = BoardImagesLibrary + fileName;
				finalImage.Save(filePath, ImageFormat.Png);

				var board = _boardsRepository.GetBoard(boardItems[0].BoardId);
				board.BoardImage = fileName;
				_boardsRepository.UpdateBoard(board);
			}
		}

		private Bitmap GetImageFromUrl(string imageUrl)
		{
			try
			{
				var request = (HttpWebRequest)WebRequest.Create(imageUrl);
				var response = (HttpWebResponse)request.GetResponse();

				if ((response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Moved ||
					response.StatusCode == HttpStatusCode.Redirect) && response.ContentType.StartsWith("image", StringComparison.OrdinalIgnoreCase))
				{
					using (var inputStream = response.GetResponseStream())
						if (inputStream != null)
							return new Bitmap(inputStream);
				}
			}
			// ReSharper disable EmptyGeneralCatchClause
			catch { }
			// ReSharper restore EmptyGeneralCatchClause

			return null;
		}
	}
}