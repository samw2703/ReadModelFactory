using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ReadModelFactory
{
	internal class ReadModelCatalogueItemsProvider
	{
		private readonly IAssemblyTypesProvider _assemblyTypesProvider;

		public ReadModelCatalogueItemsProvider(IAssemblyTypesProvider assemblyTypesProvider)
		{
			_assemblyTypesProvider = assemblyTypesProvider;
		}

		public List<ReadModelCatalogueItem> Get(Assembly[] assemblies)
		{
			var items = _assemblyTypesProvider
				.GetTypes(assemblies)
				.Where(InheritsFromReadModelProvider)
				.Select(ToCatalogueItem)
				.ToList();
			new ReadModelCatalogueItemsValidator().Validate(items);

			return items;
		}

		private ReadModelCatalogueItem ToCatalogueItem(Type type)
		{
			var readModelProviderType = GetInheritanceTree(type).Single(IsReadModelProvider);

			return new ReadModelCatalogueItem(readModelProviderType.GetGenericArguments().First(), type);
		}

		private bool InheritsFromReadModelProvider(Type type)
			=> GetInheritanceTree(type).Any(IsReadModelProvider);

		private bool IsReadModelProvider(Type type)
		{
			if (!type.IsGenericType)
				return false;

			return type.GetGenericTypeDefinition() == typeof(ReadModelProvider<,>).GetGenericTypeDefinition();
		}

		private List<Type> GetInheritanceTree(Type type)
		{
			var inheritanceTree = new List<Type>();
			var baseType = type;
			while (true)
			{
				if (baseType == typeof(object))
					break;

				inheritanceTree.Add(baseType);

				baseType = baseType.BaseType;
			}

			return inheritanceTree;
		}
	}
}