using Microsoft.Extensions.DependencyInjection;

namespace ReadModelFactory
{
	public static class ServiceCollectionExtensions
	{
		public static void AddReadModelFactory(this IServiceCollection serviceCollection)
		{
			serviceCollection.AddSingleton<IReadModelFactory>()
		}
	}
}