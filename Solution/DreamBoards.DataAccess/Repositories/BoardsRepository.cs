using System.Collections.Generic;
using System.Linq;
using DreamBoards.DataAccess.DataObjects;

namespace DreamBoards.DataAccess.Repositories
{
	public interface IBoardsRepository
	{
		void CreateNewBoard(BoardDto board);
		BoardDto GetBoard(int boardId);
		void UpdateBoard(BoardDto board);
		List<BoardDto> GetUsersBoards(long userId);
	}

	public class BoardsRepository : IBoardsRepository
	{
		private readonly ISessionFactoryProvider _sessionFactoryProvider;

		public BoardsRepository(ISessionFactoryProvider sessionFactoryProvider)
		{
			_sessionFactoryProvider = sessionFactoryProvider;
		}

		public void CreateNewBoard(BoardDto board)
		{
			using (var sessionFactory = _sessionFactoryProvider.BuildSessionFactory())
			using (var session = sessionFactory.OpenSession())
			{
				session.Save(board);
				session.Flush();
			}
		}

		public BoardDto GetBoard(int boardId)
		{
			using (var sessionFactory = _sessionFactoryProvider.BuildSessionFactory())
			using (var session = sessionFactory.OpenSession())
			{
				var board = session.QueryOver<BoardDto>()
					.Where(x => x.Id == boardId)
					.SingleOrDefault<BoardDto>();
				return board;
			}
		}

		public void UpdateBoard(BoardDto board)
		{
			using (var sessionFactory = _sessionFactoryProvider.BuildSessionFactory())
			using (var session = sessionFactory.OpenSession())
			{
				session.Update(board);
				session.Flush();
			}
		}

		public List<BoardDto> GetUsersBoards(long userId)
		{
			using (var sessionFactory = _sessionFactoryProvider.BuildSessionFactory())
			using (var session = sessionFactory.OpenSession())
			{
				var boards = session.QueryOver<BoardDto>()
					.Where(x => x.UserId == userId)
					.List<BoardDto>();
				return boards.ToList();
			}
		}
	}
}