using System.Threading.Tasks;

namespace ReadModelFactory
{
	public interface IReadModelFactory
	{
		Task<TReadModel> Get<TReadModel, TArgs>(TArgs args);
		Task<TReadModel> Get<TReadModel>();
	}
}