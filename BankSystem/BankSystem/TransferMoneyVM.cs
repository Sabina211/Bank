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

        public double TransferSum { get; set; }
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

                if (SelectedAccount.Balance >= TransferSum)
                {
                    allClients[currentClientFindInd].ClientAccounts[currentAccountFindInd].Balance = allClients[currentClientFindInd].ClientAccounts[currentAccountFindInd].Balance - TransferSum;
                    allClients[recipientClientFindInd].ClientAccounts[recipientAccountFindInd].Balance = allClients[recipientClientFindInd].ClientAccounts[recipientAccountFindInd].Balance + TransferSum;
                }
                else { MessageBox.Show("Недостаточно средств"); return; }

                Json.SerializeJson(allClients);
                UpdateClients(mainWindowVM);
                window.Close();
            });

            Clients = new ObservableCollection<Client>();
            UpdateClients(mainWindowVM);
        }
    }
}
