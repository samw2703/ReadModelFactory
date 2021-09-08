using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace ReadModelFactory
{
	public static class ServiceCollectionExtensions
	{
		public static void AddReadModelFactory(this IServiceCollection serviceCollection, params Assembly[] assemblies)
		{
			serviceCollection.AddSingleton<IReadModelFactory, ReadModelFactory>();
			AddReadModelCatalogue(serviceCollection, assemblies);
		}

		private static void AddReadModelCatalogue(IServiceCollection serviceCollection, Assembly[] assemblies)
		{
			var catalogueItems = new ReadModelCatalogueItemsProvider(new AssemblyTypesProvider())
				.Get(assemblies);
			foreach (var catalogueItem in catalogueItems)
				serviceCollection.AddTransient(catalogueItem.ProviderType);

			serviceCollection.AddSingleton(new ReadModelCatalogue(catalogueItems));
		}
	}
}