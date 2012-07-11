using DreamBoards.DataAccess.DataObjects;
using FluentNHibernate.Mapping;

namespace DreamBoards.DataAccess.Mappings
{
	public class BoardDtoMap : ClassMap<BoardDto>
	{
		public BoardDtoMap()
		{
			Table("boards");
			Not.LazyLoad();
			
			Id(x => x.Id);
			Map(x => x.UserId);
			Map(x => x.Title);
			Map(x => x.Description);
			Map(x => x.CreatedDate);
		}
	}
}