using System;
using System.Drawing.Imaging;
using System.Linq;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using DreamBoards.DataAccess.DataObjects;

namespace DreamBoards.Web.Services
{
	public interface IImageService
	{
		string MakeImageTransparent(string imageUrl);
		Bitmap SaveBoardAsImage(List<BoardItemDto> boardItems);
	}

	public class ImageService : IImageService
	{
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
			finalImage.Save("gilly-image.jpg", ImageFormat.Jpeg);
			return finalImage;
		}

		private Image GetImageFromUrl(string imageUrl)
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

			return null;
		}

		private static void DownloadRemoteImageFile(string uri, string fileName)
		{
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
			HttpWebResponse response = (HttpWebResponse)request.GetResponse();

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
				using (Stream inputStream = response.GetResponseStream())
				using (Stream outputStream = File.OpenWrite(fileName))
				{
					byte[] buffer = new byte[4096];
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