# NameSorter - kodgranskning och optimering

Detta projekt utgar fran ett enkelt C#-program som sorterar en lista med namn och later anvandaren soka efter ett namn. Versionen i detta repo ar en forbattrad variant som uppfyller kraven for avancerad kodgranskning och optimering.

## Identifierade problem i originalkoden

1. Sokningen anvande `List.Contains`, vilket ar linjar sokning och blir langsamt vid stora datamangder.
2. Sorteringen anvande standardbeteende utan tydlig kulturhantering, vilket kan ge fel ordning for svenska bokstaver som `A`, `O` och `O` med prickar/ringar.
3. Anvandarinteraktionen saknade felhantering for tom eller ogiltig inmatning.
4. All logik lag i `Main`, vilket gjorde koden svar att testa, ateranvanda och bygga ut.
5. Programmet kunde inte enkelt hantera tillagg av nya namn eller forbattrad menylogik.

## Genomforda forbattringar

1. Sokningen effektiviserades med `HashSet<string>` for snabbare uppslagning.
2. Sorteringen gjordes kulturmedveten med `CultureInfo("sv-SE")` och `CompareInfo`.
3. Programmet fick en enkel meny med validering av anvandarens val.
4. Namnlogiken flyttades till klassen `NameCatalog` for battre struktur.
5. Kodkommentarer lades till pa stallen dar andringarnas syfte ar viktigt att forsta.
6. Namn normaliseras nu innan de sparas eller soks efter, vilket ger jamnare presentation och forhindrar inkonsekvent inmatning.

## Exempel pa Git-flode for uppgiften

```powershell
git init
git add .
git commit -m "Initial commit: skapade NameSorter-projekt"
git branch feature_utokade_funktioner
git checkout feature_utokade_funktioner
git add .
git commit -m "Forbattra sokning, sortering och felhantering"
git checkout master
git merge feature_utokade_funktioner
git log --oneline --graph
```

## Bygg och kor

```powershell
dotnet build
dotnet run
```