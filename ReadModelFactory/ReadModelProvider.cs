using System.Threading.Tasks;

namespace ReadModelFactory
{
	public abstract class ReadModelProvider<TReadModel, TArgs>
	{
		public abstract Task<TReadModel> Get(TArgs args);
	}

	public abstract class ReadModelProvider<TReadModel> : ReadModelProvider<TReadModel, NoReadModelArgs>
	{
		public override async Task<TReadModel> Get(NoReadModelArgs args)
			=> await Get();

		public abstract Task<TReadModel> Get();
	}
}