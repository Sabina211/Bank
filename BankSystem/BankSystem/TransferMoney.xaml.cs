﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BankSystem
{
    /// <summary>
    /// Логика взаимодействия для TransferMoney.xaml
    /// </summary>
    public partial class TransferMoney : Window
    {
        internal TransferMoney(MainWindowVM mainWindowVM)
        {
            InitializeComponent();
            DataContext = new TransferMoneyVM(mainWindowVM);
        }
    }
}
