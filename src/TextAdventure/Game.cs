using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
    class Game
    {
        private Level CurrentLevel;
        private Player MyPlayer;

        private Dictionary<string, AbstractCommand> CommandDic;

        public Game(Level level, Player player)
        {
            CurrentLevel = level;
            MyPlayer = player;

            // Command Pattern: Invoker, Kommandos anlegen
            CommandDic = new Dictionary<string, AbstractCommand>();
            CommandDic.Add("go", new CommandGoAction(MyPlayer));
            CommandDic.Add("take", new CommandTakeAction(MyPlayer));
            CommandDic.Add("search", new CommandSearchAction(MyPlayer));
            CommandDic.Add("interact", new CommandInteractAction(MyPlayer));
            CommandDic.Add("drop", new CommandDropAction(MyPlayer));
            CommandDic.Add("exit", new CommandExitAction(MyPlayer));
        }

        public Player GetPlayer()
        {
            return MyPlayer;
        }

        // Abhängig von ActionFlag richtige Aktion durchführen
        public string ExecuteCommand(string command)
        {
            string text;
            string[] res = command.Split(' ');
            try
            {
                string action = res[0];
                int choice;
                try
                {
                    choice = int.Parse(res[1]);
                }
                catch (Exception)
                {
                    choice = 0;
                }
                // Aufruf per Command Pattern: Invoker
                // Grundidee des Patterns: der Player wird hier im Kommando versteckt
                text = CommandDic[action].Execute(choice);
            }
            catch (Exception)
            {
                //text = "\r\nDas kannst du hier nicht machen.";
                text = GetActionSelection();
            }
            return text;
        }

        // Auswahl der Aktionen anzeigen
        public string GetActionSelection()
        {
            string text = "\r\n Was möchtest du tun?";
            foreach (string commandKey in CommandDic.Keys)
            {
                text += "\r\n > " + commandKey + ": " + CommandDic[commandKey].GetDescription();
            }
            return text;
        }

    }
}
