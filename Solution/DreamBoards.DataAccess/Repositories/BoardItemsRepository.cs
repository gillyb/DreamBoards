using System.Collections.Generic;
using System.Linq;
using DreamBoards.DataAccess.DataObjects;

namespace DreamBoards.DataAccess.Repositories
{
	public interface IBoardItemsRepository
	{
		List<BoardItemDto> GetBoardItems(int boardId);
	}

	public class BoardItemsRepository : IBoardItemsRepository
	{
		private readonly ISessionFactoryProvider _sessionFactoryProvider;

		public BoardItemsRepository(ISessionFactoryProvider sessionFactoryProvider)
		{
			_sessionFactoryProvider = sessionFactoryProvider;
		}

		public List<BoardItemDto> GetBoardItems(int boardId)
		{
			using (var sessionFactory = _sessionFactoryProvider.BuildSessionFactory())
			using (var session = sessionFactory.OpenSession())
			{
				var boardItems = session.QueryOver<BoardItemDto>()
					.Where(x => x.BoardId == boardId)
					.List<BoardItemDto>();
				return boardItems.ToList();
			}
		}
	}
}