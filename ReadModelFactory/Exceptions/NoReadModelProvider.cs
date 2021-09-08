using System;

namespace ReadModelFactory
{
	internal class NoReadModelProvider : Exception
	{
		public NoReadModelProvider(Type argsType)
			: base($"No read model provider is registered for argument of type {argsType.FullName}")
		{
		}
	}
}