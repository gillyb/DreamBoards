using DreamBoards.DataAccess.DataObjects;
using FluentNHibernate.Mapping;

namespace DreamBoards.DataAccess.Mappings
{
	public class BoardItemDtoMap : ClassMap<BoardItemDto>
	{
		public BoardItemDtoMap()
		{
			Table("board_items");
			Not.LazyLoad();

			Id(x => x.ItemId);
			Map(x => x.BoardId);
			Map(x => x.ProductId);
			Map(x => x.CatalogId);
			Map(x => x.PosX);
			Map(x => x.PosY);
			Map(x => x.Width);
			Map(x => x.Height);
			Map(x => x.Rotation);
			Map(x => x.Layer);
		}
	}
}