using System.Threading.Tasks;

namespace ReadModelFactory
{
	internal class ReadModelFactory : IReadModelFactory
	{
		private readonly ReadModelCatalogue _readModelCatalogue;

		public ReadModelFactory(ReadModelCatalogue readModelCatalogue)
		{
			_readModelCatalogue = readModelCatalogue;
		}

		public async Task<TReadModel> Get<TReadModel, TArgs>(TArgs args)
		{
			return await _readModelCatalogue
				.GetProvider<TReadModel, TArgs>()
				.Get(args);
		}

		public async Task<TReadModel> Get<TReadModel>()
			=> await Get<TReadModel, NoReadModelArgs>(new NoReadModelArgs());
	}

	public class NoReadModelArgs
	{
	}
}