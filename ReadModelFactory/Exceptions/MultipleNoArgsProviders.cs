using System;

namespace ReadModelFactory
{
	internal class MultipleNoArgsProviders : Exception
	{
		public MultipleNoArgsProviders(Type readModelType)
			: base($"Multiple no args providers exist for read model {readModelType.FullName}")
		{
		}
	}
}