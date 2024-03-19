namespace Memory {
    class Program {
        static void Main() {
            Controller.MainMenu();
        }
    }
}


// Frågor: 
// 1. Hur fungerar stacken och heapen? Förklara gärna med exempel eller skiss på dess grundläggande funktion 
// 2. Vad är Value Types respektive Reference Types och vad skiljer dem åt? 
// (sammansatt svar)

// (Call) stacken är en stack - datastruktur som används för att hantera information kring subrutiner / metoder och
// deras scope. Vid varje metodanrop allokeras ett avsnitt av stacken kallad frame för att lagra metodens parametrar
// och lokala variabler samt adressen till vart metodens return value ska skickas.Pekare används för att indikera
// var framen börjar och stacken slutar, och alla variabler lagras med kända offset mot dessa pekare vilket tillåter
// random access till hela scopet trots att vi använder en stack. Om en ny metod anropas inifrån en metod skapas en ny
// frame på stacken ovanför den ursprungliga metodens frame. Då flyttas även pekarna till att istället peka på
// denna frame(nytt scope).Endast en metod(den översta) är aktiv åt gången.En metods scope stängs och framens
// utrymme frigörs när metoden returnerar ett värde. För att kunna återvända till den gamla metoden på rätt ställe
// efter att den nya metoden exekverat klart och returnerat ett värde innehåller varje frame även adresser till
// vart stackpekarna pekade innan de flyttades till den nya metoden. 

// För att systemet med pekare och offset ska fungera måste data som lagras på stacken ha känd och konstant
// storlek vid compile time, vilket betyder endast värdetyper och adresser.För att lagra objekt av
// mer komplexa typer(referenstyper) används heapen, som är en annan del av minnet.Vanligt är att heapen startar
// på låga minnesadresser och stacken på höga, och att de båda byggs på inåt. Hos en referenstyp håller en variabel en
// referens till ett objekt istället för att hålla det faktiska värdet(som hos värdetyper).
// Objektet kan i sin tur hålla referenser till ytterligare objekt och kan dessutom ändra vilka objekt sina
// referenspekare pekar på vid runtime. Detta ger referenstyper ett mer flexibelt uppträdande där de kan ändra
// storlek och betydelse, trots att varje enskilt objekt egentligen har en fast storlek då själva dess pekare är lika
// stora oavsett om de pekar på små eller stora objekt(eller null). Objekt av referenstyper lever endast så länge som
// adressen till objektet är känd. Till skillnad från stacken som självsanerar sig på avslutade metoder försvinner
// dock inte referenstypers objekt av sig själva när dess pekare dör, utan måste rensas upp av en
// garbage collector(Java, C#) eller manuellt(C, C++) för att frigöra minnet som användes för att lagra det.


// 3. Följande metoder (se bild nedan) genererar olika svar. Den första returnerar 3, den andra returnerar 4, varför?
    
// I metod 1 är x och y värdetyper, medan i metod 2 är de referenstyper.När vi sätter y = x i metod 2 så pekar både x och
// y på samma objekt. Om vi ändrar objektet genom den ena variabeln kommer även den andra variabeln peka på
// det uppdaterade objektet. I metod 1 gör y = x att en kopia av x's värde läggs i y. Att ändra y's värde i det fallet
// påverkar inte då x's värde. 



// Övning 1: ExamineList() 

// 1. Skriv klart implementationen av ExamineList-metoden så att undersökningen blir genomförbar.

// Svar: Se implementering. 


// 2. När ökar listans kapacitet? (Alltså den underliggande arrayens storlek) 

// Svar: När den underliggande arrayen är full och man försöker lägga till ytterligare ett element. 


// 3. Med hur mycket ökar kapaciteten?

// Svar: Den dubblas vid varje ökning (men börjar med 0 -> 4). 


// 4. Varför ökar inte listans kapacitet i samma takt som element läggs till?

