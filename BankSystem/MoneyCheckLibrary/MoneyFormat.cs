﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Collections.ObjectModel;
using System.Windows.Input;
using BankSystem;

namespace MoneyCheckLibrary
{
    public class MoneyFormat
    {
        public static bool Flag { get; set; } = false;
        public static bool CheckFormatMoney(string sum)
        {
            try
            {
                Flag = CheckMoney(sum);
            }
            catch (FormatException error)
            {
                MessageBox.Show($"Некорректно указана сумма");
            }
            catch (TransferException error)
            {
                MessageBox.Show($"{error.Message}");
            }
            catch (OverflowException error)
            {
                MessageBox.Show($"Сумма слишком большая");
            }
            return Flag;

        }
        public static bool CheckMoney(string sumToAdd)
        {
            if (Convert.ToInt32(sumToAdd) <= 0)
            {
                throw new TransferException();
            }
            else return true;
        }
    }
}
