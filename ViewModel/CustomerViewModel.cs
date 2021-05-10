using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using ToyStore.DAL;
using ToyStore.Model;

namespace ToyStore.ViewModel
{
    class CustomerViewModel : INotifyPropertyChanged
    {
        private CustomerModel _customer { get; set; }
        private CustomerDAO _customerDAO;

        private ObservableCollection<CustomerModel> _customers { get; set; }

        public CustomerModel Customer
        {
            get { return _customer; }
            set { _customer = value; OnPropertyChanged("Customer"); }
        }

        public ObservableCollection<CustomerModel> Customers
        {
            get { return _customers; }
            set { _customers = value; OnPropertyChanged("Customers"); }
        }

        public CustomerViewModel()
        {
            _customerDAO = new CustomerDAO();
            _customer = new CustomerModel();
            ClearView();
            UpdateList();
        }

        public bool Save()
        {
            bool success;
            if (_customer.Id == 0)
                success = _customerDAO.Insert(_customer);
            else
                success = _customerDAO.Update(_customer);
            if (success)
            {
                UpdateList();
                ClearView();
                return true;
            }
            return false;
        }

        private void UpdateList()
        {
            Customers = new ObservableCollection<CustomerModel>(_customerDAO.ToList());
        }

        public void Query(string search)
        {
            Customers = new ObservableCollection<CustomerModel>(_customerDAO.Query(search));
        }

        public void Select(int index)
        {
            Customer = Customers[index];
        }

        public void ClearView()
        {
            Customer = new CustomerModel { Birthday = DateTime.Today }; ;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string nomePropriedade)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nomePropriedade));
        }
    }
}