// Svar:
// Strategin att alltid dubbla storleken vid kapacitetsökning leder till lägre tidskomplexitet för att skala upp listan än
// vid linjär ökning(konstant antal element).
//
// Vi kan jämföra hur många flyttningar / kopieringar A vi behöver göra för att skala upp en lista till storlek n:

// Dubbling: 
//
// A = 1 + 2 + 4 + ... + 2^(k-1)
//
// , där k är antalet dubblingar som behövs för att n <= 2 ^ k.Vi antar att sekvensen startar med 1-> 2-> 4 istället
// för 0-> 4 för att kunna förenkla uttrycket som en geometrisk summa: 
//
// A = (2^(k) - 1)/(2 - 1) = 2^(k) - 1 < 2n - 1 = O(n)
//
// Linjär ökning: 
//
// A = C + 2C + 3C + ... + (n/C - 1)C
//
// , där C är den konstanta ökningen och n / C är så många ökningar som behövs för att nå storlek n.Bryt ut C och skriv
// om som aritmetisk summa: 
//
// A = C(1 + 2 + 3 + ... + (n/C - 1)) = C(n/C - 1)(n/C)/2 = n(n - C)/2 = O(n^2)


// 5. Minskar kapaciteten när element tas bort ur listan? 
//
// Svar: Nej.


// 6. När är det då fördelaktigt att använda en egendefinierad array istället för en lista? 
//
// Svar: Man undviker lite overhead på att inte använda list.Om man vet från början vilken storlek man behöver och att
// den inte kommer ändras OCH att man klarar sig utan lists metoder, så kan man tjäna lite på att välja array.Kan även
// vara ett sätt att vara tydlig med vilken storlek en samling förväntas ha. 
//
// Att undvika skalningskostnaden med en fix storlek är inte skäl nog, eftersom list kan ges en valfri initial kapacitet. 



// Övning 2: ExamineQueue() 

// 1. Simulera följande kö på papper:
// a. ICA öppnar och kön till kassan är tom 
// b. Kalle ställer sig i kön 
// c. Greta ställer sig i kön 
// d. Kalle blir expedierad och lämnar kön 
// e. Stina ställer sig i kön 
// f. Greta blir expedierad och lämnar kön 
// g. Olle ställer sig i kön 
// h. … 
//
// Svar: 
// a. L = []
// b. L = ["Kalle"]
// c. L = ["Kalle", "Greta"]
// d. L = ["Greta"]
// e. L = ["Greta", "Stina"]
// f. L = ["Stina"]
// g. L = ["Stina", "Olle"]



// Övning 3:  ExamineStack() 

// 1. Simulera ännu en gång ICA - kön på papper.Denna gång med en stack.Varför är det inte så smart att använda en stack i
// det här fallet? 

// Svar: 
// a. L = ["Kalle"]
// b. L = ["Greta", "Kalle"]
// c. L = ["Kalle"]
// d. L = ["Stina", "Kalle"]
// e. L = ["Kalle"]
// f. L = ["Olle", "Kalle"]

// Stacken är inte en fair kö.Den som joinar kön tidigt riskerar starvation, dvs. att aldrig få bli expiderad,
// eftersom stacken är FILO. 


// 2. Implementera en ReverseText - metod som läser in en sträng från användaren och med hjälp av en stack vänder ordning på
// teckenföljden för att sedan skriva ut den omvända strängen till användaren. 

// Svar: se implementation. 



// Övning 4: CheckParenthesis() 

// 1. Skapa med hjälp av er nya kunskap funktionalitet för att kontrollera en välformad sträng på papper. Du ska använda
// dig av någon eller några av de datastrukturer vi precis gått igenom.Vilken datastruktur använder du?  
//
// Svar: Stack. Vi behöver matcha en stängande parentes med den senast öppnade parentesen.Om de matchar ska vi stryka denna
// öppnande parentes och matcha nästa stängande parentes med den näst senaste öppnande, osv. Detta beteende får
// vi naturligt om vi lägger öppnande parenteser i en stack och poppar den översta varje gång vi behöver matcha en
// stängande parentes.Slutligen måste vi även kontrollera så att stacken är tom när vi gått igenom hela strängen.

