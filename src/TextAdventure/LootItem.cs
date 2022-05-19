using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
    class LootItem : Item
    {
        private Room MyRoom;

        private string Typ;

        public LootItem(int id, string name, string description, string interactText, string packText, bool open, bool secret, bool fix, Room room, string typ): base(id, name, description, interactText, packText)
        {
            MaxNumber = 1;
            Open = open;
            Secret = secret;
            Fix = fix;
            MyRoom = room;
            Typ = typ;
        }

        public Room GetRoom()
        {
            return MyRoom;
        }

        public string GetTyp()
        {
            return Typ;
        }
    }
}
