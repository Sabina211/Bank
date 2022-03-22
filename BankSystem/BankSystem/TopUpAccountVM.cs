using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using MoneyCheckLibrary;

namespace BankSystem
{
    class TopUpAccountVM : WindowTemplateVM
    {
        public string SumToAdd { set; get; }
        public TopUpAccount window;
        public ICommand SaveCommand { get; }

        public TopUpAccountVM(MainWindowVM mainWindowVM)
        {
            SaveCommand = new RelayCommand(obj =>
            {
                if (SumToAdd == null || SelectedAccount== null) { ErrorEnable = "Visible"; return; }
                if (SelectedAccount.IsActive==false)
                {
                    MessageBox.Show("Невозможно пополнить закрытый счет");
                    return;
                }
                window = Application.Current.Windows.OfType<TopUpAccount>().SingleOrDefault(w => w.IsActive);
                bool flag = MoneyFormat.CheckFormatMoney(SumToAdd);

                if (flag)
                {
                    int sum = Convert.ToInt32(SumToAdd);
                    double newBalance = SelectedAccount.Balance + sum;
                    List<Client> allClients = Json.DeserializedCustomersJson();

                    int currentClientFindInd = allClients.FindIndex(x => x.ClientId.Equals(selectedClient.ClientId));
                    int currentAccountFindInd = allClients[currentClientFindInd].ClientAccounts.FindIndex(x => x.AccountId.Equals(SelectedAccount.AccountId));

                    allClients[currentClientFindInd].ClientAccounts[currentAccountFindInd].Balance = newBalance;
                    Json.SerializeJson(allClients);
                    Logs.AccountEvent += () => { MessageBox.Show($"Счет пополнен на {SumToAdd} руб. "); };
                    Logs.SaveLog(SelectedAccount, SumToAdd, "Пополнение");
                    UpdateClients(mainWindowVM);
                    window.Close();
                }

            });

            Clients = new ObservableCollection<Client>();
            UpdateClients(mainWindowVM);
        }
    }
}
