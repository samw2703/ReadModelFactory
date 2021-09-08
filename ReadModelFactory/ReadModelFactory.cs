using System;
using System.Threading.Tasks;

namespace ReadModelFactory
{
	internal class ReadModelFactory : IReadModelFactory
	{
		private readonly ReadModelCatalogue _readModelCatalogue;
		private readonly IServiceProvider _serviceProvider;

		public ReadModelFactory(ReadModelCatalogue readModelCatalogue, IServiceProvider serviceProvider)
		{
			_readModelCatalogue = readModelCatalogue;
			_serviceProvider = serviceProvider;
		}

		public async Task<TReadModel> Get<TReadModel, TArgs>(TArgs args)
		{
			return await _readModelCatalogue
				.GetProvider<TReadModel, TArgs>(_serviceProvider)
				.Get(args);
		}

		public async Task<TReadModel> Get<TReadModel>()
			=> await Get<TReadModel, NoReadModelArgs>(new NoReadModelArgs());
	}

	public class NoReadModelArgs
	{
	}
}