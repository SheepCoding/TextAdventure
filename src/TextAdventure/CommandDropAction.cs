using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
    class CommandDropAction : AbstractCommand
    {

        public CommandDropAction(Player player) : base(player)
        {
            Description = "Item fallen lassen";
        }

        public override string Execute(int choice)
        {
            // reciever.Action ()
            return MyPlayer.DropAction(choice);
        }
    }

}
