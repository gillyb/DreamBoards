using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;

namespace DreamBoards.DataAccess
{
	public interface ISessionFactoryProvider
	{
		ISessionFactory BuildSessionFactory();
	}

	public class SessionFactoryProvider : ISessionFactoryProvider
	{
		public string ConnString { get; private set; }

		public SessionFactoryProvider(string connString)
		{
			ConnString = connString;
		}

		public ISessionFactory BuildSessionFactory()
		{
			return Fluently.Configure().Database(
				MySQLConfiguration.Standard.ConnectionString(ConnString)
			).Mappings(
				x => x.FluentMappings.AddFromAssemblyOf<SessionFactoryProvider>()
			).BuildSessionFactory();
		}
	}
}