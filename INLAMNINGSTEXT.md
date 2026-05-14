# Inlamningstext - Avancerad Kodgranskning och Optimering

I den har uppgiften fick jag arbeta med att granska och forbattra ett befintligt C#-program. Originalprogrammet var ganska enkelt och kunde skriva ut en lista med namn, sortera den och sedan lata anvandaren soka efter ett namn. Programmet fungerade, men nar jag tittade pa koden mer noggrant sag jag ganska snabbt att det fanns flera delar som kunde bli battre.

Det jag framfor allt upptackte var att sokningen inte var sa effektiv som den kunde vara. I originalkoden anvandes `List.Contains`, vilket betyder att programmet maste ga igenom listan steg for steg for att hitta ett namn. Det fungerar om listan ar liten, men det ar inte en bra losning om programmet skulle hantera fler namn i framtiden. Jag sag ocksa att sorteringen inte var anpassad efter svenska bokstaver, vilket kan bli fel nar namn innehaller `Å`, `Ä` eller `Ö`.

En annan sak jag reagerade pa var att all logik lag direkt i `Main`. Det gjorde koden ganska rorig och svarare att vidareutveckla. Dessutom saknades det bra felhantering om anvandaren skrev in ett tomt namn eller valde fel alternativ i programmet.

For att forbattra programmet gjorde jag flera andringar. Jag valde bland annat att anvanda en `HashSet<string>` for sokningen, eftersom det ar ett snabbare satt att kontrollera om ett namn redan finns i samlingen. Samtidigt beholl jag en lista for att kunna skriva ut och sortera namnen pa ett smidigt satt.

Jag andrade ocksa sorteringen sa att den fungerar battre i en svensk kontext. Genom att anvanda `CultureInfo("sv-SE")` och `CompareInfo` blir ordningen mer korrekt for svenska namn. Det ar en liten detalj i koden, men det gor stor skillnad for hur programmet faktiskt beter sig.

For att fa battre struktur flyttade jag namnlogiken till en separat klass som heter `NameCatalog`. Pa sa satt blev huvudprogrammet renare, och ansvaret delades upp tydligare. `Program` hanterar nu anvandaren och menyn, medan `NameCatalog` skoter lagring, sokning och sortering av namn.

Jag forbattrade ocksa anvandarupplevelsen genom att lagga till en enkel meny. Istallet for att bara gora en sokning och sedan avsluta programmet kan anvandaren nu soka efter namn, lagga till nya namn, visa listan eller avsluta sjalv. Jag lade ocksa till kontroll av inmatning sa att programmet hanterar tomma svar och ogiltiga menyval pa ett tydligt satt.

En sista viktig forbattring var att normalisera namnen innan de sparas eller soks efter. Det betyder att om anvandaren till exempel skriver extra mellanslag eller blandar stora och sma bokstaver sa behandlas namnet anda pa ett konsekvent satt. Det gor att programmet blir mer stabilt och att resultatet ser renare ut.

Nar jag var klar med andringarna tyckte jag att programmet kandes betydligt mer genomarbetat an originalversionen. Det blev enklare att anvanda, enklare att lasa och mer effektivt bakom kulisserna.

Jag dokumenterade ocksa arbetet med Git. Jag skapade ett eget repository for projektet, gjorde en forsta commit och arbetade sedan vidare i branchen `feature_utokade_funktioner`. Nar forbattringarna var klara mergades den branchen tillbaka till `master`. Pa det sattet blev hela processen tydlig och latt att folja.

Sammanfattningsvis tycker jag att uppgiften var bra eftersom den visade att kod inte bara ska fungera, utan ocksa vara tydlig, effektiv och latt att utveckla vidare. Genom att forst granska koden och sedan forbattrade den fick jag en battre forstaelse for hur sma designval kan paverka hela programmet.