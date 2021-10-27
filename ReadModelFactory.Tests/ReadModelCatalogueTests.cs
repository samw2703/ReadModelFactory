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
		public async Task GetProvider_NoProviderExistsForReadModelArgsTypeCombo_ThrowsNoReadModelProvider()
		{
			Assert.ThrowsAsync<NoReadModelProvider>(async () => await GetProvider<object, NoReadModelArgs>());
		}

		[Test]
		public async Task GetProvider_ProviderExistsForReadModelArgsTypeCombo_ReturnsProvider()
		{
			var provider = await GetProvider<TestReadModel, TestArgs>();

			Assert.IsNotNull(provider);
		}

		private async Task<ReadModelProvider<TReadModel, TArgs>> GetProvider<TReadModel, TArgs>()
		{
			return new ReadModelCatalogue(CreateCatalogueItems())
				.GetProvider<TReadModel, TArgs>(CreateServiceProvider());
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
				new(typeof(TestArgs), typeof(TestReadModel), typeof(TestReadModelProvider))
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