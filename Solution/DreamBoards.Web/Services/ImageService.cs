using System;
using System.IO;
using System.Net;

namespace DreamBoards.Web.Services
{
	public interface IImageService
	{
		string MakeImageTransparent(string imageUrl);
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