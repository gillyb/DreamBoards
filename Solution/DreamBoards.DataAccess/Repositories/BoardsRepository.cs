using System.Collections.Generic;
using System.Linq;
using DreamBoards.DataAccess.DataObjects;

namespace DreamBoards.DataAccess.Repositories
{
	public interface IBoardsRepository
	{
		int CreateNewBoard(BoardDto board);
		BoardDto GetBoard(int boardId);
		void UpdateBoard(BoardDto board);
		List<BoardDto> GetUsersBoards(long userId);
		List<BoardDto> GetPopularBoards();
	}

	public class BoardsRepository : IBoardsRepository
	{
		private readonly ISessionFactoryProvider _sessionFactoryProvider;

		public BoardsRepository(ISessionFactoryProvider sessionFactoryProvider)
		{
			_sessionFactoryProvider = sessionFactoryProvider;
		}

		public int CreateNewBoard(BoardDto board)
		{
			int newBoardId;
			using (var sessionFactory = _sessionFactoryProvider.BuildSessionFactory())
			using (var session = sessionFactory.OpenSession())
			{
				newBoardId = (int)session.Save(board);
				session.Flush();
			}
			return newBoardId;
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

		public List<BoardDto> GetPopularBoards()
		{
			using (var sessionFactory = _sessionFactoryProvider.BuildSessionFactory())
			using (var session = sessionFactory.OpenSession())
			{
				var boards = session.QueryOver<BoardDto>()
					.OrderBy(x => x.CreatedDate).Asc
					.Take(20)
					.List<BoardDto>();
				return boards.ToList();
			}
		}
	}
}