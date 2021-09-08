using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace ReadModelFactory.Tests
{
	public class ReadModelFactoryTests
	{
		private IServiceProvider _serviceProvider;

		[SetUp]
		public void Setup()
		{
			_serviceProvider = CreateServiceProvider();
		}

		[Test]
		public async Task Get_CommandCatalogueContainsProviderForRequestedReadModelType_RunsReadModelProviderAndGetsReadModel()
		{
			var readModel = await Get<TestReadModel, TestReadModelArgs>(new TestReadModelArgs());

			Assert.IsNotNull(readModel);
			Assert.True(_serviceProvider.GetService<TestReadModelProvider>().Ran);
		}

		private async Task<TReadModel> Get<TReadModel, TReadModelArgs>(TReadModelArgs readModelArgs)
		{
			return await new ReadModelFactory(CreateCatalogue())
				.Get<TReadModel, TReadModelArgs>(readModelArgs);
		}

		private ReadModelCatalogue CreateCatalogue()
		{
			var catalogueItems = new List<ReadModelCatalogueItem>
			{
				new ReadModelCatalogueItem(typeof(TestReadModel), typeof(TestReadModelProvider))
			};
			return new ReadModelCatalogue(catalogueItems, _serviceProvider);
		}

		private IServiceProvider CreateServiceProvider()
		{
			var serviceCollection = new ServiceCollection();
			serviceCollection.AddSingleton<TestReadModelProvider>();
			return serviceCollection.BuildServiceProvider();
		}

		private class TestReadModelArgs
		{
		}

		private class TestReadModel
		{
		}

		private class TestReadModelProvider : ReadModelProvider<TestReadModel, TestReadModelArgs>
		{
			public bool Ran { get; private set; }

			public override Task<TestReadModel> Get(TestReadModelArgs args)
			{
				Ran = true;
				return Task.FromResult(new TestReadModel());
			}
		}
	}
}