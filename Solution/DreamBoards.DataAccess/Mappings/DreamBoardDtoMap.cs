using DreamBoards.DataAccess.DataObjects;
using FluentNHibernate.Mapping;

namespace DreamBoards.DataAccess.Mappings
{
	public class DreamBoardDtoMap : ClassMap<DreamBoardDto>
	{
		public DreamBoardDtoMap()
		{
			Table("dreamboards");
			Not.LazyLoad();
			
			Id(x => x.Id);
			Map(x => x.UserId);
			Map(x => x.Title);
			Map(x => x.Description);
			Map(x => x.CreatedDate);
		}
	}
}