using System.Collections.ObjectModel;
using System.ComponentModel;
using ToyStore.DAL;
using ToyStore.Model;

namespace ToyStore.ViewModel
{
    class CustomerOfPurchaseViewModel : INotifyPropertyChanged
    {
        private CustomerModel _selectedCustomer { get; set; }
        private ObservableCollection<CustomerModel> _allCustomers { get; set; }
        private CustomerDAO _customerDAO;
        private PurchaseViewModel _purchaseViewModel { get; set; }

        public CustomerModel SelectedCustomer
        {
            get { return _selectedCustomer; }
            set { _selectedCustomer = value; OnPropertyChanged("SelectedCustomer"); }
        }

        public ObservableCollection<CustomerModel> AllCustomers
        {
            get { return _allCustomers; }
            set { _allCustomers = value; OnPropertyChanged("AllCustomers"); }
        }

        public CustomerOfPurchaseViewModel(PurchaseViewModel purchaseViewModel)
        {
            _purchaseViewModel = purchaseViewModel;
            _customerDAO = new CustomerDAO();
            SelectedCustomer = new CustomerModel();
            UpdateAllToysList();
        }

        public void InsertCustomer()
        {
            _purchaseViewModel.InsertCustomer(SelectedCustomer);
        }

        public void Select(int index)
        {
            SelectedCustomer = AllCustomers[index];
        }

        public void Query(string busca)
        {
            AllCustomers = new ObservableCollection<CustomerModel>(_customerDAO.Query(busca));
        }

        private void UpdateAllToysList()
        {
            AllCustomers = new ObservableCollection<CustomerModel>(_customerDAO.ToList());
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string nomePropriedade)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nomePropriedade));
        }
    }
}
