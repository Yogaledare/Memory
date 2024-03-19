# Övning 4 - Minneshantering

_OBS - Resultatet av övningen skall visas för lärare och godkännas innan den kan anses vara genomförd._

## Instruktioner:

**Samtlig kod för övningarna skrivs i det medföljande projektet ”Ovning4_SkalProj” på klassens
GitHub. I koden finns även kompletterande instruktioner.**
Frågorna som ställs besvaras som kommentarer i koden.
Övning 1-4 är prioriterade. 5-6 är extra om tid finns.

## Teori och fakta

Mycket av minneshanteringen inom .NET sköter sig självt, men som programmerare är det
bra att ha en viss insikt om hur det går till under huven när koden körs. Minnet är då
uppdelat i en **_stack_** (trave/stapel, senare benämnt: **_stacken_** ) och en **_heap_** (en trädstruktur,
senare benämnd: **_heapen_** ), men innan vi går in på vad dessa två delar hanterar, ska vi ta en
titt på hur de fungerar.

Stacken, som namnet föreslår, kan ses som en mängd boxar staplade på varandra. Där vi
använder innehållet i den översta boxen och för att komma åt den undre måste den
ovanstående boxen först lyftas av. För att exemplifiera detta ytterligare kan stacken ses
som skolådor i en skobutik, där du för att komma åt de nedre lådorna måste flytta bort de
övre.

Heapen däremot, är inte lika enkel att förklara i ord. _Heapen_ är en form av struktur där allt
är tillgängligt på en gång med enkel åtkomst. För att även verklighetsförankra denna
datastruktur går det att likna _heapen_ med en hög med ren tvätt som ligger utspridd över
en säng: allt går att nå fort och enkelt bara vi vet vad vi vill ha.

_Stacken_ har koll på vilka _anrop_ och _metoder_ som körs, när metoden är körd kastas den från
_stacken_ och nästa körs. _Stacken_ är alltså självunderhållande och behöver ingen hjälp med
minnet. _Heapen_ däremot som håller stor del av informationen (men inte har någon koll på
exekveringsordning) behöver oroa sig för **_Garbage Collection_**. Alltså: skokartongerna
sköter sig själv medan tvätthögen måste rensas på smutsig tvätt ibland.

Vad skiljer då det som lagras i stacken och heapen? För att förstå det ska vi lära oss mer
om de fyra typerna i C#, **_Value Types_** , **_Reference Types_** , **_Pointers_** och **_Instructions_**.

_Value Types_ är typer från _System.ValueType_ som listas nedan:

_- bool
- byte
- char
- decimal
- double_
    _- enum_
    _- float_
    _- int_
    _- long_
    _- sbyte_
       _- short_
       _- struct_
       _- uint_
       _- ulong_
       _- ushort_


_Reference Types_ är typer som ärver från _System.Object_ (eller är _System.Object.object_ )

_- class
- interface
- object
- delegate
- string_

Nästa typ är pointers. Dessa är inget som vi behöver tänka på utan behandlas av **_Common
Language Runtime (CLR)_**_._ En _pointer_ skiljer sig från _reference types_ , i det avseendet att när
något är av en _reference type_ , så kommer vi åt det via en _pointer_. En _pointer_ är alltså något
som tar plats i minnet och pekar på antingen en annan plats i minnet eller _null_.
_Instructions_ kommer inte gås igenom i denna övning, men ni ska veta att det finns.

Hur vet vi då vad som lagras vart?

En _reference type_ lagras alltid på _heapen_. Medan _Value types_ , lagras där de deklareras.
Alltså kan _value types_ lagras både på _stacken_ eller _heapen_. Följande exempel kan ge mer
klarhet:

Denna metod ( _se bild ovan_ ) kommer allt att köras på stacken. Detta då alla _value types_
deklareras i en metod, som läggs på stacken.


I exemplet till vänster kommer dock MyValue
att ligga på heapen , då den deklarerats i en
klass som är en reference type.


Den huvudsakliga skillnaden mellan dessa två,
är att all information i det första exemplet
kommer att raderas när det är färdigkört då
stacken rensar sig själv, medan MyInt :en från
exempel två kommer fortsätta ta upp plats på
heapen även efter stacken är klar med den. Den
kommer ligga där tills GC (Garbage Collector)
tar hand om den.

Frågor:

1. Hur fungerar _stacken_ och _heapen_? Förklara gärna med exempel eller skiss på dess
    grundläggande funktion


2. Vad är _Value Types_ respektive _Reference Types_ och vad skiljer dem åt?
3. Följande metoder ( _se bild nedan_ ) genererar olika svar. Den första returnerar 3, den
    andra returnerar 4, varför?


## Datastrukturer och minneseffektivitet

För att underlätta minneshantering och skriva minnessnålare funktionalitet är det bra att
ha ett hum om olika datastrukturer och när de kan användas. Detta ska ni nu öva på, dels
på papper och dels genom kod. Kom ihåg att kommentera all kod.

Som nämnt tidigare genomförs dessa övningar i det bifogade skalprojektet och frågorna
besvaras på relevant plats med kommentarer i koden.

_Övning 1: ExamineList()_

En lista är en _abstrakt datastruktur_ som kan implementeras på flera olika sätt. Till skillnad
från **_arrayer_** har _listor_ inte en förutbestämd storlek utan storleken ökar i takt med att
antalet element i listan ökar. **_Listklassen_** har dock en underliggande _array_ som ni nu ska
undersöka. För att se storleken på listans underliggande array används _Capacity-metoden_ i
_Listklassen_.

