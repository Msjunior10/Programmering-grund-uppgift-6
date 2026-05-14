# NameSorter - kodgranskning och optimering

Det har projektet bygger pa ett enkelt C#-program som arbetar med en lista av namn. Grundversionen kunde skriva ut namn, sortera dem och lata anvandaren soka efter ett namn. I den har versionen har programmet granskats och forbedrats for att bli mer strukturerat, mer anvandarvanligt och mer effektivt.

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
git checkout master
git merge feature_utokade_funktioner
git log --oneline --graph
```

## Bygg och kor

```powershell
dotnet build
dotnet run
```