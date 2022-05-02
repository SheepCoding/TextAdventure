using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
    class DecoItem : Item
    {

        public DecoItem(int id, string name, string description, string interactText, string packText, int maxNumber): base(id, name, description, interactText, packText)
        {
            MaxNumber = maxNumber;
            Secret = false;
            Fix = true;
        }

    }
}
