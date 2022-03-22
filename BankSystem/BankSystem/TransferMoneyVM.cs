using MoneyCheckLibrary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace BankSystem
{
    class TransferMoneyVM : WindowTemplateVM
    {
        public TransferMoney window;
        //получатель
        private Client selectedRecipient;
        public Client SelectedRecipient
        {
            get
            {
                return selectedRecipient;
            }
            set
            {
                selectedRecipient = value;
                OnPropertyChanged("SelectedRecipient");
            }
        }
        private Account selectedRecipientAccount;
        public Account SelectedRecipientAccount
        {
            set { selectedRecipientAccount = value; OnPropertyChanged("SelectedRecipientAccount"); }
            get { return selectedRecipientAccount; }
        }

        public string TransferSum { get; set; }
        public ICommand TransferCommand { get; }


        public TransferMoneyVM(MainWindowVM mainWindowVM)
        {
            TransferCommand = new RelayCommand(obj =>
            {
                if (SelectedAccount == null || SelectedRecipientAccount == null) { ErrorEnable = "Visible"; return; }
                window = Application.Current.Windows.OfType<TransferMoney>().SingleOrDefault(w => w.IsActive);
                List<Client> allClients = Json.DeserializedCustomersJson();

                //отправитель
                int currentClientFindInd = allClients.FindIndex(x => x.ClientId.Equals(selectedClient.ClientId));
                int currentAccountFindInd = allClients[currentClientFindInd].ClientAccounts.FindIndex(x => x.AccountId.Equals(SelectedAccount.AccountId));

                //получатель
                int recipientClientFindInd = allClients.FindIndex(x => x.ClientId.Equals(SelectedRecipient.ClientId));
                int recipientAccountFindInd = allClients[recipientClientFindInd].ClientAccounts.FindIndex(x => x.AccountId.Equals(SelectedRecipientAccount.AccountId));


                if (SelectedAccount.IsActive == false || SelectedRecipientAccount.IsActive == false)
                {
                    MessageBox.Show("Нельзя при переводе использовать закрытые счета"); return;
                }
                bool flag = MoneyFormat.CheckFormatMoney(TransferSum);

                if (flag)
                {

                    if (SelectedAccount.Balance >= Convert.ToDouble(TransferSum))
                    {
                        allClients[currentClientFindInd].ClientAccounts[currentAccountFindInd].Balance = allClients[currentClientFindInd].ClientAccounts[currentAccountFindInd].Balance - Convert.ToDouble(TransferSum);
                        allClients[recipientClientFindInd].ClientAccounts[recipientAccountFindInd].Balance = allClients[recipientClientFindInd].ClientAccounts[recipientAccountFindInd].Balance + Convert.ToDouble(TransferSum);
                    }
                    else { MessageBox.Show("Недостаточно средств"); return; }

                    Json.SerializeJson(allClients);
                    Logs.AccountEvent += () => { MessageBox.Show($"Перевод {TransferSum} руб. успешно выполнен"); };
                    Logs.SaveLog(SelectedAccount, SelectedRecipientAccount, TransferSum.ToString(), "Перевод денег");
                    UpdateClients(mainWindowVM);
                    window.Close();
                }

            });

            Clients = new ObservableCollection<Client>();
            UpdateClients(mainWindowVM);
        }
    }
}
