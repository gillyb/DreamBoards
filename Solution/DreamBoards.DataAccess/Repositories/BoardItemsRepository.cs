using System.Collections.Generic;
using System.Linq;
using DreamBoards.DataAccess.DataObjects;

namespace DreamBoards.DataAccess.Repositories
{
	public interface IBoardItemsRepository
	{
		List<BoardItemDto> GetBoardItems(int boardId);
		void SaveBoardItems(int boardId, List<BoardItemDto> boardItems);
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

		public void SaveBoardItems(int boardId, List<BoardItemDto> boardItems)
		{
			using (var sessionFactory = _sessionFactoryProvider.BuildSessionFactory())
			using (var session = sessionFactory.OpenSession())
			{
				// delete all the existing items
				var existingItems = session.QueryOver<BoardItemDto>()
					.Where(x => x.BoardId == boardId)
					.List<BoardItemDto>();
				foreach (var item in existingItems)
					session.Delete(item);
				session.Flush();

				// insert the new ones
				foreach (var newItem in boardItems)
				{
					session.Save(new BoardItemDto {
						BoardId = boardId,
						Height = newItem.Height,
						Width = newItem.Width,
						ImageUrl = newItem.ImageUrl,
						Layer = newItem.Layer,
						PosX = newItem.PosX,
						PosY = newItem.PosY,
						ProductId = newItem.ProductId,
						CatalogId = newItem.CatalogId,
						Rotation = newItem.Rotation
					});
					session.Flush();
				}
			}
		}
	}
}