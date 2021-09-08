using System.Threading.Tasks;

namespace ReadModelFactory
{
	internal class ReadModelFactory : IReadModelFactory
	{
		public Task<TReadModel> Get<TReadModel, TArgs>(TArgs args)
		{
			throw new System.NotImplementedException();
		}

		public async Task<TReadModel> Get<TReadModel>()
			=> await Get<TReadModel, NoReadModelArgs>(new NoReadModelArgs());
	}

	internal class NoReadModelArgs
	{
	}
}