using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
    // Command Pattern: concrete command
    class CommandGoAction : AbstractCommand
    {

        public CommandGoAction(Player player): base (player)
        {
            Description = "Raum wechseln";
        }

        public override string Execute(int choice)
        {
            // reciever.Action ()
            return MyPlayer.GoAction(choice);
        }

    }
}
