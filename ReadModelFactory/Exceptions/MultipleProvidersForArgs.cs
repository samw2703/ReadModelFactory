using System;

namespace ReadModelFactory
{
	internal class MultipleProvidersForArgs : Exception
	{
		public MultipleProvidersForArgs(Type argsType)
			: base($"Multiple providers exist for args {argsType.FullName}")
		{
		}
	}
}