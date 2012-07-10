namespace DreamBoards.Domain.Settings
{
    public interface IApplicationSettings
    {
        string AppSecret { get; }
        long AppId { get; }
    	string CookieName { get; }
    }

	public class ApplicationSettings : IApplicationSettings
	{
		public string AppSecret { get { return "1E3E6AEE-6B7E-4A05-93BD-60F6087F"; } }

		public long AppId { get { return 777; } }

		public string CookieName { get { return "dreamboards"; } }
	}
}
