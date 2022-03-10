using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem
{
    internal class Organisation : Client
    {
        //public string OrgName { get; set; }
        public Guid ClientId { get; set; }
        public new string Name { get {return OrgName; } }
        public new string OrgName { get; set; }
        public new bool IsPerson { get { return false; } }
        public string OGRN { get; set; }
        public List<Account> ClientAccounts { get; set; }

        public Organisation()
        {
            
        }

        public Organisation(string OrgName, string OGRN, List<Account>  ClientAccounts)
        {
            this.OrgName = OrgName;
            this.OGRN = OGRN;
            this.ClientAccounts = ClientAccounts;
            this.ClientId = Guid.NewGuid();
        }

        public Organisation(string OrgName, string OGRN, List<Account> ClientAccounts, Guid ClientId)
        {
            this.OrgName = OrgName;
            this.OGRN = OGRN;
            this.ClientAccounts = ClientAccounts;
            this.ClientId = ClientId;
        }
    }
}
