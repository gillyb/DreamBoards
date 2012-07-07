using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;

namespace DreamBoards.DataAccess
{
	public class SessionFactoryProvider
	{
		public ISessionFactory BuildSessionFactory(string connString)
		{
			return Fluently.Configure().Database(
				MySQLConfiguration.Standard.ConnectionString(connString)
			).Mappings(
				x => x.FluentMappings.AddFromAssemblyOf<SessionFactoryProvider>()
			).BuildSessionFactory();
		}
	}
}