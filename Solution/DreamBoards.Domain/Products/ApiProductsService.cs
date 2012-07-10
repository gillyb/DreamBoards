using System.Collections.Generic;
using PlatformClient.Platform;

namespace DreamBoards.Domain.Products
{
	public interface IApiProductsService
	{
		List<Product> DiscoverByCategoryId(long categoryId);
	}

	public class ApiProductsService : IApiProductsService
	{
		private readonly IPlatformProxy _platformProxy;

		public ApiProductsService(IPlatformProxy platformProxy)
		{
			_platformProxy = platformProxy;
		}

		public List<Product> DiscoverByCategoryId(long categoryId)
		{
			var discoverCategoryParams = new[] {
				new KeyValuePair<string, object>("categoryIds", new[] {categoryId}),
				new KeyValuePair<string, object>("maxItems", 20)
			};

			var productIds = _platformProxy.Get<List<long>>("/products/discover/by-category-ids", discoverCategoryParams);

			var getProductsParamas = new[] {
				new KeyValuePair<string, object>("ids", productIds)
			};
			var products = _platformProxy.Get<List<Product>>("/products/get", getProductsParamas);

			return products;
		}
	}
}