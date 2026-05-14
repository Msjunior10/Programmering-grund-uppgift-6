using System.Globalization;
using NameSorter;
using Xunit;

namespace NameSorter.Tests;

public sealed class NameCatalogTests
{
	private static readonly CultureInfo SwedishCulture = new("sv-SE");

	[Fact]
	public void Contains_IgnoresCaseAndWhitespaceAfterNormalization()
	{
		NameCatalog catalog = new(new[] { "Anna" }, SwedishCulture);

		bool result = catalog.Contains("  anna ");

		Assert.True(result);
	}

	[Fact]
	public void TryAdd_RejectsDuplicateNameAfterNormalization()
	{
		NameCatalog catalog = new(new[] { "Anna" }, SwedishCulture);

		bool result = catalog.TryAdd(" anna ", out string message);

		Assert.False(result);
		Assert.Equal("Anna finns redan i listan.", message);
	}

	[Fact]
	public void Sort_UsesSwedishCultureOrder()
	{
		NameCatalog catalog = new(new[] { "Östen", "Zara", "Åke", "Anna" }, SwedishCulture);

		catalog.Sort();

		Assert.Equal(new[] { "Anna", "Zara", "Åke", "Östen" }, catalog.Names);
	}
}