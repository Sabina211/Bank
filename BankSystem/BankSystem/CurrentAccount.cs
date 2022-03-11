using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem
{
    class CurrentAccount : Account
    {
        //local
        public new bool IsDeposit { get; set; } = false;

        public CurrentAccount(string Number, double Balance)
        {
        //rtyrty
            this.Number = Number;
            this.Balance = Balance;
            this.AccountId = Guid.NewGuid();
        }

        public CurrentAccount()
        {

        }
    }
}
