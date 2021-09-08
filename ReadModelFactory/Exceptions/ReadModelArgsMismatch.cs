using System;

namespace ReadModelFactory
{
	internal class ReadModelArgsMismatch : Exception
	{
		public ReadModelArgsMismatch(Type argsType, Type readModelType)
			: base($"The provider that is registered for {readModelType.FullName} does not accept arguments of type {readModelType.FullName}")
		{
		}
	}
}