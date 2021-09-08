using System;

namespace ReadModelFactory
{
	internal class ReadModelCatalogueItem
	{
		public Type ReadModelType { get; }
		public Type ProviderType { get; }

		public ReadModelCatalogueItem(Type readModelType, Type providerType)
		{
			ReadModelType = readModelType;
			ProviderType = providerType;
		}
	}
}