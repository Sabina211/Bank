using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace BankSystem
{
    class CloseAccountVM : WindowTemplateVM
    {
        public CloseAccount window;
        public ICommand CloseCommand { get; }

        public CloseAccountVM(MainWindowVM mainWindowVM)
        {
            CloseCommand = new RelayCommand(obj =>
            {
                if (SelectedAccount == null) { ErrorEnable = "Visible"; return; }
                window = Application.Current.Windows.OfType<CloseAccount>().SingleOrDefault(w => w.IsActive);

                List<Client> allClients = Json.DeserializedCustomersJson();

                int currentClientFindInd = allClients.FindIndex(x => x.ClientId.Equals(selectedClient.ClientId));
                int currentAccountFindInd = allClients[currentClientFindInd].ClientAccounts.FindIndex(x => x.AccountId.Equals(SelectedAccount.AccountId));
                List<Client> updateClients = new List<Client>();

                if (SelectedAccount.IsActive == false)
                {
                    MessageBox.Show("Счет уже закрыт"); return;
                }

                if (SelectedAccount.Balance == 0)
                {
                    allClients[currentClientFindInd].ClientAccounts[currentAccountFindInd].IsActive = false;
                }
                else { MessageBox.Show("Перед закрытием счета оставшиеся денежные средства необходимо перевести на другой счет");  return; }
                  
                Json.SerializeJson(allClients);
                UpdateClients(mainWindowVM);
                window.Close();
            });

            Clients = new ObservableCollection<Client>();
            UpdateClients(mainWindowVM);
        }
    }
}
