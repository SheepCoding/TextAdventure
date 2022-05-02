using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MySql.Data.MySqlClient;

namespace TextAdventure
{
    class MapFile
    {
        private int LastKey;

        private Dictionary<int, Room> RoomDic;
        private Dictionary<int, Door> DoorDic;
        private Dictionary<int, LootItem> LootItemDic;
        private Dictionary<int, DecoItem> DecoItemDic;

        public MapFile()
        {
            RoomDic = new Dictionary<int, Room>();
            DoorDic = new Dictionary<int, Door>();
            LootItemDic = new Dictionary<int, LootItem>();
            DecoItemDic = new Dictionary<int, DecoItem>();
        }

        /* Mit der Datenbank verbinden und alles laden
        */
        public void LoadDatabase(Level level)
        {
            // Schritt 1: Verbinden mit der Datenbank
            string connStr = "server=localhost;user=root;database=textadventure;port=3306;password=";
            MySqlConnection connection = new MySqlConnection(connStr);

            try
            {
                Console.WriteLine("Connecting to MySQL...");
                connection.Open();

                this.LoadRoom(connection);
                this.LoadDoor(connection);
                this.LoadLootItem(connection);
                this.LoadDecoItem(connection);

                connection.Close();
                // Console.ReadLine ();

                // Räume mit Items füllen
                FillRooms();



            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            // ersten Raum festlegen
            level.SetFirstRoom(GetRoom(1));
        }

        private Room GetRoom(int key)
        {
            // Schlüssel drin?
            if (RoomDic.ContainsKey(key))
            {
                return RoomDic[key];
            }
            // Fehler
            Console.WriteLine("Dieser Raum existiert nicht.");
            return null;
        }

        /* fügt Raum zu lokalen Dictionary hinzu
         */
        private void AddRoom(Room room)
        {
            // wenn Schlüssel noch nicht vorhanden, Raum hinzufügen, sonst Fehlermeldung
            if (!RoomDic.ContainsKey(room.GetId()))
            {
                RoomDic.Add(room.GetId(), room);
            }
            else
            {
                Console.WriteLine("Raum " + room.GetName() + "kann nicht hinzugefügt werden.");
            }
        }

        /* fügt Tür zu lokalen Dictionary und zu entsprechenden Räumen hinzu
         */
        private void AddDoor(Door door)
        {
            if (!DoorDic.ContainsKey(door.GetId()))
            {
                // tür in unsere interne level laden verwaltung hinzufügen
                DoorDic.Add(door.GetId(), door);

                door.GetRoom1().AddDoor(door);
                door.GetRoom2().AddDoor(door);
            }
            else
            {
                Console.WriteLine("Fehler addDoor " + door.GetId());
            }

        }

        /* fügt LootItem zu lokalen Dictionary hinzu
         */
        private void AddLootItem(LootItem item)
        {
            if (!LootItemDic.ContainsKey(item.GetId()))
            {
                // Item in unsere interne level laden verwaltung hinzufügen
                LootItemDic.Add(item.GetId(), item);

                //item.GetRoom().AddItem(item);
            }
            else
            {
                Console.WriteLine("Fehler addItem " + item.GetId());
            }
        }

        /* fügt DecoItem zu lokalen Dictionary hinzu 
         * und merkt sich größte Id
         */
        private void AddDecoItem(DecoItem item)
        {
            if (!DecoItemDic.ContainsKey(item.GetId()))
            {
                // Item in unsere interne level laden verwaltung hinzufügen
                DecoItemDic.Add(item.GetId(), item);
                // wenn Id Größer ist als alle vorherigen, merken
                if (item.GetId() > LastKey)
                {
                    LastKey = item.GetId();
                }
            }
            else
            {
                Console.WriteLine("Fehler addDecoItem " + item.GetId());
            }
        }

        /* Raum mit DekoItems füllen und Itemliste mischen
         */
        private void FillRooms()
        {
            Random rnd = new Random();
            // jeden Raum mit DekoItems füllen füllen
            foreach (Room room in RoomDic.Values)
            {
                // maximale Anzahl von DecoItems im Raum
                int roomMaxNumber = 10;
                int number = 0;
                // solange maximale Anzahl noch nicht überschritten ist, füge zufälliges DecoItem hinzu
                while (number <= roomMaxNumber)
                {
                    // suche zufällige Zahl zwischen 1 und der größten Id der DecoItems
                    int randomId = rnd.Next(1, LastKey);
                    // prüfe, ob ein DecoItem mit dieser Id existiert
                    if (DecoItemDic.ContainsKey(randomId))
                    {
                        // wenn ja, wähle dieses Item
                        DecoItem item = DecoItemDic[randomId];
                        // prüfe, ob die maximale Anzahl von diesem Item noch nicht im Raum ist
                        if (!room.IsFull(item))
                        {
                            // wenn ja, füge Item zu Raum hinzu und erhöhe Anzahl
                            room.AddItem(item);
                            number++;
                        }
                    }
                }
            }
            // sortiere jedes Item in den passenen Raum
            foreach (LootItem item in LootItemDic.Values)
            {
                item.GetRoom().AddItem(item);
            }
            // in jedem Raum Items mischen
            foreach (Room room in RoomDic.Values)
            {
                room.MixItems();
            }
        }

        /* läd alle Räume aus Datenbank
         */
        private void LoadRoom(MySqlConnection connection)
        {
            // Sql Befehl losschicken
            string sql = "SELECT * FROM Room";
            MySqlCommand command = new MySqlCommand(sql, connection);

            // Daten holen
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                //public Room(int id, string name, string preposition, string description)
                String id = reader[0].ToString();
                String name = reader[1].ToString();
                String preposition = reader[2].ToString();
                String description = reader[3].ToString();

                // hier in der Praxis überprüfen ob id eine Zahl ist
                // und ggf. eine Fehlermeldung erzeugen
                AddRoom(new Room(Int32.Parse(id), name, preposition, description));
            }
            reader.Close();

        }

