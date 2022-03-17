using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem
{
    class Logs
    {
        public static event Action AccountEvent;
        public string DateAndTime { get; set; }
        public string AutorAccountNumber { get; set; }
        public Guid AutorAccountId { get; set; }
        public string RecipientAccountNumber { get; set; }
        public string Sum { get; set; }
        public string EditKind { get; set; }


        public Logs(string DateAndTime,  string AutorAccountNumber, Guid AutorAccountId, string Recipient, string Sum, string EditKind)
        {
            this.DateAndTime = DateAndTime;
            this.AutorAccountNumber = AutorAccountNumber;
            this.AutorAccountId = AutorAccountId;
            this.RecipientAccountNumber = Recipient;
            this.Sum = Sum;
            this.EditKind = EditKind;
        }

        public Logs() { }
        
        //открытие
        public static void SaveLog(string editKind)
        {
            Logs editLog = new Logs(
                DateTime.Now.ToString(),
                null,
                Guid.Empty,
                null,
                null,
                editKind
                );
            List<Logs> allLogs = new List<Logs>();
            allLogs = DeserializedELogsJson();
            allLogs.Add(editLog);
            SerializeJson(allLogs);
           // AccountEvent?.Invoke();
        }

        //закрытие
        public static void SaveLog(Account AutorAccount, string editKind)
        {
            Logs editLog = new Logs(
                DateTime.Now.ToString(),
                AutorAccount.Number,
                AutorAccount.AccountId,
                null,
                "0",
                editKind
                );
            List<Logs> allLogs = new List<Logs>();
            allLogs = DeserializedELogsJson();
            allLogs.Add(editLog);
            SerializeJson(allLogs);
            //AccountEvent?.Invoke();
        }

        //пополнение
        public static void SaveLog(Account Recipient, string Sum, string editKind)
        {
            Logs editLog = new Logs(
                DateTime.Now.ToString(),
                null,
                Guid.Empty,
                Recipient.Number,
                Sum,
                editKind
                );
            List<Logs> allLogs = new List<Logs>();
            allLogs = DeserializedELogsJson();
            allLogs.Add(editLog);
            SerializeJson(allLogs);
            //AccountEvent?.Invoke();
        }

        //перевод
        public static void SaveLog(Account currentAccount, Account Recipient, string Sum, string editKind)
        {
            Logs editLog = new Logs(
                DateTime.Now.ToString(),
                currentAccount.Number,
                currentAccount.AccountId,
                Recipient.Number,
                Sum,
                editKind
                );
            List<Logs> allLogs = new List<Logs>();
            allLogs = DeserializedELogsJson();
            allLogs.Add(editLog);
            SerializeJson(allLogs);
            //AccountEvent?.Invoke();
           // AccountEvent = null;
        }

        public static List<Logs> DeserializedELogsJson()
        {
            List<Logs> logs = new List<Logs>();
            bool exist = File.Exists("Storage\\editLog.json");
            if (exist)
            {
                string text = File.ReadAllText("Storage\\editLog.json");
                logs = JsonConvert.DeserializeObject<List<Logs>>(text);
            }
            return logs;
        }

        public static void SerializeJson(List<Logs> logs)
        {
            string json = JsonConvert.SerializeObject(logs);
            File.WriteAllText("Storage\\editLog.json", json);
            AccountEvent?.Invoke();
            AccountEvent = null;
        }

        public static string ReturnTextLogs()
        {
            string text = File.ReadAllText("Storage\\editLog.json");
            string text2 = text.Replace("},{", "\n \n");
            return text2;
        }

    }
}
