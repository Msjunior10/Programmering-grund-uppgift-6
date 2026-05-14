using System.Globalization;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("NameSorter.Tests")]

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
			MenuUi.PrintMenu();
			MenuAction action = MenuUi.ReadMenuAction();

			switch (action)
			{
				case MenuAction.Search:
					SearchName(catalog);
					break;
				case MenuAction.Add:
					AddName(catalog);
					break;
				case MenuAction.ShowSorted:
					PrintHeader("Sorterad lista (svensk kultur)");
					PrintNames(catalog.Names);
					break;
				case MenuAction.Exit:
					return;
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

internal enum MenuAction
{
	Search = 1,
	Add = 2,
	ShowSorted = 3,
	Exit = 4
}

internal static class MenuUi
{
	internal static void PrintMenu()
	{
		Console.WriteLine();
		Console.WriteLine("Val:");
		Console.WriteLine("1. Sok efter namn");
		Console.WriteLine("2. Lagg till namn");
		Console.WriteLine("3. Visa sorterad lista");
		Console.WriteLine("4. Avsluta");
	}

	internal static MenuAction ReadMenuAction()
	{
		while (true)
		{
			Console.Write("Ange val: ");
			string? input = Console.ReadLine();

			if (TryParseMenuAction(input, out MenuAction action))
			{
				return action;
			}

			Console.WriteLine("Ogiltigt val. Forsok igen med 1, 2, 3 eller 4.");
		}
	}

	internal static bool TryParseMenuAction(string? input, out MenuAction action)
	{
		if (int.TryParse(input, out int numericChoice) && Enum.IsDefined(typeof(MenuAction), numericChoice))
		{
			action = (MenuAction)numericChoice;
			return true;
		}

		action = default;
		return false;
	}
}

internal sealed class NameCatalog
{
	private readonly CultureInfo culture;
	private readonly List<string> names;
	private readonly HashSet<string> nameIndex;
	private readonly CompareInfo compareInfo;

	internal NameCatalog(IEnumerable<string> initialNames, CultureInfo culture)
	{
		this.culture = culture;
		StringComparer searchComparer = StringComparer.Create(culture, ignoreCase: true);

		// Ett hash-index ger snabbare sokningar an List.Contains nar datamangden vaxer.
		nameIndex = new HashSet<string>(searchComparer);
		names = new List<string>();

		// Kulturstyrd sortering gor att svenska bokstaver hamnar i den ordning anvandaren forvantar sig.
		compareInfo = culture.CompareInfo;

		foreach (string name in initialNames)
		{
			TryAdd(name, out _);
		}
	}

	internal IReadOnlyList<string> Names => names;

	internal bool Contains(string name)
	{
		return nameIndex.Contains(NormalizeName(name));
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

		// Normalisering gor listan mer konsekvent oavsett hur anvandaren skriver namnet.
		string normalizedName = NormalizeName(name);

		if (!nameIndex.Add(normalizedName))
		{
			message = $"{normalizedName} finns redan i listan.";
			return false;
		}

		names.Add(normalizedName);
		message = $"{normalizedName} har lagts till i listan.";
		return true;
	}

	private string NormalizeName(string name)
	{
		// Normalisering minskar risken for dubbletter som bara skiljer sig i mellanslag eller versaler.
		return culture.TextInfo.ToTitleCase(name.Trim().ToLower(culture));
	}
}
