using System;



string parolaEstratta = "", parolaindovinata = "", parolasbagliata = "";
// stampa le parole indovinate e sbagliate a seconda del numero n
void stampaparolefinale(string parole, int n )// n è il numero che determina se deve stampare le parole sbagliate dall'utente o quelle indovinate
{
    string[] parolearray = parole.Split(';');
    if (n == 1)
    {
        Console.WriteLine($"Hai indovinato {parolearray.Length} parole");
        Console.WriteLine($"Parole indovinate: ");
        for (int i = 0; i < parolearray.Length; i++)
        {
         
                Console.Write(parolearray[i] + ", ");
           
         
        }
    }
    if (n == 2) 
    {
        Console.WriteLine($"Non hai indovinato {parolearray.Length-1} parole");
        Console.WriteLine($"Parole sbagliate: ");
        for (int i = 0; i < parolearray.Length; i++)
        {
           
                Console.Write(parolearray[i] + ", ");
            

        }
    }
}

// variabili relative alla gestione dei tentativi
int tentativi = 0, tentativiIniziali = 0;
// variabili relative alle monete
int coins = 30, costo = 20, gain = 10;
// difficoltà in base al numero di scelte disponibile, rispettivamente: facile, media, difficile
int[] mode = { 10, 7, 4 };
// decisione del giocatore della continuazione del gioco
bool partita=true;
//round nel quale cerca di indovinare
bool guessed = false;

char lettera = ' ';
bool letteratrovata = false;
int contalettere = 0, lettereoparola = 1;
string paroleIndovinate = "", parolesbagliate = "", parolaFinale = "";

Console.Title = "Gioco dell'Impiccato";

Console.WriteLine("======================================");
Console.WriteLine("      BENVENUTO NEL GIOCO DELL'IMPICCATO      ");
Console.WriteLine("======================================\n");

Console.WriteLine("Regole del gioco:");
Console.WriteLine("- Dovrai indovinare una parola segreta lettera per lettera.");
Console.WriteLine("- Hai un numero limitato di errori che puoi fare prima di perdere.");
Console.WriteLine("- Il numero di errori che puoi fare scade solo quando sbagli.");
Console.WriteLine("- Se indovini tutte le lettere, vinci!\n");

Console.WriteLine("Scegli la difficoltà:");
Console.WriteLine("[0] Facile   - 10 tentativi");
Console.WriteLine("[1] Medio    - 7 tentativi");
Console.WriteLine("[2] Difficile - 4 tentativi");

string scelta = "";
//ciclo while per scelta della difficoltà.
bool scelte = true;
while (scelte==true)
{
    Console.Write("\nInserisci la tua scelta (0-2): ");
    scelta = Console.ReadLine();

    if (scelta == "0")
    {
        tentativi = 10;
        Console.WriteLine("\nHai scelto la modalità FACILE! Avrai 10 tentativi.\n");
        scelte = false;
    }
    else if (scelta == "1")
    {
        tentativi = 7;
        Console.WriteLine("\nHai scelto la modalità MEDIA! Avrai 7 tentativi.\n");
        scelte=false;
    }
    else if (scelta == "2")
    {
        tentativi = 4;
        Console.WriteLine("\nHai scelto la modalità DIFFICILE! Avrai solo 4 tentativi.\n");
        scelte = false;
    }
    else
    {
        Console.WriteLine("Scelta non valida, riprova.");
    }
}
//salva numero di tentativi scelti
tentativiIniziali = tentativi;

//selta categoria
string chosentxtfile = "";
Console.WriteLine("Scegli una categoria di parole:");
Console.WriteLine("[1] Animali");
Console.WriteLine("[2] Paesi");
Console.WriteLine("[3] Strumenti Musicali");
Console.WriteLine("[4] Parole qualunque");
string sceltacategoria= Console.ReadLine();

    if(sceltacategoria == "1")
    {
        chosentxtfile = "animali.txt";
    }
    if (sceltacategoria == "2")
    {
        chosentxtfile = "paesi.txt";
    }
    if(sceltacategoria == "3")
    {
        chosentxtfile = "strumentiMusicali.txt";
    }
    if (sceltacategoria == "4") {
        chosentxtfile = "parole.txt";
    }
string[] wordsarray = File.ReadAllLines(chosentxtfile);

////sistema monete 
//Console.WriteLine("\n--- SISTEMA DI MONETE E AIUTI ---");
//Console.WriteLine("- Inizi con 30 monete.");
//Console.WriteLine("- Puoi spendere 20 monete per comprare un aiuto.");
//Console.WriteLine("- Ogni parola indovinata ti farà guadagnare 10 monete in più.");
//Console.WriteLine("- Usa le monete saggiamente per massimizzare le tue possibilità di vincere!\n");
//bool jollyused = false;

Console.WriteLine("Premi INVIO per iniziare il gioco...");
Console.ReadLine();


//for (int i = 0; i < wordsarray.Length; i++) { Console.WriteLine(wordsarray[i]); } prova per vedere se ha letto correttamente txt

string lettereUsate = null;

Random rnd = new Random();
int nrandom = rnd.Next(0, wordsarray.Length);


