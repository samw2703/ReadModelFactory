using System;

namespace ReadModelFactory
{
	internal class ReadModelArgsMismatch : Exception
	{
		public ReadModelArgsMismatch(Type argsType, Type readModelType)
			: base($"The provider that is registered for {argsType.FullName} does not return a {readModelType.FullName}")
		{
		}
	}
}