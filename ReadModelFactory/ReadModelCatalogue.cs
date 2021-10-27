using System;
using System.Collections.Generic;
using System.Linq;

namespace ReadModelFactory
{
	internal class ReadModelCatalogue
	{
		private readonly List<ReadModelCatalogueItem> _items;

		public ReadModelCatalogue(List<ReadModelCatalogueItem> items)
		{
			_items = items;
		}

		public ReadModelProvider<TReadModel, TArgs> GetProvider<TReadModel, TArgs>(IServiceProvider serviceProvider)
		{
			var catalogueItem = _items.SingleOrDefault(x => x.ReadModelType == typeof(TReadModel) && x.ArgsType == typeof(TArgs));
			if (catalogueItem == null)
				throw new NoReadModelProvider(typeof(TReadModel));

			return serviceProvider.GetService(catalogueItem.ProviderType) as ReadModelProvider<TReadModel, TArgs>;
		}
	}
}