while (partita == true)
{
    guessed = false;
    tentativi = tentativiIniziali;
    lettereUsate = null;
    nrandom = rnd.Next(0, wordsarray.Length);
    parolaEstratta=wordsarray[nrandom];
    char[] parolaAlterata = new char[parolaEstratta.Length];
    
    //trasforma tutta la parola in _
    for(int i = 0; i < parolaEstratta.Length; i++)
    {
        parolaAlterata[i] ='_';

    }

    // sostituisce i _ con le lettere in posizione casuale
    for (int i = 0; i < 3; i++)
    {
        nrandom = rnd.Next(0, parolaAlterata.Length);
        parolaAlterata[nrandom] = parolaEstratta[nrandom];
        
    }

    //output parola con spazi vuoti
    for (int i = 0; i < parolaAlterata.Length; i++)
    {
        Console.Write($"{parolaAlterata[i]} ");
    }
    Console.WriteLine();
    //indovina
    while (guessed == false && tentativi!=0)
    {
        parolaFinale = null;
        lettereoparola = 1;
        letteratrovata= false;
        
        //errori a disposizione

            Console.WriteLine("Errori a disposizione: " + tentativi+ "\n");
        
       

        if (contalettere>=parolaAlterata.Length/2+1) 
        {
            Console.WriteLine("\nHai due opzioni:");
            Console.WriteLine("1) Continuare a indovinare una lettera per volta.");
            Console.WriteLine("2) Tentare di indovinare l'intera parola in un solo colpo!");

            Console.WriteLine("\nATTENZIONE: Se decidi di scrivere tutta la parola e sbagli, perderai immediatamente!");
            Console.WriteLine("Scegli con saggezza...\n");

            Console.Write("Digita [1] per continuare con una lettera, [2] per provare a indovinare la parola: ");
            lettereoparola=int.Parse(Console.ReadLine());
        }
        Console.WriteLine();
        Thread.Sleep(1000);

        if (lettereoparola == 1)
        {
            if (lettereUsate != null)
            {
                Console.WriteLine($"Lettere usate: {lettereUsate} ");
            }
            //prende lettera e converte in char
            Console.Write("Inserisci una lettera: ");
            lettera = char.ToLower(char.Parse(Console.ReadLine()));
            lettereUsate= lettera.ToString()+ ", ";

            for (int i = 0; i < parolaAlterata.Length; i++)
            {
                if (lettera == parolaEstratta[i])
                {
                    parolaAlterata[i] = lettera;
                    letteratrovata = true;
                }
                

            }
            //azioni di controllo nel caso il giocatore abbia indovinato la lettera
            if (letteratrovata == true)
            {

                Console.WriteLine("======================================");
                Console.WriteLine("LETTERA GIUSTA");
                Console.WriteLine("======================================\n");
                Console.Write($"la parola ora corrisponde a:  ' ");
                for (int i = 0; i < parolaAlterata.Length; i++)
                {
                    Console.Write($"{ parolaAlterata[i]} ");
                   
                }
                Console.Write(" '");
                Console.WriteLine();
            }

            //print parola ora se non ha indovinato
            else if(letteratrovata==false)
            {
                Console.WriteLine("======================================");
                Console.WriteLine($"LETTERA SBAGLIATA ");
                Console.WriteLine("======================================\n");
                Console.Write("La parola corrisponde a prima:  ' ");
                for (int i = 0; i < parolaAlterata.Length; i++)
                {
                    Console.Write(parolaAlterata[i] );
                }
                Console.Write(" '");
                Console.WriteLine();
                tentativi--;
            }
            //conta le lettere che appaiono nella parola
            for (int i = 0; i < parolaAlterata.Length; i++)
            {

                if (parolaAlterata[i] != '_')
                {
                    contalettere++;
                }
            }
            //controllo se ha indovinato in totalità la parola
            for (int i = 0; i < parolaEstratta.Length; i++)
            {
                parolaFinale += parolaAlterata[i];
            }
       

            if (parolaFinale == parolaEstratta)
            {
                guessed = true;
                Console.WriteLine("\n======================================");
                Console.WriteLine("         COMPLIMENTI!           ");
                Console.WriteLine("======================================\n");
                Console.WriteLine("Hai indovinato la parola! Ottimo lavoro!");
                Console.WriteLine("Hai guadagnato 10 monete extra.\n");
                coins += 10;
                paroleIndovinate += parolaEstratta + ";";
            }
        }


        if (lettereoparola == 2)
        {
            Console.WriteLine("Digita la parola che pensi sia giusta");
            parolaFinale=Console.ReadLine();
            if (parolaFinale == parolaEstratta)
            {

                Console.WriteLine("\nCOMPLIMENTI!");
                Console.WriteLine("Hai indovinato la parola! Ottimo lavoro!");
                Console.WriteLine("Hai guadagnato 10 monete extra.\n");
                paroleIndovinate += parolaEstratta + ";";
                guessed = true;
            }
            else
            {
                Console.WriteLine("\nMi dispiace, non hai indovinato la parola.");
                Console.WriteLine($"La parola corretta era: {parolaEstratta}.\n");
                parolesbagliate += parolaEstratta+";";

                tentativi = 0;
            }
            
        }

        
       

        
    }
    contalettere = 0;
    Console.WriteLine("\nVuoi provare con un'altra parola o terminare la partita?");
    Console.WriteLine("[1] Sì, prova un'altra parola");
    Console.WriteLine("[2] No, termina la partita e mostra il resoconto");

    string sceltaContinuazionegioco = Console.ReadLine();

    if (sceltaContinuazionegioco == "2")
    {
        partita = false;
    }
}

stampaparolefinale(paroleIndovinate,1);
stampaparolefinale(parolesbagliate, 2);
