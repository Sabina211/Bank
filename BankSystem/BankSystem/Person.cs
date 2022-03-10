using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem
{
    class Person : Client
    {
        public new Guid ClientId { get; set; }
        public new string FirstName { get; set; }
        public new string Surname { get; set; }
        public new string Patronymic { get; set; }
        public new string Name { get { return $"{FirstName} {Surname} {Patronymic}"; } }
        public new bool IsPerson { get { return true; } }
        public new List<Account> ClientAccounts { get; set; }
        public Person()
        { }

        public Person(string FirstName, string Surname, string Patronymic, List<Account> ClientAccounts)
        {
            this.FirstName = FirstName;
            this.Surname = Surname;
            this.Patronymic = Patronymic;
            this.ClientAccounts = ClientAccounts;
            this.ClientId = Guid.NewGuid();
        }
        public Person(string FirstName, string Surname, string Patronymic, List<Account> ClientAccounts, Guid ClientId)
        {
            this.FirstName = FirstName;
            this.Surname = Surname;
            this.Patronymic = Patronymic;
            this.ClientAccounts = ClientAccounts;
            this.ClientId = ClientId;
        }
    }
}
