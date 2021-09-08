using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;

namespace ReadModelFactory.Tests
{
	public class ReadModelCatalogueItemsProviderTests
	{
		[Test]
		public void Get_BuildsCorrectListOfCatalogueItems()
		{
			var catalogueItems = Get();

			Assert.AreEqual(2, catalogueItems.Count);
			Assert.AreEqual(typeof(NoArgsTestReadModelProvider), catalogueItems.Single(x => x.ReadModelType == typeof(NoArgsTestReadModel)).ProviderType);
			Assert.AreEqual(typeof(TestReadModelProvider), catalogueItems.Single(x => x.ReadModelType == typeof(TestReadModel)).ProviderType);
		}

		private List<ReadModelCatalogueItem> Get()
		{
			return new ReadModelCatalogueItemsProvider(MockAssemblyTypesProvider())
				.Get(null);
		}

		private IAssemblyTypesProvider MockAssemblyTypesProvider()
		{
			var mock = new Mock<IAssemblyTypesProvider>();
			var types = new List<Type>
			{
				typeof(object),
				typeof(string),
				GetType(),
				typeof(NoArgsTestReadModelProvider),
				typeof(TestReadModelProvider),
			};
			mock
				.Setup(x => x.GetTypes(It.IsAny<Assembly[]>()))
				.Returns(types);

			return mock.Object;
		}

		private class NoArgsTestReadModel
		{
		}

		private class NoArgsTestReadModelProvider : ReadModelProvider<NoArgsTestReadModel>
		{
			public override Task<NoArgsTestReadModel> Get()
			{
				throw new NotImplementedException();
			}
		}

		private class TestReadModel
		{
		}

		private class TestReadModelArgs
		{
		}

		private class TestReadModelProvider : ReadModelProvider<TestReadModel, TestReadModelArgs>
		{
			public override Task<TestReadModel> Get(TestReadModelArgs args)
			{
				throw new NotImplementedException();
			}
		}
	}
}