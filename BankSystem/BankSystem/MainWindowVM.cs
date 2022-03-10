using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BankSystem
{
    internal class MainWindowVM : Bindable
    {
        //коммент
        private Client selectedClient;
        public Client SelectedClient
        {
            get { return selectedClient; }
            set 
            {
                selectedClient = value;
                OnPropertyChanged("SelectedClient");
            }
        }
        public ObservableCollection<Client> сlients;
        public ObservableCollection<Client> Clients
        {
            get {return сlients; }
            set
            {
                сlients = value;
                OnPropertyChanged("Clients");
            }
        }
        public ICommand AddAccountCommand { get; }
        public ICommand TopUpAccountCommand { get; }
        public ICommand CloseAccountCommand { get; }
        public ICommand TransferMoneyCommand { get; }

        public MainWindowVM()
        {
            Clients = new ObservableCollection<Client>();
            AddAccountCommand = new RelayCommand(obj =>
            {
                AddAccount addWindow = new AddAccount(this);
                addWindow.Show();
            });

            TopUpAccountCommand = new RelayCommand(obj =>
            {
                TopUpAccount topUpWindow = new TopUpAccount(this);
                topUpWindow.Show();
            });

            CloseAccountCommand = new RelayCommand(obj =>
            {
                CloseAccount closeWindow = new CloseAccount(this);
                closeWindow.Show();
            });

            TransferMoneyCommand = new RelayCommand(obj =>
            {
                TransferMoney transferWindow = new TransferMoney(this);
                transferWindow.Show();
            });


            /*Account oneA = new Account(111, 1000, true);
            Account twoA = new Account(12345678, 25000, false);
            Client tree = new Organisation("Организация Карла", "123456789", new List<Account> {  twoA });
            Client four = new Person("Игнатов", "Марк", "Кириллович", new List<Account> { oneA, twoA });
            List<Client> newClients = new List<Client>();
            newClients.Add(tree);
            newClients.Add(four);
            Json.SerializeJson(newClients);*/
            UpdateClients();
        }

        public void UpdateClients()
        {
            List<Client> allCustomers = Json.DeserializedCustomersJson();
            Clients.Clear();
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
        }
    }
}
