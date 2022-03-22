using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoneyCheckLibrary;

namespace BankSystem
{
    public class TransferException : Exception
    {
        public new string Message { get; set; } = "Сумма должна быть больше нуля";
        public TransferException()
        {
        }
    }
}
