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
        private int ActionFlag;
        private Random Rnd;

        public Game(Level level, Player player)
        {
            CurrentLevel = level;
            MyPlayer = player;
            ActionFlag = 0;
            Rnd = new Random();
        }

        public Player GetPlayer()
        {
            return MyPlayer;
        }

        // Abhängig von ActionFlag richtige Aktion durchführen
        public string ExecuteCommand(string command)
        {
            string text;
            switch (ActionFlag)
            {
                // Spieler hat Action ausgewählt
                case 0:
                    text = Action(command);
                    break;
                // Spieler hat Tür ausgewählt
                case 1:
                    text = Go(command);
                    break;
                // Spieler hat Item zum Iteragieren ausgewählt
                case 3:
                    text = InteractItem(command);
                    break;
                // Spieler hat Item zum Einpacken ausgewählt
                case 4:
                    text = PackItem(command);
                    break;
                default:
                    ActionFlag = 0;
                    text = "\r\nUps, du hast vergessen was du eigentlich tun wolltest.";
                    text += GetActionSelection();
                    break;
            }
            return text;
            
        }

        // Auswahl der Aktionen anzeigen
        public string GetActionSelection()
        {
            string text = "\r\n Was möchtest du tun?";
            text += "\r\n > 1: Raum wechseln";
            text += "\r\n > 2: Raum durchsuchen";
            text += "\r\n > 3: mit einem Item interagieren";
            text += "\r\n > 4: ein Item einpacken";
            return text;
        }

        // Action 1: Auswahl der Türen anzeigen
        private string GetDoorSelection()
        {
            string text = "\r\nDurch welche Tür möchtes du gehen? ";
            text += MyPlayer.GetCurrentRoom().PrintDoors();
            return text;
        }

        // Action 3 & 4: Auswahl der Items anzeigen
        private string GetItemSelection()
        {
            string text = MyPlayer.GetCurrentRoom().PrintItems();
            return text;
        }

        // ActionFlag = 0: ausgewählte Aktion ausführen
        private string Action(string command)
        {
            string text;
            try
            {
                int nextAction = int.Parse(command);

                switch (nextAction)
                {
                    case 0:
                        text = "\r\nDu hast Selbstmord begangen und bist jetzt ein freier Geist.";
                        break;
                    case 1:
                        text = GetDoorSelection();
                        ActionFlag = 1;
                        break;
                    case 2:
                        text = SearchRoom();
                        ActionFlag = 0;
                        break;
                    case 3:
                        text = "\r\nWelches Item möchtest du benutzen? ";
                        text += GetItemSelection();
                        ActionFlag = 3;
                        break;
                    case 4:
                        text = "\r\nWelches Item möchtest du einpacken? ";
                        text += GetItemSelection();
                        ActionFlag = 4;
                        break;
                    default:
                        text = "\r\nDas stand nicht zur Auswahl.";
                        text += GetActionSelection();
                        break;
                }
                return text;
            }
            catch (Exception)
            {
                text = "\r\nDu bist wegen zu großer Idiotie gestorben und bist jetzt ein Untoter.";
                return text + GetActionSelection();
            }
        }

        //  ActionFlag = 1: durch Tür gehen (Türauswahl auswerten)
        public string Go(string command)
        {
            string text = "";
            try
            {
                int nextDoor = int.Parse(command);

                if (nextDoor == 0)
                {
                    ActionFlag = 0;
                    return ("\r\nDu hast Selbstmord begangen und bist jetzt ein freier Geist.");
                }

                Door door = MyPlayer.GetCurrentRoom().GetDoor(nextDoor);

                // gab es diese Tür?
                if (door != null)
                {
                    // prüfe ob Tür offen ist
                    if (door.IsOpen())
                    {
                        // wenn ja, durchgehen
                        text = door.GetOpenText();
                        // ist der raum1 der tür gleich dem raum, in dem wir gerade sind?
                        if (door.GetRoom1().GetId() == MyPlayer.GetCurrentRoom().GetId())
                        {
                            // ja
                            MyPlayer.SetCurrentRoom(door.GetRoom2());
                        }
                        else
                        {
                            // nein
                            MyPlayer.SetCurrentRoom(door.GetRoom1());
                        }
                    }
                    else
                    {
                        ActionFlag = 0;
                        text = ("Diese Tür ist verschlossen.");
                        return text + GetActionSelection();
                    }

                }
                // durch Tür gehen, neuen Raum anzeigen und fragen, was der Spieler tun möchte
                ActionFlag = 0;
                text += (" Du gehst durch die Tür. " + "\r\n" + MyPlayer.GetCurrentRoom().Print());
                return text + GetActionSelection();
            }
            catch (Exception)
            {
                ActionFlag = 0;
                text = ("Du bist wegen zu großer Idiotie gestorben und bist jetzt ein Untoter.");
                return text + GetActionSelection();
            }
        }

        //  Action 2: geheime Sachen finden und anzeigen
        private string SearchRoom()
        {
            string text = "Du durchsuchst den Raum. ";
            // findet zufällig bis zu 4 geheime Items und/oder Türen
            text += MyPlayer.GetCurrentRoom().Search(Rnd.Next(1, 5));
            return text + "\r\n" + GetActionSelection(); 
        }

        // ActionFlag = 3: mit ausgewähltem Item interagieren
        private string InteractItem(string command)
        {
            string text;
            try
            {
                int index = int.Parse(command);
                text = MyPlayer.InteractItem(index - 1);
                ActionFlag = 0;
                return text + GetActionSelection();
            }
            catch (Exception)
            {
                ActionFlag = 0;
                text = ("Du bist wegen zu großer Idiotie gestorben und bist jetzt ein Untoter.");
                return text + GetActionSelection();
            }
        }

        // ActionFlag = 4: ausgewähltes Item einpacken
        private string PackItem(string command)
        {
            string text;
            try
            {
                int index = int.Parse(command);
                text = MyPlayer.PackItem(index - 1);
                ActionFlag = 0;
                return text + GetActionSelection();
            }
            catch (Exception)
            {
                ActionFlag = 0;
                text = ("Du bist wegen zu großer Idiotie gestorben und bist jetzt ein Untoter.");
                return text + GetActionSelection();
            }
        }


    }
}
