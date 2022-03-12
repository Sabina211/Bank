using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem
{
    class DepositAccount : Account
    {
        //test1
        public new bool IsDeposit { get; set; } = true;
        public new int DepositTerm { get; set; }
        private double depositRate;
        public double DepositRate
        {
            get => depositRate;
            set
            {
                if (DepositTerm == 12) depositRate = 5.0;
                if (DepositTerm == 6) depositRate = 4.0;
                if (DepositTerm == 3) depositRate = 3.0;
            }
        }


        public DepositAccount(string Number, double Balance)
        {
            this.AccountId = Guid.NewGuid();
            this.Number = Number;
            this.Balance = Balance;
        }

        public DepositAccount(string Number, double Balance, int DepositTerm)
        {
            this.AccountId = Guid.NewGuid();
            this.Number = Number;
            this.Balance = Balance;
            this.DepositTerm = DepositTerm;
            this.DepositRate = DepositRate;
        }
        public DepositAccount()
        {

        }
    }
}
