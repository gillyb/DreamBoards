using System.Configuration;

namespace DreamBoards.Web.Services
{
	public static class ViewHelperService
	{
		 public static string GetImageBoardUrl(string image)
		 {
		 	return string.Format("{0}/{1}/{2}",
		 	    ConfigurationManager.AppSettings["DreamBoardsDomain"],
		 	    ConfigurationManager.AppSettings["BoardImagesLibrary"],
		 	    image);
		 }
	}
}