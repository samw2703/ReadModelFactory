using System;

namespace ReadModelFactory
{
	internal class ReadModelCatalogueItem
	{
		public Type ArgsType { get; }
		public Type ProviderType { get; }

		public ReadModelCatalogueItem(Type argsType, Type providerType)
		{
			ArgsType = argsType;
			ProviderType = providerType;
		}
	}
}