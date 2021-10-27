using System;

namespace ReadModelFactory
{
	internal class ReadModelCatalogueItem
	{
		public Type ArgsType { get; set; }
		public Type ReadModelType { get; }
		public Type ProviderType { get; }

		public ReadModelCatalogueItem(Type argsType, Type readModelType, Type providerType)
		{
			ArgsType = argsType;
			ReadModelType = readModelType;
			ProviderType = providerType;

		}
	}
}