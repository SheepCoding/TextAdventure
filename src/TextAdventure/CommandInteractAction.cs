using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
    class CommandInteractAction : AbstractCommand
    {
        public CommandInteractAction(Player player): base(player)
        {
            Description = "Item benutzen";
        }
        public override string Execute(int choice)
        {
            // reciever.Action ()
            return MyPlayer.InteractAction(choice);

        }
    }
}
