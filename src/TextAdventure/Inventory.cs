using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
    class Inventory
    {
        private List<Item> ItemList;
        private int Size;
        private int MaxSize;

        public Inventory(int maxSize)
        {
            ItemList = new List<Item>();
            Size = 0;
            MaxSize = maxSize;
        }

        // liefert Item aus der ItemListe an der Stelle des übergebenen Index zurück
        public Item GetItem(int index)
        {
            // Zur Sicherheit prüfen: gibt es das Item
            if (index < ItemList.Count())
            {
                return (ItemList[index]);
            }

            Console.WriteLine("Dieses Item " + index + " gibt es nicht");
            return null;
        }
        
        // gibt die Anzahl aller Items zurück
        public int GetSize()
        {
            return Size;
        }

        // zur Liste der Items ein neues Item hinzufügen
        public void AddItem(Item item)
        {
            if (Size < MaxSize)
            {
                // prüfe, ob Item vom Typ LootItem ist
                if (item.GetType().Equals(typeof(LootItem)))
                {
                    // LootItems dürfen nur einmal in der Liste vorkommen
                    if (!ItemList.Contains(item))
                    {
                        ItemList.Add(item);
                        Size++;
                    }
                    // ToDo: else mit Fehlermeldung
                }
                // DecoItems können beliebig oft in der Liste sein
                else
                {
                    ItemList.Add(item);
                }
            }
        }

        // aus Liste der Items übergebenes Item löschn
        public void RemoveItem(Item item)
        {
            ItemList.Remove(item);
            Size--;
        }

        // aus Liste der Items Item an der Stelle des übergebenen Indexes löschn
        public void RemoveItemAt(int index)
        {
            ItemList.RemoveAt(index);
            Size--;
        }

        // gibt die Anzahl des übergebenen Items zurück
        public int CountItem(Item myItem)
        {
            int number = 0;
            foreach (Item item in ItemList)
            {
                if (item == myItem)
                {
                    number++;
                }
            }
            return number;
        }

        // Mischt die Liste der Items
        public void MixItems()
        {
            List<Item> newItemList = new List<Item>();
            Random rnd = new Random();
            // solange alte Liste noch nicht leer ist, füge ein Item aus der alten Liste in die Neue
            while (ItemList.Count() > 0)
            {
                int index = rnd.Next(0, ItemList.Count());
                newItemList.Add(ItemList[index]);
                ItemList.RemoveAt(index);
            }
            ItemList = newItemList;
        }

        // Ausgabe aller Namen der Items mit Komma getrennt
        public string PrintNames()
        {
            string text = "";
            foreach (Item item in ItemList)
            {
                // die Item kann sich selber drucken
                text += " " + item.PrintName() + ", ";
            }
            return text;
        }
        
        // Ausgabe aller Items nummeriert und untereinander
        public string Print()
        {
            string text = "";
            int i = 0;
            foreach (Item item in ItemList)
            {
                // das Item kann sich selber drucken
                i++;
                text += "\r\n > " + i.ToString() + ": " + item.Print();
            }
            return text;
        }

    }
}
