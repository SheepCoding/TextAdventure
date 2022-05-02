using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
    class Room
    {
        private int Id;
        private string Name;
        private string Preposition;
        private string Description;

        private Dictionary<int, Door> DoorDic;
        private Inventory Items;
        private Inventory SecretItems;


        public Room(int id, string name, string preposition, string description)
        {
            Id = id;
            Name = name;
            Description = description;
            Preposition = preposition;
            DoorDic = new Dictionary<int, Door>();
            Items = new Inventory(30);
            SecretItems = new Inventory(15);
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

        public Door GetDoor(int id)
        {
            // Zur Sicherheit prüfen: gibt es die Tür
            if (DoorDic.ContainsKey(id))
            {
                return (DoorDic[id]);
            }

            Console.WriteLine("Die Tür " + id + " gibt es nicht");
            return null;
        }
        
        // liefert Item an der Stelle des übergebenen Index zurück
        public Item GetItem(int index)
        {
            return Items.GetItem(index);
        }
        
        // zur Liste der Türen im Raum eine neue Tür hinzufügen
        public void AddDoor(Door door)
        {
            // zur Sicherheit hier prüfen, ob es die Tür schon gibt
            if (!DoorDic.ContainsKey(door.GetId()))
            {
                // Tür in Dictionary eintragen
                DoorDic.Add(door.GetId(), door);
            }
            else
            {
                // es gibt sie schon (irgendjemand hat mist gebaut)
                // Fehler melden, zB per Log File
                Console.WriteLine("Room " + this.Id + ", " + Name + " AddDoor: " + door.GetId() + " kann nicht hinzugefügt werden");
            }
        }

        // zur Liste der Items im Raum ein neues Item hinzufügen
        public void AddItem(Item item)
        {
            // gheime Items in die Liste der Secret Items tun
            if (item.IsSecret())
            {
                SecretItems.AddItem(item);
            }
            else
            {
                Items.AddItem(item);
            }
        }
        
        // aus der Liste der Items ein Item löschen
        public void RemoveItem(Item item)
        {
            Items.RemoveItem(item);
        }

        // Püfen, ob Raum schon genug von übergebenen DecoItem hat
        public bool IsFull(DecoItem decoItem)
        {
            int number = Items.CountItem(decoItem);
            if (decoItem.GetMaxNumber() == number)
            {
                return true;
            }
           return false;
        }

        
        // Mischt die Liste der Items
        public void MixItems()
        {
            Items.MixItems();
            SecretItems.MixItems();
        }

        // zeigt maximal "MaxNumber" geheime Items und Türen
        public string Search(int MaxNumber)
        {
            string text = "Nach genauem Durchsuchen findst du ";
            int number = 0;
            bool gefunden = true;
            // solange etwas gefunden wurde und  noch nicht die maximale Anzahl an Sachen erreicht ist, suche
            while (gefunden && number < MaxNumber)
            {
                gefunden = false;
                // suche nach geheimen Items
                if (SecretItems.GetSize() > 0)
                {
                    // wenn geheimes Item gefunden, Item sichtbar machen und anzeigen 
                    Item item = SecretItems.GetItem(0);
                    item.SetSecret(false);
                    SecretItems.RemoveItemAt(0);
                    Items.AddItem(item);
                    text += "\r\n - " + item.Print();
                    gefunden = true;
                    number++;
                }
                else
                {
                    // wenn nicht genug Items existieren, suche geheime Türen
                    foreach (Door door in DoorDic.Values)
                    {
                        // wenn geheime Tür gefunden, Tür sichtbar machen und anzeigen
                        if (door.IsSecret())
                        {
                            door.SetSecret(false);
                            text += "\r\n - " + door.Print();
                            gefunden = true; 
                            number++;
                            break;
                        }
                    }
                }
            }
            // wenn nichts neus gefunden wurde
            if (number == 0)
            {
                return "Du hast nichts neues gefunden.";
            }
            return text;
        }

        // zeigt die Beschreibung des Raumes und alle Namen von den nicht geheimen Türen und Items
        public string Print()
        {
            // Ausgabe des Namens und der Beschreibung ds Raumes
            string text = "\r\n Du befindest dich " + Preposition + " " + Name + "." + "\r\n" + Description;
            text += "\r\n Du siehst folgende Sachen: ";
            // Ausgabe aller Türen
            //  .Values: die Liste der im Dictionary gespeicherten türen
            foreach (Door door in DoorDic.Values)
            {
                if (door.Print()!=null)
                {
                    // die Tür kann sich selber drucken
                text += " " + door.Print() + ", ";
                }
            }
            // Ausgabe aller Items
            text += Items.PrintNames();

            return (text + "\r\n");
        }

        // zeigt alle Türen mit Id
        public string PrintDoors()
        {
            string text = "Du hast folgende Türen zur Auswahl: ";
            foreach (Door door in DoorDic.Values)
            {
                // die Tür kann sich selber drucken
                text += "\r\n > " + door.PrintId();
            }
            return text;
        }

        // zeigt alle Items mit Bescheibung und nummeriert
        public string PrintItems()
        {
            string text = "Du hast folgende Items zur Auswahl: ";
            text += Items.Print();
            return text;
        }



    }
}
