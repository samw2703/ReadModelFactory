using System;
using System.Collections.Generic;
using System.Linq;

namespace ReadModelFactory
{
	internal class ReadModelCatalogue
	{
		private readonly List<ReadModelCatalogueItem> _items;
		private readonly IServiceProvider _serviceProvider;

		public ReadModelCatalogue(List<ReadModelCatalogueItem> items, IServiceProvider serviceProvider)
		{
			_items = items;
			_serviceProvider = serviceProvider;
		}

		public ReadModelProvider<TReadModel, TArgs> GetProvider<TReadModel, TArgs>()
		{
			var catalogueItem = _items.SingleOrDefault(x => x.ReadModelType == typeof(TReadModel));
			if (catalogueItem == null)
				throw new NoReadModelProvider(typeof(TReadModel));

			var provider = _serviceProvider.GetService(catalogueItem.ProviderType) as ReadModelProvider<TReadModel, TArgs>;
			if (provider == null)
				throw new ReadModelArgsMismatch(typeof(TArgs), typeof(TReadModel));

			return provider;
		}
	}
}