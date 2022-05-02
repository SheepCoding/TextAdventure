using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
    class Level
    {
        private Room FirstRoom = null;

        public Level()
        {
        }
        
        
        public Room GetFirstRoom()
        {
            return FirstRoom;
        }

        public void SetFirstRoom(Room room)
        {
            FirstRoom = room;
        }
        
        public void Load()
        {
            MapFile map = new MapFile();
            map.LoadDatabase(this);
        }

    }
}
