using System.Globalization;

namespace NameSorter;

internal static class Program
{
	private static readonly CultureInfo SwedishCulture = new("sv-SE");

	private static void Main()
	{
		NameCatalog catalog = new(
			new[] { "Anna", "John", "Alice", "Bob", "Zara", "Åke", "Östen" },
			SwedishCulture);

		PrintHeader("Original lista");
		PrintNames(catalog.Names);

		catalog.Sort();

		PrintHeader("Sorterad lista (svensk kultur)");
		PrintNames(catalog.Names);

		RunMenu(catalog);
	}

	private static void RunMenu(NameCatalog catalog)
	{
		while (true)
		{
			Console.WriteLine();
			Console.WriteLine("Val:");
			Console.WriteLine("1. Sok efter namn");
			Console.WriteLine("2. Lagg till namn");
			Console.WriteLine("3. Visa sorterad lista");
			Console.WriteLine("4. Avsluta");
			Console.Write("Ange val: ");

			string? choice = Console.ReadLine();

			switch (choice)
			{
				case "1":
					SearchName(catalog);
					break;
				case "2":
					AddName(catalog);
					break;
				case "3":
					PrintHeader("Sorterad lista (svensk kultur)");
					PrintNames(catalog.Names);
					break;
				case "4":
					return;
				default:
					Console.WriteLine("Ogiltigt val. Forsok igen med 1, 2, 3 eller 4.");
					break;
			}
		}
	}

	private static void SearchName(NameCatalog catalog)
	{
		Console.Write("Ange namn att soka efter: ");
		string? input = Console.ReadLine();

		if (string.IsNullOrWhiteSpace(input))
		{
			Console.WriteLine("Du maste skriva ett namn for att kunna soka.");
			return;
		}

		string trimmedName = input.Trim();

		if (catalog.Contains(trimmedName))
		{
			Console.WriteLine($"{trimmedName} finns i listan.");
			return;
		}

		Console.WriteLine($"{trimmedName} finns inte i listan.");
	}

	private static void AddName(NameCatalog catalog)
	{
		Console.Write("Ange namn att lagga till: ");
		string? input = Console.ReadLine();

		if (catalog.TryAdd(input, out string message))
		{
			catalog.Sort();
		}

		Console.WriteLine(message);
	}

	private static void PrintHeader(string title)
	{
		Console.WriteLine();
		Console.WriteLine(title);
		Console.WriteLine(new string('-', title.Length));
	}

	private static void PrintNames(IEnumerable<string> names)
	{
		foreach (string name in names)
		{
			Console.WriteLine(name);
		}
	}
}

internal sealed class NameCatalog
{
	private readonly List<string> names;
	private readonly HashSet<string> nameIndex;
	private readonly CompareInfo compareInfo;

	internal NameCatalog(IEnumerable<string> initialNames, CultureInfo culture)
	{
		StringComparer searchComparer = StringComparer.Create(culture, ignoreCase: true);

		// Ett hash-index ger snabbare sokningar nar listan blir stor.
		nameIndex = new HashSet<string>(searchComparer);
		names = new List<string>();

		// Kulturstyrd sortering gor att svenska bokstaver hamnar i forvantad ordning.
		compareInfo = culture.CompareInfo;

		foreach (string name in initialNames)
		{
			TryAdd(name, out _);
		}
	}

	internal IReadOnlyList<string> Names => names;

	internal bool Contains(string name)
	{
		return nameIndex.Contains(name);
	}

	internal void Sort()
	{
		names.Sort((left, right) => compareInfo.Compare(left, right, CompareOptions.IgnoreCase));
	}

	internal bool TryAdd(string? name, out string message)
	{
		if (string.IsNullOrWhiteSpace(name))
		{
			message = "Tom inmatning ar inte tillaten.";
			return false;
		}

		string trimmedName = name.Trim();

		if (!nameIndex.Add(trimmedName))
		{
			message = $"{trimmedName} finns redan i listan.";
			return false;
		}

		names.Add(trimmedName);
		message = $"{trimmedName} har lagts till i listan.";
		return true;
	}
}
