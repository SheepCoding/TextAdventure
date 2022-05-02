using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
    class CommandTakeAction : AbstractCommand
    {
        public CommandTakeAction(Player player) : base(player)
        {
            Description = "Item mitnehmen";
        }

        public override string Execute(int choice)
        {
            // reciever.Action ()
            return MyPlayer.TakeAction(choice);
        }

    }
}