1. Skriv klart implementationen av _ExamineList-metoden_ så att undersökningen blir
    genomförbar.
2. När ökar listans kapacitet? (Alltså den underliggande arrayens storlek)
3. Med hur mycket ökar kapaciteten?
4. Varför ökar inte listans kapacitet i samma takt som element läggs till?
5. Minskar kapaciteten när element tas bort ur listan?
6. När är det då fördelaktigt att använda en egendefinierad _array_ istället för en lista?

Övning 2: ExamineQueue()

Datastrukturen **_kö_** (implementerad i **_Queue-klassen_** ) fungerar enligt **Först In Först Ut
(** **_FIFO_** **)** principen. Alltså att det element som läggs till först kommer vara det som tas bort
först.

1. Simulera följande kö på papper:
    a. ICA öppnar och kön till kassan är tom
    b. Kalle ställer sig i kön
    c. Greta ställer sig i kön
    d. Kalle blir expedierad och lämnar kön
    e. Stina ställer sig i kön
    f. Greta blir expedierad och lämnar kön
    g. Olle ställer sig i kön
    h. ...
2. Implementera metoden _ExamineQueue_. Metoden ska simulera hur en _kö_ fungerar
    genom att tillåta användaren att ställa element i kön ( **_enqueue_** ) och ta bort element
    ur kön ( **_dequeue_** ). Använd _Queue-klassen_ till hjälp för att implementera metoden.
    Simulera sedan ICA-kön med hjälp av ditt program.

_Övning 3: ExamineStack()_

Stackar påminner om köer, men en stor skillnad är att stackar använder sig av **Först In Sist
Ut (** **_FILO_** ) principen. Alltså gäller att det element som stoppas in först ( **_push_** ) är det som
kommer tas bort sist ( **_pop_** ).


1. Simulera ännu en gång ICA-kön på papper. Denna gång med en _stack_. Varför är det
    inte så smart att använda en _stack_ i det här fallet?
2. Implementera en ReverseText-metod som läser in en sträng från användaren och
    med hjälp av en stack vänder ordning på teckenföljden för att sedan skriva ut den omvända strängen till användaren.

_Övning 4: CheckParenthesis()_

Ni bör nu ha tillräckliga kunskaper om ovannämnda datastrukturer för att lösa följande
problem.

Vi säger att en sträng är _välformad_ om alla parenteser som öppnas även stängs korrekt.
Att en parentes öppnas och stängs korrekt dikteras av följande regler:

- ), }, ] får enbart förekomma efter respektive (, {, [
- Varje parentes som öppnas måste stängas dvs ”(” följs av ”)”

Exempelvis är ([{}]({})) välformad men inte ({)}. Vidare kan en sträng innehålla andra tecken,
t ex är ”List<int> lista = new List<int>(){2, 3, 4};” välformad. Vi bryr oss alltså endast om
parenteser!

1. Skapa med hjälp av er nya kunskap funktionalitet för att kontrollera en välformad
    sträng på papper. Du ska använda dig av någon eller några av de datastrukturer vi
    precis gått igenom. Vilken datastruktur använder du?
2. Implementera funktionaliteten i metoden _CheckParentheses_. Låt programmet läsa
    in en _sträng_ från användaren och returnera ett svar som reflekterar huruvida
    strängen är välformad eller ej.


## Rekursion och Iteration (Extra om tid finns)

För att ta reda på mer om hur viktigt det är att tänka på hur mycket som läggs på _stacken_
finns även detta kapitel om **_rekursion_** och **_iteration_**. För en som inte är insatt kan _rekursion_
och _iteration_ se väldigt lika ut, detta för att en _rekursion_ kan ses som en _iteration_ av sig
själv.

Rekursion är en funktion som anropar sig själv, ned till ett basfall, och därefter gör alla
beräkningar upp till det anrop som initierade rekursionen. Nedan följer ett exempel på hur
en rekursiv metod kan beräkna det n:te udda talet.

Det metoden gör är att se efter om _n_ är _1_ , om så returerar den det första udda talet _1_.
Annars så anropar den sig själv för ett mindre _n_ och lägger till två.

_Övning 5: Rekursion_

1. Illustrera förloppen för RecursiveOdd(1), RecursiveOdd(3) och RecursiveOdd(5) på
    papper för att förstå den rekursiva loopen.
2. Skriv en _RecursiveEven(int n)_ metod som rekursivt beräknar det _n_ :te jämna talet.
3. Implementera en rekursiv funktion för att beräkna tal i _fibonaccisekvensen: (f(n) =_
    _f(n-1) + f(n-2))_



_Övning 6: Iteration_

Nu när ni är bekanta med rekursion är det dags att kolla på iteration. Iteration är en
funktion som upprepar samma sak till dess att målet är uppnått. Så en iterativ funktion för
att göra föregående beräkning om det n:te udda talet skulle se ut:


Denna metod börjar från 1 och adderar 2 till dess att resultat blir det n :te udda talet.

1. Illustrera på papper förloppen för _IterativeOdd(1)_ , _IterativeOdd(3)_ och
    _IterativeOdd(5)_ för att förstå iterationen.
2. Skapa en _IterativeEven(int n)_ funktion för att iterativt beräkna det _n_ :te jämna talet.
3. Implementera en iterativ version av _fibonacciberäknaren_.

Fråga:
Utgå ifrån era nyvunna kunskaper om iteration, rekursion och minneshantering. Vilken av
ovanstående funktioner är mest minnesvänlig och varför?


