using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TextAdventure
{
    class Program
    {
        static void Main(string[] args)
        {
            // Ordentliche Ausgabe von Sonderzeichen und Umlauten
            Console.OutputEncoding = Encoding.UTF8;

            // neues Level erstellen und Daten aus Datei laden
            Level myLevel = new Level();
            myLevel.Load();

            // neuen Spiler erzeugen
            Player player = new Player("Max", myLevel.GetFirstRoom(), 10);

            // neues Spiel erzeugen mit geladenen Level
            Game myGame = new Game(myLevel, player);

            Console.WriteLine("\n\n\n Versuche zu entkommen.");
            // Ersten Raum ausgeben
            Console.WriteLine(player.GetCurrentRoom().Print());
            Console.WriteLine(myGame.GetActionSelection());

            string command = "";
            while (command != "exit")
            {
                command = Console.ReadLine();
                string text = myGame.ExecuteCommand(command);
                Console.WriteLine(text);
            }

            Console.ReadLine();
        }
    }
}
