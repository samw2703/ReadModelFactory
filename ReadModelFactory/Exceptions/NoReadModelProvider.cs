using System;

namespace ReadModelFactory
{
	internal class NoReadModelProvider : Exception
	{
		public NoReadModelProvider(Type readModelType)
			: base($"No read model provider is registered for read model of type {readModelType.FullName}")
		{
		}
	}
}