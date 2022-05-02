using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
    class Door
    {
        private int Id;
        private string Name;
        private string OpenText;
        private bool Open;
        private bool Secret;

        private Room Room1;
        private Room Room2;

        public Door(int id, string name, string openText, bool open, bool secret, Room room1, Room room2)
        {
            Id = id;
            Name = name;
            OpenText = openText;
            Open = open;
            Secret = secret;
            Room1 = room1;
            Room2 = room2;


    }
        
        public int GetId()
        {
            return Id;
        }

        public string GetName()
        {
            return Name;
        }
        
        public string GetOpenText()
        {
            return OpenText;
        }

        public Room GetRoom1()
        {
            return Room1;
        }

        public Room GetRoom2()
        {
            return Room2;
        }

        public void SetSecret(bool secret)
        {
            Secret = secret;
        }

        public bool IsOpen()
        {
            return Open;
        }

        public bool IsSecret()
        {
            return Secret;
        }


        // übergibt Id unnd Name der Tür, wenn sie nicht geheim ist
        public string PrintId()
        {
            // übergibt nur Türen, die nicht geheim sind
            if (!Secret)
            {
                return (Id + ": " + Name);
            }
            return null;
        }

        // übergibt Name der Tür, wenn sie nicht geheim ist
        public string Print()
        {
            // übergibt nur Türen, die nicht geheim sind
            if (!Secret)
            {
                return (Name);
            }
            return null;
        }

    }

}
