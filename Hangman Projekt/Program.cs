using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Hangman_Projekt;


class Program
{
    static void Main(string[] args)
    {
        MainMenu();
    }
    static void MainMenu()
    {
        while (true)
        {
            Console.WriteLine(@"

 ██░ ██  ▄▄▄       ███▄    █   ▄████  ███▄ ▄███▓ ▄▄▄       ███▄    █ 
▓██░ ██▒▒████▄     ██ ▀█   █  ██▒ ▀█▒▓██▒▀█▀ ██▒▒████▄     ██ ▀█   █ 
▒██▀▀██░▒██  ▀█▄  ▓██  ▀█ ██▒▒██░▄▄▄░▓██    ▓██░▒██  ▀█▄  ▓██  ▀█ ██▒
░▓█ ░██ ░██▄▄▄▄██ ▓██▒  ▐▌██▒░▓█  ██▓▒██    ▒██ ░██▄▄▄▄██ ▓██▒  ▐▌██▒
░▓█▒░██▓ ▓█   ▓██▒▒██░   ▓██░░▒▓███▀▒▒██▒   ░██▒ ▓█   ▓██▒▒██░   ▓██░
 ▒ ░░▒░▒ ▒▒   ▓▒█░░ ▒░   ▒ ▒  ░▒   ▒ ░ ▒░   ░  ░ ▒▒   ▓▒█░░ ▒░   ▒ ▒ 
 ▒ ░▒░ ░  ▒   ▒▒ ░░ ░░   ░ ▒░  ░   ░ ░  ░      ░  ▒   ▒▒ ░░ ░░   ░ ▒░
 ░  ░░ ░  ░   ▒      ░   ░ ░ ░ ░   ░ ░      ░     ░   ▒      ░   ░ ░ 
 ░  ░  ░      ░  ░         ░       ░        ░         ░  ░         ░ 
                                                                     

");
            
            Console.WriteLine();

            Console.WriteLine("Wähle eine Aktion aus: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("[1] Spielen");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("[2] Beenden");
            Console.ResetColor();
            Console.WriteLine();


            Console.Write("Aktion: ");
            int action = Convert.ToInt32(Console.ReadLine());
            bool end = false;


            switch (action)
            {
                case 1:
                    StartGame();
                    break;

                case 2:
                    end = true;
                    break;
            }
            if (end)
            {
                break;
            }
            Console.Clear();
        }
    }
    static void StartGame()
    {
        string[] words = new string[]
        {
            "Ausbildung",
            "Programmierung",
            "Microsoft",
            "Csharp",
            "AnnualTour",
            "GermanSecureNetwork",
            "Groupinfra"

        };
        //Variable "rnd" erzeugt ein Zufälligen integer
        Random rnd = new Random();
        //neuer int "index" erstellt
        // (meine Variable rnd, wurde der Random methode zugeordnet). next (ist eine Methode der Random klasse mit der man eine nicht negative Ganzzahl erzeugen kann.)
        //rnd.Next(0 [kleinster Wert der gefunden werden kann in unserem Array], words.Length [das Programm kann bis zum letzten Array aus unserem String array alles zufällig auswählen])
        int index = rnd.Next(0, words.Length);
        //To.Lower bedeutet das der String nur in klein buchstaben ausgegeben wird, damit der Nutzer nicht "leben" abgezogen bekommt weil er den Großbuchstaben nicht errät.
        string word = words[index].ToLower();
        GameLoop(word);
    }
       
    static void GameLoop(string word)
    {
        int lives = 10;
        string hiddenWord = "";

        for(int i= 0; i < word.Length; i++)
        {
            hiddenWord += "_";
        }
        
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Gesuchtes Wort: " + hiddenWord);
            Console.Write("Noch übrige Versuche: ");

            for( int i= 0; i < lives; i++)
            {
                Console.ForegroundColor = ConsoleColor.Red; 
                Console.Write("X");
                Console.ResetColor();
            }
            Console.WriteLine();
            Console.Write("Buchstabe: ");
            //User kann einen groß oder klein buchstaben eingeben, das Ergebnis ist das gleiche.
            char character = Convert.ToChar(Console.ReadLine().ToLower());

            bool foundCharacterInWord = false;

            //Die for schleife sucht nach dem Wort das wir bekommen und vergleicht mit Character ob der Buchstabe(char) == enthalten ist.
            for (int i= 0; i < word.Length; i++)
            {
                if (word[i] == character)
                {
                    foundCharacterInWord = true;
                    break;
                }
            }
            string tempHiddenWord = hiddenWord;
            hiddenWord = "";
            if (foundCharacterInWord)
            {
                for (int i= 0; i < word.Length; i++)
                {
                    if (word[i] == character)
                    {
                        hiddenWord += character;
                    }
                    else if (tempHiddenWord[i] != '_')
                    {
                        hiddenWord += tempHiddenWord[i]; 
                    }
                    else
                    {
                        hiddenWord += '_';
                    }
                }
                if (hiddenWord == word)
                {
                    Console.Clear();
                    Console.ForegroundColor= ConsoleColor.Green;
                    Console.WriteLine("GEWONNEN!!!");
                    Console.WriteLine("Das Wort war: " + word);
                    Console.ReadKey();
                    Console.ResetColor();
                    break;
                }
            }
            else
            {
                hiddenWord = tempHiddenWord;

                if (lives > 0)
                {
                    lives -= 1;
                }
                else
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("GAME OVER!");
                    Console.ReadKey();
                    Console.ResetColor();
                    break;
                }
            }

        }
    }
}