        /* läd alle Türen aus Datenbank
         */
        private void LoadDoor(MySqlConnection connection)
        {
            // Sql Befehl losschicken
            string sql = "SELECT * FROM Door";
            MySqlCommand command = new MySqlCommand(sql, connection);

            // Daten holen
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                //  Door(int id, string name, bool open, bool secret, Room room1, Room room2)
                String idStr = reader[0].ToString();
                String nameStr = reader[1].ToString();
                String openTextStr = reader[2].ToString();
                String openStr = reader[3].ToString();
                String secretStr = reader[4].ToString();
                String room1FkStr = reader[5].ToString();
                String room2FkStr = reader[6].ToString();

                // wenn Räume angeben wurden
                if (room1FkStr != "" && room2FkStr != "")
                {

                    // Räume anhand der übergebenen ID suchen
                    Room room1 = GetRoom(int.Parse(room1FkStr));
                    Room room2 = GetRoom(int.Parse(room2FkStr));

                    // Umwandeln der übergebenen Werte von "open" und "secret" in boolean
                    // 1 = true, 0 = false
                    bool open = true;
                    if (openStr == "0")
                    {
                        open = false;
                    }

                    bool secret = false;
                    if (secretStr == "1")
                    {
                        secret = true;
                    }
                    // neue Tür erzeugen und hinzufügen
                    //  Door(int id, string name, bool open, bool secret, Room room1, Room room2)
                    Door newDoor = new Door(int.Parse(idStr), nameStr, openTextStr, open, secret, room1, room2);
                    AddDoor(newDoor);
                }
            }
            reader.Close();
        }

        /* läd alle LootItems aus Datenbank
         */
        private void LoadLootItem(MySqlConnection connection)
        {
            // Sql Befehl losschicken
            string sql = "SELECT * FROM LootItem";
            MySqlCommand command = new MySqlCommand(sql, connection);

            // Daten holen
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                //Item(int id, string name, string description, bool secret, bool fix, Room room)
                String idStr = reader[0].ToString();
                String name = reader[1].ToString();
                String description = reader[2].ToString();
                String interactText = reader[3].ToString();
                String packText = reader[4].ToString();
                String openStr = reader[5].ToString();
                String secretStr = reader[6].ToString();
                String fixStr = reader[7].ToString();
                String roomFkStr = reader[8].ToString();

                // wenn Raume angeben wurden
                if (roomFkStr != "")
                {
                    // Raum anhand der übergebenen ID suchen
                    Room room = GetRoom(int.Parse(roomFkStr));

                    // Umwandeln der übergebenen Werte von "open", "secret"  und "fix" in boolean
                    // 1 = true, 0 = false
                    bool open = true;
                    if (openStr == "0")
                    {
                        open = false;
                    }
                    bool secret = false;
                    if (secretStr == "1")
                    {
                        secret = true;
                    }
                    bool fix = false;
                    if (fixStr == "1")
                    {
                        fix = true;
                    }
                    // neues Item erzeugen und hinzufügen
                    LootItem newItem = new LootItem(Int32.Parse(idStr), name, description, interactText, packText, open, secret, fix, room);
                    AddLootItem(newItem);
                }
            }
            reader.Close();
        }

        /* läd alle DecoItems aus Datenbank
         */
        private void LoadDecoItem(MySqlConnection connection)
        {
            // Sql Befehl losschicken
            string sql = "SELECT * FROM DecoItem";
            MySqlCommand command = new MySqlCommand(sql, connection);

            // Daten holen
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                //DecoItem(int id, string name, string description, int maxNumber)
                String idStr = reader[0].ToString();
                String name = reader[1].ToString();
                String description = reader[2].ToString();
                String interactText = reader[3].ToString();
                String packText = reader[4].ToString();
                String maxNumberStr = reader[5].ToString();

                // neue Tür erzeugen und hinzufügen
                DecoItem newItem = new DecoItem(Int32.Parse(idStr), name, description, interactText, packText, Int32.Parse(maxNumberStr));
                AddDecoItem(newItem);
            }
            reader.Close();

        }

    }

}
