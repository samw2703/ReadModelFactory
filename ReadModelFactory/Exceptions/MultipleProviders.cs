using System;

namespace ReadModelFactory
{
	internal class MultipleProviders : Exception
	{
		public MultipleProviders(Type readModelType)
			: base($"Multiple providers exist for read model {readModelType.FullName}")
		{
		}
	}
}