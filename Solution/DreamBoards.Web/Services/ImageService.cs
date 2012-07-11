using System;
using System.Drawing.Imaging;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
		Bitmap SaveBoardAsImage(List<BoardItemDto> boardItems);
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
			get { return HttpContext.Current.Server.MapPath("~") + "UGC\\board_image\\"; }
		}

		public string MakeImageTransparent(string imageUrl)
		{
			// TODO: add validation for image file extension
			// TODO: download image from given url
			// TODO: make the image transparent
			// TODO: save on our server, and return url

			DownloadRemoteImageFile(imageUrl, "static/test.jpg");

			return string.Empty;
		}

		public Bitmap SaveBoardAsImage(List<BoardItemDto> boardItems)
		{
			var finalImage = new Bitmap(600, 600);
			using (var finalGraphics = Graphics.FromImage(finalImage))
			{
				finalGraphics.Clear(Color.White);
				foreach (var item in boardItems)
				{
					var itemImage = GetImageFromUrl(item.ImageUrl);
					finalGraphics.DrawImage(itemImage, (float)item.PosX, (float)item.PosY,
						(float)item.Width, (float)item.Height);
				}
			}

			var fileName = Guid.NewGuid().ToString() + ".jpg";
			var filePath = BoardImagesLibrary + fileName;
			finalImage.Save(filePath, ImageFormat.Jpeg);

			var board = _boardsRepository.GetBoard(boardItems[0].BoardId);
			board.BoardImage = fileName;
			_boardsRepository.UpdateBoard(board);

			return finalImage;
		}

		private Image GetImageFromUrl(string imageUrl)
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

		private static void DownloadRemoteImageFile(string uri, string fileName)
		{
			var request = (HttpWebRequest)WebRequest.Create(uri);
			var response = (HttpWebResponse)request.GetResponse();

			// Check that the remote file was found. The ContentType
			// check is performed since a request for a non-existent
			// image file might be redirected to a 404-page, which would
			// yield the StatusCode "OK", even though the image was not
			// found.
			if ((response.StatusCode == HttpStatusCode.OK ||
				response.StatusCode == HttpStatusCode.Moved ||
				response.StatusCode == HttpStatusCode.Redirect) &&
				response.ContentType.StartsWith("image", StringComparison.OrdinalIgnoreCase))
			{
				// if the remote file was found, download oit
				using (var inputStream = response.GetResponseStream())
				using (var outputStream = File.OpenWrite(fileName))
				{
					var buffer = new byte[4096];
					int bytesRead;
					do
					{
						bytesRead = inputStream.Read(buffer, 0, buffer.Length);
						outputStream.Write(buffer, 0, bytesRead);
					} while (bytesRead != 0);
				}
			}
		}

	}
}