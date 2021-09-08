using System.Collections.Generic;
using System.Linq;

namespace ReadModelFactory
{
	internal class ReadModelCatalogueItemsValidator
	{
		public void Validate(List<ReadModelCatalogueItem> items)
		{
			foreach (var item in items)
			{
				if (items.Count(x => x.ReadModelType == item.ReadModelType) > 1)
					throw new MultipleProviders(item.ReadModelType);
			}
		}
	}
}