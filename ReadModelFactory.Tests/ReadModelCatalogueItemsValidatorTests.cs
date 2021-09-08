using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;

namespace ReadModelFactory.Tests
{
	public class ReadModelCatalogueItemsValidatorTests
	{
		[Test]
		public void Validate_MultipleProvidersExistForReadModelType_ThrowsMultipleProviders()
		{
			Assert.Throws<MultipleProviders>(() => Validate(
				(typeof(MultipleProvidersTestReadModel), typeof(MultipleProvidersTestReadModelProvider1)),
				(typeof(MultipleProvidersTestReadModel), typeof(MultipleProvidersTestReadModelProvider2))
			));
		}

		[Test]
		public void Validate_HappyPath()
		{
			Assert.DoesNotThrow(() => Validate());
		}

		private void Validate(params (Type ReadModelType, Type ProviderType)[] catalogueArray)
		{
			new ReadModelCatalogueItemsValidator().Validate(CreateCatalogueItems(catalogueArray));
		}

		private List<ReadModelCatalogueItem> CreateCatalogueItems((Type ReadModelType, Type ProviderType)[] catalogueArray)
		{
			return catalogueArray
				.Select(x => new ReadModelCatalogueItem(x.ReadModelType, x.ProviderType))
				.ToList();
		}

		private class MultipleProvidersTestReadModel
		{
		}

		private class MultipleProvidersTestReadModelProvider1 : ReadModelProvider<MultipleProvidersTestReadModel>
		{
			public override Task<MultipleProvidersTestReadModel> Get()
			{
				throw new System.NotImplementedException();
			}
		}

		private class MultipleProvidersTestReadModelProvider2 : ReadModelProvider<MultipleProvidersTestReadModel, object>
		{
			public override Task<MultipleProvidersTestReadModel> Get(object args)
			{
				throw new System.NotImplementedException();
			}
		}
	}
}