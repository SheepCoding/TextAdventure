using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
    class Item
    {
        private int Id;
        private string Name;
        private string Description;
        private string InteractText;
        private string PackText;
        protected int MaxNumber;
        protected bool Open;
        protected bool Secret;
        protected bool Fix;

        public Item(int id, string name, string description, string interactText, string packText)
        {
            Id = id;
            Name = name;
            Description = description;
            InteractText = interactText;
            PackText = packText;
            MaxNumber = 1;
            Open = true;
            Secret = false;
            Fix = true;
        }

        public int GetId()
        {
            return Id;
        }

        public string GetName()
        {
            return Name;
        }

        public string GetDescription()
        {
            return Description;
        }

        public string GetInteractText()
        {
            return InteractText;
        }
        
        public string GetPackText()
        {
            return PackText;
        }

        public int GetMaxNumber()
        {
            return MaxNumber;
        }

        public void SetSecret(bool secret)
        {
            Secret = secret;
        }

        public bool IsOpen()
        {
            return Open;
        }

        public bool IsSecret()
        {
            return Secret;
        }

        public bool IsFix()
        {
            return Fix;
        }

        // übergibt Name und Beschreibung, wenn Item nicht geheim ist
        public string Print()
        {
            // übergibt nur Items, die nicht geheim sind
            if (!Secret)
            {
                return (Name + ": " + Description);
            }
            return null;
        }

        // übergibt Name, wenn Item nicht geheim ist
        public string PrintName()
        {
            // übergibt nur Items, die nicht geheim sind
            if (!Secret)
            {
                return (Name);
            }
            return null;
        }



    }
}
