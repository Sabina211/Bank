using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem
{
    class CurrentAccount : Account
    {
<<<<<<< HEAD
        //local
=======
        //server
>>>>>>> 6a5c9a20f91e4e2fa25741e6b7013aac936c8868
        public new bool IsDeposit { get; set; } = false;

        public CurrentAccount(string Number, double Balance)
        {
            this.Number = Number;
            this.Balance = Balance;
            this.AccountId = Guid.NewGuid();
        }

        public CurrentAccount()
        {

        }
    }
}
