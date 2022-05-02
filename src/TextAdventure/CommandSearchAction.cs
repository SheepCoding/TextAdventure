using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
    class CommandSearchAction : AbstractCommand
    {

        public CommandSearchAction(Player player): base(player)
        {
            Description = "Raum durchsuchen";
        }

        public override string Execute(int choice)
        {
            return MyPlayer.SearchAction();
        }
    }
}
