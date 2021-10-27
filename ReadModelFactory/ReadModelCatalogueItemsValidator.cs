using System.Collections.Generic;
using System.Linq;

namespace ReadModelFactory
{
	internal class ReadModelCatalogueItemsValidator
	{
		public void Validate(List<ReadModelCatalogueItem> items)
		{
			ValidateArgsAreDistinctArgs(items);
			ValidateNoArgsProvidersReturnDistinctReadModels(items);
		}

		private void ValidateNoArgsProvidersReturnDistinctReadModels(List<ReadModelCatalogueItem> items)
		{
			var noArgsItems = items.Where(x => x.ArgsType == typeof(NoReadModelArgs)).ToList();
			var distinctNoArgsReadModels = noArgsItems.Select(x => x.ReadModelType).Distinct();
			foreach (var readModelType in distinctNoArgsReadModels)
			{
				if (noArgsItems.Count(x => x.ReadModelType == readModelType) > 1)
					throw new MultipleNoArgsProviders(readModelType);
			}
		}

		private void ValidateArgsAreDistinctArgs(List<ReadModelCatalogueItem> items)
		{
			var distinctArgsTypes = items.Select(x => x.ArgsType).Distinct();
			foreach (var argsType in distinctArgsTypes)
			{
				if (argsType == typeof(NoReadModelArgs))
					continue;

				if (items.Count(x => x.ArgsType == argsType) > 1)
					throw new MultipleProvidersForArgs(argsType);
			}
		}
	}
}