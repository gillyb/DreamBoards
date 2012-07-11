using System.Configuration;

namespace DreamBoards.Web.Services
{
	public static class ImageServiceSettings
	{
		public static string BoardImageLibrary
		{
			get { return ConfigurationManager.AppSettings["BoardImagesPath"]; }
		}
	}
}