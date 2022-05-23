using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
    // Command Pattern: concrete command
    class CommandUseAction : AbstractCommand
    {
        public CommandUseAction(Player player) : base(player)
        {
            Description = "eigenes Item benutzen";
        }

        public override string Execute(int choice)
        {
            // reciever.Action ()
            return MyPlayer.UseAction(choice);
        }

    }
}
