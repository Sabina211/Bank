using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem
{
    class Client
    {
        public Guid ClientId { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string OrgName { get; set; }
        public string OGRN { get; set; }
        public string Name { get; set; }
        private bool isPerson;
        public bool IsPerson
        {
            get => isPerson;
            set
            {
                isPerson = value;
                if (IsPerson == true) { clientKind = "ФЛ"; }
                else { clientKind = "Организация"; }
            }
        }
        private string clientKind;
        public string ClientKind
        {
            get => clientKind;
            set 
            { 
                if(IsPerson == true) clientKind = "ФЛ";
                else clientKind =  "Организация";
            }
        }
        public List<Account> ClientAccounts { get; set; }

        public Client(string Name, bool IsPerson, List<Account> ClientAccounts, Guid Id)
        {
            this.Name = Name;
            this.IsPerson = IsPerson;
            this.ClientAccounts = ClientAccounts;
            this.ClientId = Id;
        }

        public Client(string Name, bool IsPerson, List<Account> ClientAccounts)
        {
            this.Name = Name;
            this.IsPerson = IsPerson;
            this.ClientAccounts = ClientAccounts;
            this.ClientId = Guid.NewGuid();
        }

        public Client()
        {
        }
    }
}
