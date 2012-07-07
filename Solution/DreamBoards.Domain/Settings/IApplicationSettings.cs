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
		public string AppSecret { get { return "d836f9ae-70f5-4477-8226-97e14d61"; } }

		public long AppId { get { return 100; } }

		public string CookieName { get { return "dreamboards"; } }
	}
}
