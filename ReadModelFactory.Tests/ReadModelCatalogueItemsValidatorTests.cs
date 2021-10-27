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
		public void Validate_MultipleProvidersExistForReadModelArgs_ThrowsMultipleProvidersForArgs()
		{
			Assert.Throws<MultipleProvidersForArgs>(() => Validate(
				(typeof(MultipleProvidersTestReadModelArgs), typeof(MultipleProvidersTestReadModel), typeof(MultipleProvidersTestReadModelProvider2)),
				(typeof(MultipleProvidersTestReadModelArgs), typeof(object), typeof(MultipleProvidersTestReadModelProvider3))
			));
		}

		[Test]
		public void Validate_MultipleNoArgsProvidersExistForNoArgs_ThrowsMultipleNoArgsProviders()
		{
			Assert.Throws<MultipleNoArgsProviders>(() => Validate(
				(typeof(NoReadModelArgs), typeof(NoArgsReadModel), typeof(NoArgsReadModelProvider1)),
				(typeof(NoReadModelArgs), typeof(NoArgsReadModel), typeof(NoArgsReadModelProvider2))
			));
		}

		[Test]
		public void Validate_MultipleProvidersExistForReadModelTypeWithDifferentArgs_DoesNotThrow()
		{
			Assert.DoesNotThrow(() => Validate(
				(typeof(object), typeof(MultipleProvidersTestReadModel), typeof(MultipleProvidersTestReadModelProvider3)),
				(typeof(MultipleProvidersTestReadModelArgs), typeof(MultipleProvidersTestReadModel), typeof(MultipleProvidersTestReadModelProvider2))
			));
		}

		[Test]
		public void Validate_HappyPath()
		{
			Assert.DoesNotThrow(() => Validate());
		}

		private void Validate(params (Type argsTypes, Type ReadModelType, Type ProviderType)[] catalogueArray)
		{
			new ReadModelCatalogueItemsValidator().Validate(CreateCatalogueItems(catalogueArray));
		}

		private List<ReadModelCatalogueItem> CreateCatalogueItems((Type argsTypes, Type ReadModelType, Type ProviderType)[] catalogueArray)
		{
			return catalogueArray
				.Select(x => new ReadModelCatalogueItem(x.argsTypes, x.ReadModelType, x.ProviderType))
				.ToList();
		}

		private class MultipleProvidersTestReadModel
		{
		}

		private class MultipleProvidersTestReadModelArgs
		{
		}

		private class MultipleProvidersTestReadModelProvider1 : ReadModelProvider<MultipleProvidersTestReadModel>
		{
			public override Task<MultipleProvidersTestReadModel> Get()
			{
				throw new System.NotImplementedException();
			}
		}

		private class MultipleProvidersTestReadModelProvider2 : ReadModelProvider<MultipleProvidersTestReadModel, MultipleProvidersTestReadModelArgs>
		{
			public override Task<MultipleProvidersTestReadModel> Get(MultipleProvidersTestReadModelArgs args)
			{
				throw new NotImplementedException();
			}
		}

		private class MultipleProvidersTestReadModelProvider3 : ReadModelProvider<object, MultipleProvidersTestReadModelArgs>
		{
			public override Task<object> Get(MultipleProvidersTestReadModelArgs args)
			{
				throw new NotImplementedException();
			}
		}

		private class NoArgsReadModel
		{
		}

		private class NoArgsReadModelProvider1 : ReadModelProvider<NoArgsReadModel>
		{
			public override Task<NoArgsReadModel> Get()
			{
				throw new NotImplementedException();
			}
		}

		private class NoArgsReadModelProvider2 : ReadModelProvider<NoArgsReadModel>
		{
			public override Task<NoArgsReadModel> Get()
			{
				throw new NotImplementedException();
			}
		}
	}
}