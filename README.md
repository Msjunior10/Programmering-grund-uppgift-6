# NameSorter - kodgranskning och optimering

Det har projektet ar en C#-konsolapplikation som utgar fran en enkel namnlista och utvecklas vidare genom kodgranskning, optimering och Git-dokumentation.

Programmet kan:

1. visa en lista med namn
2. sortera namn enligt svenska sorteringsregler
3. soka efter namn pa ett effektivare satt
4. lagga till nya namn med validerad inmatning

Syftet med projektet ar att visa hur ett enkelt program kan forbattras nar man arbetar med struktur, prestanda, anvandarupplevelse och versionshantering.

## Vad som var svagt i originalkoden

Nar jag granskade originalkoden hittade jag flera delar som kunde forbattras:

1. Sokningen anvande `List.Contains`, vilket innebar en linjar sokning som blir mindre effektiv nar datan vaxer.
2. Sorteringen tog inte tydlig hansyn till svensk kultur, vilket kunde ge mindre korrekt ordning for namn med `Å`, `Ä` och `Ö`.
3. All logik lag samlad i `Main`, vilket gjorde koden svarare att lasa och bygga ut.
4. Programmet saknade tydlig felhantering for tom inmatning och ogiltiga val.
5. Anvandarupplevelsen var begransad eftersom programmet inte hade nagon tydlig meny eller fortsatt arbetsflode.

## Forbattringar som genomfordes

For att hoja kvaliteten i programmet gjorde jag flera forbattringar:

1. Sokningen effektiviserades med `HashSet<string>` for snabbare uppslagning.
2. Sorteringen anpassades till svensk kultur med `CultureInfo("sv-SE")` och `CompareInfo`.
3. Namnlogiken flyttades till klassen `NameCatalog` for att ge tydligare struktur.
4. Programmet fick en enkel meny dar anvandaren kan soka, lagga till namn, visa listan eller avsluta.
5. Inmatning valideras sa att tomma svar och ogiltiga val hanteras tydligare.
6. Namn normaliseras innan de sparas eller soks efter, vilket ger ett mer konsekvent resultat.

## Resultat

Efter andringarna ar programmet mer stabilt och enklare att anvanda. Koden ar ocksa mer uppdelad och lattare att vidareutveckla. Den storsta tekniska skillnaden ar att sokningen nu ar mer effektiv, samtidigt som sorteringen fungerar battre for svenska namn.

## Verifiering

For att gora losningen mer trovärdig finns nu enkla automatiserade tester for de viktigaste forbattringarna:

1. att sokningen fungerar trots blandning av versaler, gemener och extra mellanslag
2. att dubbletter stoppas efter normalisering
3. att sorteringen foljer svensk kulturordning
4. att menyval bara accepterar giltiga alternativ

Kor verifieringen med:

```powershell
dotnet test NameSorter.Tests/NameSorter.Tests.csproj
```

## Git-flode

Arbetet dokumenterades med Git enligt uppgiftens krav.

```powershell
git init
git add .
git commit -m "Initial commit: skapade NameSorter-projekt"
git branch feature_utokade_funktioner
git checkout feature_utokade_funktioner
git add .
git commit -m "Forbattra namnnormalisering och anvandarupplevelse"
git checkout main
git merge feature_utokade_funktioner
git log --oneline --graph
```

## Bygg och kor

```powershell
dotnet build
dotnet run
```