using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ReadModelFactory
{
	internal interface IAssemblyTypesProvider
	{
		List<Type> GetTypes(Assembly[] assemblies);
	}

	internal class AssemblyTypesProvider : IAssemblyTypesProvider
	{
		public List<Type> GetTypes(Assembly[] assemblies)
			=> assemblies.SelectMany(x => x.GetTypes()).ToList();
	}
}