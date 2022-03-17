using System;

namespace BankSystem
{
    public class Account<T> where T : Account<T>
    {
        public T Property { get; set; }
        public Account(T Value)
        {
            Property = Value;
        }
        public Account()
        {

        }
    }

    public class Account
    {
       
        public Guid AccountId { get; set; }
        public string Number { get; set; }
        public double Balance { get; set; }
        private bool isDeposit;
        public bool IsDeposit
        {
            get => isDeposit;
            set=> isDeposit = value;
        }
        public int DepositTerm { get; set; }

        public string AccountName { 
            get { 
                if(IsActive==false) return $"{Number} (счет закрыт)";
                else return $"{Number} {Balance} руб."; 
            }
        }
        public string FullNumber
        {
            get
            {
                if (IsActive == false) return $"🔒 {Number} (счет закрыт)";
                else return $"{Number}";
            }
        }

        public bool IsActive { get; set; } = true;

        public Account(string Number, double Balance, bool IsDeposit)
        {
            this.Number = Number;
            this.Balance = Balance;
            this.AccountId = Guid.NewGuid();
        }
        public Account()
        {
        }
    }
}
