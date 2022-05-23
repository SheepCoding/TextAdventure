using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
    class Player
    {
        private Room CurrentRoom;
        private Inventory MyInventory;
        private string Name;
        private Random RandomClass;

        // speichert die Tür, die der Spieler öffnen möchte
        private int DoorToOpenId;

        public Player(string name, Room currentRoom, int maxSizeInventory)
        {
            Name = name;
            CurrentRoom = currentRoom;
            MyInventory = new Inventory(maxSizeInventory);
            RandomClass = new Random();
            DoorToOpenId = -1;

        }

        public string GetName()
        {
            return Name;
        }

        public Room GetCurrentRoom()
        {
            return CurrentRoom;
        }

        public Inventory GetInventory()
        {
            return MyInventory;
        }

        public void SetCurrentRoom(Room room)
        {
            CurrentRoom = room;
        }

        private string GoThroughDoor(Door door)
        {
            string text = " Diese Tür existiert nicht.";
            // prüfe, das Tür existiert
            if (door != null)
            {
                // prüfe, ob Tür offen ist
                // wenn ja, durchgehen
                if (door.IsOpen())
                {
                    // vorherige geschlossene Tür vergessen
                    DoorToOpenId = -1;

                    text = door.GetOpenText();
                    // ist der raum1 der tür gleich dem raum, in dem wir gerade sind?
                    if (door.GetRoom1().GetId() == CurrentRoom.GetId())
                    {
                        // ja --> in anderen Raum gehen
                        CurrentRoom = door.GetRoom2();
                    }
                    else
                    {
                        // nein --> in Raum gehen
                        CurrentRoom = door.GetRoom1();
                    }
                    text += " Du gehst durch die Tür. " + "\r\n" + CurrentRoom.Print();
                }
                else
                {
                    // speichern der zu öffenen Tür
                    DoorToOpenId = door.GetId();
                    text = " Diese Tür ist verschlossen. \n Wenn du sie öffnen möchtest, benutze einen Schlüssel aus deinem Inventar.";
                    text += MyInventory.Print();
                }
            }
            return text;

        }

        // Raum wechseln
        public string GoAction(int nextDoor)
        {
            string text;
            // wenn keine Tür gewählt wurde, Auswahl anzeigen
            if (nextDoor == 0)
            {
                text = "\r\n Durch welche Tür möchtes du gehen? ";
                text += CurrentRoom.PrintDoors();
            }
            // es wurde eine Tür gewählt
            else
            {
                Door door = CurrentRoom.GetDoor(nextDoor);
                // durch Tür gehen
                text = GoThroughDoor(door);
            }
            return text;
        }

        // Raum durchsuchen
        public string SearchAction()
        {
            string text = " Du durchsuchst den Raum genauer. ";
            // findet zufällig bis zu 4 geheime Items und/oder Türen
            text += CurrentRoom.Search(RandomClass.Next(1, 5));
            return text;
        }

        // Item ins eigene Inventar einpacken
        public string TakeAction(int index)
        {
            string text;
            // wenn kein Item gewählt wurde, Auswahl anzeigen
            if (index == 0)
            {
                text = "\r\n Welches Item möchtest du mitnehmen? ";
                text += CurrentRoom.PrintItems();
            }
            // es wurde ein Item ausgewählt
            else
            {
                Item item = CurrentRoom.GetItem(index - 1);
                // prüfe, ob Item existiert
                if (item != null)
                {
                    // prüfe, ob man das Item mitnehmen kann
                    if (!item.IsFix())
                    {
                        // Item einpacken
                        MyInventory.AddItem(item);
                        CurrentRoom.RemoveItem(item);
                    }
                    text = item.GetPackText();
                }
                else
                {
                    text = " Das stand nicht zur Auswahl.";
                }
            }
            return text;
        }

        // interagiert mit Item im Raum
        public string InteractAction(int index)
        {
            string text;
            // wenn kein Item gewählt wurde, Auswahl anzeigen
            if (index == 0)
            {
                text = "\r\n Welches Item möchtest du benutzen? ";
                text += CurrentRoom.PrintItems();
            }
            // es wurde ein Item ausgewählt
            else
            {
                Item item = CurrentRoom.GetItem(index - 1);
                // prüfe, ob Item existiert
                if (item != null)
                {
                    text = item.GetInteractText();
                }
                else
                {
                    text = " Das stand nicht zur Auswahl.";
                }
            }
            return text;
        }

        // Item aus eigenem Inventar fallen lassen
        public string DropAction(int index)
        {
            string text;
            // wenn kein Item gewählt wurde, Auswahl anzeigen
            if (index == 0)
            {
                text = "\r\n Welches Item möchtest du fallen lassen? ";
                text += MyInventory.Print();
            }
            // es wurde ein Item ausgewählt
            else
            {
                Item item = MyInventory.GetItem(index - 1);
                // prüfe, ob Item existiert
                if (item != null)
                {
                    // Item fallen lassen
                    MyInventory.RemoveItem(item);
                    CurrentRoom.AddItem(item);
                    text = " Du hast " + item.GetName() + " fallen gelassen.";
                }
                else
                {
                    text = " Das stand nicht zur Auswahl.";
                }
            }
            return text;
        }

        // Spiel beenden
        public string ExitAction()
        {
            // ToDo Speichern
            return "\r\n Spiel beendet!";
        }

        // Item aus eigenem Inventar benutzen
        public string UseAction(int index)
        {
            string text;
            // wenn kein Item gewählt wurde, Auswahl anzeigen
            if (index == 0)
            {
                text = "\r\n Welches deiner Items möchtest du benutzen? ";
                text += MyInventory.Print();
            }
            // es wurde ein Item ausgewählt
            else
            {
                Item item = MyInventory.GetItem(index - 1);
                // prüfe, ob Item existiert
                if (item != null)
                {
                    //ToDo: Item benutzen
                    switch (item.GetTyp())
                    {
                        // Item ist Schlüssel
                        case "key":
                            // Tür öffnen, wenn eine gemerkt wurde
                            if (DoorToOpenId != -1)
                            {
                                text = " Du benutzt den Schlüssel und öffnest die Tür.";
                                CurrentRoom.GetDoor(DoorToOpenId).SetOpen(true);
                                text += GoThroughDoor(CurrentRoom.GetDoor(DoorToOpenId));
                                // Schlüssel verschwindet (bleibt in Tür stecken)
                                MyInventory.RemoveItem(item);
                            }
                            else
                            {
                                text = " Du kannst den Schlüssel hier nicht benutzen, da du vor keiner verschlossenen Tür stehst.";
                            }
                            break;
                        default:
                            text = item.GetInteractText();
                            break;
                    }
                }
                else
                {
                    text = " Das stand nicht zur Auswahl.";
                }
            }
            return text;
        }
    }

}

