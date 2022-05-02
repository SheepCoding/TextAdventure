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

        public Player(string name, Room currentRoom, int maxSizeInventory)
        {
            Name = name;
            CurrentRoom = currentRoom;
            MyInventory = new Inventory(maxSizeInventory);
            RandomClass = new Random();

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

                // gab es diese Tür?
                if (door != null)
                {
                    // prüfe ob Tür offen ist
                    if (door.IsOpen())
                    {
                        // wenn ja, durchgehen
                        text = door.GetOpenText();
                        // ist der raum1 der tür gleich dem raum, in dem wir gerade sind?
                        if (door.GetRoom1().GetId() == CurrentRoom.GetId())
                        {
                            // ja
                            CurrentRoom = door.GetRoom2();
                        }
                        else
                        {
                            // nein
                            CurrentRoom = door.GetRoom1();
                        }
                        text += " Du gehst durch die Tür. " + "\r\n" + CurrentRoom.Print();
                    }
                    else
                    {
                        text = " Diese Tür ist verschlossen.";
                    }
                }
                else
                {
                    text = " Diese Tür existiert nicht.";
                }
            }
            return text;
        }

        // Raum durchsuchen
        public string SearchAction()
        {
            string text = " Du durchsuchst den Raum. ";
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
    }

}