// se implementation


// 2.Implementera funktionaliteten i metoden CheckParentheses. Låt programmet läsa in en sträng från användaren och
// returnera ett svar som reflekterar huruvida strängen är välformad eller ej. 

// Svar: se implementation. 



// Övning 5: Rekursion 

// 1. Illustrera förloppen för RecursiveOdd(1), RecursiveOdd(3) och RecursiveOdd(5) på papper för att förstå den rekursiva loopen. 
//
// Svar: 
// RecursiveOdd(1) = 1
//
// RecursiveOdd(3) = 
// (RecursiveOdd(2) + 2) = 
// ((RecursiveOdd(1) + 2) + 2) = 
// ((1 + 2) + 2) = 
// (3 + 2) = 
// 5
//
// RecursiveOdd(5) = 
// (RecursiveOdd(4) + 2) = 
// ((RecursiveOdd(3) + 2) + 2) =
// (((RecursiveOdd(2) + 2) + 2) + 2) = 
// ((((RecursiveOdd(1) + 2) + 2) + 2) + 2) = 
// ((((1 + 2) + 2) + 2) + 2) = 
// (((3 + 2) + 2) + 2) = 
// ((5 + 2) + 2) =
// (7 + 2) = 
// 9


// 2. Skriv en RecursiveEven(int n) metod som rekursivt beräknar det n:te jämna talet. 

// Svar: se implementation. Jag räknar 0 som det första jämna talet.  


// 3. Implementera en rekursiv funktion för att beräkna tal i fibonaccisekvensen: (f(n) = f(n-1) + f(n-2)) 

// Svar: se implementation. Jag har indexerat från 0, dvs F_0 = 0, F_1 = 1, ...



// Övning 6: Iteration 

// 1. Illustrera på papper förloppen för IterativeOdd(1), IterativeOdd(3) och IterativeOdd(5) för att förstå iterationen.

// Svar:
// n = 1
// criteria: n - 1 = 0
//
// iterationer (state): 
// result = 1, i = 0, i < 0 är false (exit) -> return 1
//
//
// n = 3
// criteria = n - 1 = 2
//
// iterationer (state): 
// result = 1, i = 0, i < 2 är true (stay)
// result = 3, i = 1, i < 2 är true (stay)
// result = 5, i = 2, i < 2 är false (exit) -> return 5
//
//
// n = 5
// criteria = n - 1 = 4
//
// iterationer (state): 
// result = 1, i = 0, i < 4 är true (stay)
// result = 3, i = 1, i < 4 är true (stay)
// result = 5, i = 2, i < 4 är true (stay)
// result = 7, i = 3, i < 4 är true (stay)
// result = 9, i = 4, i < 4 är false (exit) -> return 9 


// 2. Skapa en IterativeEven(int n) funktion för att iterativt beräkna det n:te jämna talet. 

// Svar: se implementation. 


// 3. Implementera en iterativ version av fibonacciberäknaren. 

// Svar: se implementation. 


// Fråga: 
// Utgå ifrån era nyvunna kunskaper om iteration, rekursion och minneshantering. Vilken av ovanstående funktioner är mest 
// minnesvänlig och varför?  
//
// Svar: IterativeOdd. Vid varje metodanrop för RecursiveOdd reserveras en ny frame på stacken. Ingen frame kommer
// stängas förrän dess metod returnerar ett värde, men varje metod väntar på att nästa ska bli klar. Detta betyder att
// vi kommer bygga en stapel av frames ända fram till dess att vi når basfallet och det sista callet
// returnerar, varpå vi kan börja nysta upp rekursionen baklänges. Denna process tar upp mer minne än vid
// iteration, där vi bara använder en frame och istället uppdaterar variabler inom den framen. 