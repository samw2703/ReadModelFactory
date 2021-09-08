using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;
using Microsoft.Extensions.DependencyInjection;

namespace ReadModelFactory.Tests
{
	public class ReadModelCatalogueTests
	{
		[Test]
		public async Task GetProvider_NoProviderExistsForReadModelType_ThrowsNoReadModelProvider()
		{
			Assert.ThrowsAsync<NoReadModelProvider>(async () => await GetProvider<object, NoReadModelArgs>());
		}

		[Test]
		public async Task GetProvider_ProviderExistsButDoesNotAcceptSpecifiedArgsType_ThrowsReadModelArgsMismatch()
		{
			Assert.ThrowsAsync<ReadModelArgsMismatch>(async () => await GetProvider<TestReadModel, NoReadModelArgs>());
		}

		[Test]
		public async Task GetProvider_ProviderExistsAndIsOfCorrectType_ReturnsProvider()
		{
			var provider = await GetProvider<TestReadModel, TestArgs>();

			Assert.IsNotNull(provider);
		}

		private async Task<ReadModelProvider<TReadModel, TArgs>> GetProvider<TReadModel, TArgs>()
		{
			return new ReadModelCatalogue(CreateCatalogueItems(), CreateServiceProvider())
				.GetProvider<TReadModel, TArgs>();
		}

		private IServiceProvider CreateServiceProvider()
		{
			var serviceCollection = new ServiceCollection();
			serviceCollection.AddTransient<TestReadModelProvider>();
			return serviceCollection.BuildServiceProvider();
		}

		private List<ReadModelCatalogueItem> CreateCatalogueItems()
		{
			return new()
			{
				new(typeof(TestReadModel), typeof(TestReadModelProvider))
			};
		}

		private class TestArgs
		{
		}

		private class TestReadModel
		{
		}

		private class TestReadModelProvider : ReadModelProvider<TestReadModel, TestArgs>
		{
			public override Task<TestReadModel> Get(TestArgs args)
			{
				throw new NotImplementedException();
			}
		}
	}
}