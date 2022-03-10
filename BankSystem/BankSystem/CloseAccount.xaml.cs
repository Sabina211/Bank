using System;
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
    /// Логика взаимодействия для CloseAccount.xaml
    /// </summary>
    public partial class CloseAccount : Window
    {
        internal CloseAccount(MainWindowVM mainWindowVM)
        {
            InitializeComponent();
            DataContext = new CloseAccountVM(mainWindowVM);
        }
    }
}
