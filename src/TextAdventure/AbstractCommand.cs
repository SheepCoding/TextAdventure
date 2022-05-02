using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
    abstract class AbstractCommand
    {
        // Command Pattern: Interface oder abstrakte Klasse
        protected Player MyPlayer;
        protected string Description;

        public AbstractCommand(Player player)
        {
            MyPlayer = player;
            Description = "";
        }

        // Bauplan wie im Interface
        public abstract string Execute(int choice);

        public string GetDescription()
        {
            return Description;
        }

    }
}
