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

        public Player(string name, Room currentRoom, int maxSizeInventory)
        {
            Name = name;
            CurrentRoom = currentRoom;
            MyInventory = new Inventory(maxSizeInventory);

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

        // packt Item, wenn möglich, ins eigene Inventar
        public string PackItem(int index)
        {
            Item item = CurrentRoom.GetItem(index);
            // prüfe, ob Item existiert
            if (item != null)
            {
                // prüfe, ob man das Item mitnehmen kann
                if (!item.IsFix())
                {
                    MyInventory.AddItem(item);
                    CurrentRoom.RemoveItem(item);
                }
                return item.GetPackText();
            }
            else
            {
                return "Das stand nicht zur Auswahl.";
            }
        }

        // interagiert mit Item, wenn es existiert
        public string InteractItem(int index)
        {
            Item item = CurrentRoom.GetItem(index);
            // prüfe, ob Item existiert
            if (item != null)
            {
                string text = item.GetInteractText();
                return text;
            }
            else
            {
                return "Das stand nicht zur Auswahl.";
            }
        }

    }

}

