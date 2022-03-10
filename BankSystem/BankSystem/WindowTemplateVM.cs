using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem
{
    class WindowTemplateVM : Bindable
    {
        public Client selectedClient;
        public Client SelectedClient
        {
            get
            {
                if (selectedClient != null)
                {
                    List<Client> allClients = Json.DeserializedCustomersJson();
                    Client currentClient = new Client();
                    currentClient = allClients.Find(x => x.ClientId.Equals(selectedClient.ClientId));
                    if (currentClient != null)
                    {
                        if (selectedClient.IsPerson == true)
                        {
                            Person person = new Person(
                                currentClient.FirstName,
                                currentClient.Surname,
                                currentClient.Patronymic,
                                currentClient.ClientAccounts,
                                currentClient.ClientId);
                            return person;
                        }
                        else
                        {
                            Organisation organisation = new Organisation(
                                currentClient.OrgName,
                                currentClient.OGRN,
                                currentClient.ClientAccounts,
                                currentClient.ClientId);
                            return organisation;
                        }
                    }
                    else return selectedClient;
                }
                else return selectedClient;
            }
            set
            {
                selectedClient = value;

                if (selectedClient != null)
                {
                    if (selectedClient.Name != "--Добавить нового клиента--")
                    {
                        if (selectedClient.IsPerson == true) ClientKindIndex = 0; // ФЛ
                        else ClientKindIndex = 1; //Орг
                    }
                    else ClientKindIndex = -1; //Не выбрано значение
                }
                else ClientKindIndex = -1;

                OnPropertyChanged("SelectedClient");
                OnPropertyChanged("ClientKindIndex");
                OnPropertyChanged("OrgEnable");
                OnPropertyChanged("PersonEnable");
                OnPropertyChanged("ClientAccounts");
            }
        }
        private Account selectedAccount;
        public Account SelectedAccount
        {
            set { selectedAccount = value; OnPropertyChanged("SelectedAccount"); }
            get { return selectedAccount; }
        }
        private ObservableCollection<Client> сlients;
        public ObservableCollection<Client> Clients
        {
            get { return сlients; }
            set
            {
                сlients = value;
                OnPropertyChanged("Clients");
            }
        }

        public List<string> ClientK { get; set; } = new List<string> { "Физическое лицо", "Организация" };
        private int clientKindIndex = -1;
        public int ClientKindIndex
        {
            get
            {
                return clientKindIndex;
            }
            set
            {
                clientKindIndex = value;
                OnPropertyChanged("ClientKindIndex");
                OnPropertyChanged("OrgEnable");
                OnPropertyChanged("PersonEnable");
            }
        }
        public bool OrgEnable
        {
            get
            {
                if (selectedClient != null)
                {
                    if (selectedClient.Name != "--Добавить нового клиента--") return false; //если клиент выбран, то поле заблокировано
                    else
                    {
                        if (ClientKindIndex == 0) return false;
                        if (ClientKindIndex == -1) return false;
                        else return true;
                    }
                }
                else return false;
            }
            set
            {
                OnPropertyChanged("OrgEnable");
            }
        }
        public bool PersonEnable
        {
            get
            {
                if (selectedClient != null)
                {
                    if (selectedClient.Name != "--Добавить нового клиента--") return false; //если клиент выбран, то поле заблокировано
                    else
                    {
                        if (ClientKindIndex == 0) return true;
                        else return false;
                    }
                }
                else return false;
            }
            set
            {
                OnPropertyChanged("PersonEnable");
            }
        }
        private string accountNumber;
        public string AccountNumber
        {
            get
            {
                return accountNumber;
            }
            set
            {
                accountNumber = value;
                OnPropertyChanged("AccountNumber");
            }
        }
        private string errorEnable = "Hidden";
        public string ErrorEnable
        {
            get
            {
                return errorEnable;
            }
            set
            {
                errorEnable = value;
                OnPropertyChanged("ErrorEnable");
            }
        }

        public void UpdateClients(MainWindowVM mainWindowVM)
        {
            List<Client> allCustomers = Json.DeserializedCustomersJson();
            for (int i = 0; i < allCustomers.Count; i++)
            {
                Clients.Add(new Client
                (
                    allCustomers[i].Name,
                    allCustomers[i].IsPerson,
                    allCustomers[i].ClientAccounts,
                    allCustomers[i].ClientId
                ));
            }
            mainWindowVM.UpdateClients();
        }
    }
}
