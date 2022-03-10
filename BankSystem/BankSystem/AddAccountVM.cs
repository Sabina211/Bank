using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace BankSystem
{
    internal class AddAccountVM : WindowTemplateVM
    {
        public AddAccount window;
        public List<string> AccountType { get; set; } = new List<string> { "Депозитный счет", "Текущий счет" };
        public List<string> DepositTerms { get; set; } = new List<string> { "3 мес", "6 мес", "1 год" };
        private int accountTypeIndex = 0;
        public int AccountTypeIndex
        {
            get => accountTypeIndex;
            set
            {
                accountTypeIndex = value;
                OnPropertyChanged("AccountTypeIndex");
                if (accountTypeIndex == 1) DepositTermIsEnable = false;
                else DepositTermIsEnable = true;
            }
        }
        private int depositTermIndex = -1;
        public int DepositTermIndex
        {
            get => depositTermIndex;
            set
            {
                depositTermIndex = value;
                OnPropertyChanged("DepositTermIndex");

            }
        }
        public int DepositTermValue
        {
            get
            {
                if (DepositTermIndex == 0) return 3;
                if (DepositTermIndex == 1) return 6;
                if (DepositTermIndex == 2) return 12;
                else return 0;

            }
        }
        private bool depositTermIsEnable = true; // по умолчанию активное поле
        public bool DepositTermIsEnable
        {
            get => depositTermIsEnable;
            set
            {
                depositTermIsEnable = value;
                OnPropertyChanged("DepositTermIsEnable");
            }
        }

        public ICommand SaveCommand { get; }

        public AddAccountVM(MainWindowVM mainWindowVM)
        {
            SaveCommand = new RelayCommand(obj =>
            {
                if (ClientKindIndex == -1 || AccountNumber == "" || AccountNumber == null) { ErrorEnable = "Visible"; return; }
                window = Application.Current.Windows.OfType<AddAccount>().SingleOrDefault(w => w.IsActive);
                List<Client> allClients;
                if (AccountTypeIndex == 0)
                {
                    if (DepositTermIndex == -1) { ErrorEnable = "Visible"; return; }
                    else 
                    {
                        ErrorEnable = "Hidden"; 
                        DepositAccount newAccount = new DepositAccount(AccountNumber, 0, DepositTermValue);
                        allClients = AddMetod(newAccount);
                    }
                }
                else
                {
                    CurrentAccount newAccount = new CurrentAccount(AccountNumber, 0);
                    allClients = AddMetod(newAccount);
                }

                ErrorEnable = "Hidden";
                Json.SerializeJson(allClients);
                UpdateClients(mainWindowVM);
                window.Close();
            });


            Clients = new ObservableCollection<Client>();
            Clients.Add(new Client("--Добавить нового клиента--", false, null));
            UpdateClients(mainWindowVM);
        }

        private List<Client> AddMetod(BankSystem.Account newAccount)
        {
            Client currentClient = new Client();
            if (selectedClient.Name != "--Добавить нового клиента--")
            {
                if (ClientKindIndex == 0)
                {
                    selectedClient.ClientAccounts.Add(newAccount);
                    currentClient = new Person(selectedClient.FirstName, selectedClient.Surname, selectedClient.Patronymic, selectedClient.ClientAccounts, selectedClient.ClientId);
                }
                if (ClientKindIndex == 1)
                {
                    selectedClient.ClientAccounts.Add(newAccount);
                    currentClient = new Organisation(selectedClient.OrgName, selectedClient.OGRN, selectedClient.ClientAccounts, selectedClient.ClientId);
                }
            }
            else
            {
                if (ClientKindIndex == 0)
                {
                    currentClient = new Person(window.FirstName.Text, window.Surname.Text, window.Patronymic.Text, new List<Account> { newAccount });
                }
                if (ClientKindIndex == 1)
                {
                    currentClient = new Organisation(window.OrgName.Text, window.OGRN.Text, new List<Account> { newAccount });
                }
            }
            //запись в джейсон
            List<Client> allClients = Json.DeserializedCustomersJson();
            Client currentClientFind = allClients.Find(x => x.ClientId.Equals(selectedClient.ClientId));
            if (currentClientFind == null) allClients.Add(currentClient);
            else
            {
                for (int i = 0; i < allClients.Count; i++)
                {
                    if (selectedClient.ClientId == allClients[i].ClientId)
                    {
                        allClients[i].ClientAccounts.Add(newAccount);
                    }
                    Clients.Add(new Client
                    (
                        allClients[i].Name,
                        allClients[i].IsPerson,
                        allClients[i].ClientAccounts,
                        allClients[i].ClientId
                    ));
                }
            }

            return allClients;
        }
    }
}
