using Newtonsoft.Json;
using Newtonsoft;
using System.Collections.Generic;
using System.IO;



namespace BankSystem
{
    class Json
    {
        public List<Client> Clients { get; set; }

        public Json(List<Client> Clients)
        {
            this.Clients = Clients;
        }

        public static void SerializeJson(List<Client> clients)
        {
            string json = JsonConvert.SerializeObject(clients);
            File.WriteAllText("Storage\\clients.json", json);
        }

        public static List<Client> DeserializedCustomersJson()
        {
            List<Client> users = new List<Client>();
            bool exist = File.Exists("Storage\\clients.json");
            if (exist)
            {
                string text = File.ReadAllText("Storage\\clients.json");
                users = JsonConvert.DeserializeObject<List<Client>>(text);
            }
            return users;
        }

    }
}
