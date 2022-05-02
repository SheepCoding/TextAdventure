using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
    class CommandExitAction : AbstractCommand
    {
        public CommandExitAction(Player player): base(player)
        {
            Description = "Spiel beenden";
        }

        public override string Execute(int choice)
        {
            // reciever.Action ()
            return MyPlayer.ExitAction();
        }
    }
}
