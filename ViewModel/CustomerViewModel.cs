using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using ToyStore.DAL;
using ToyStore.Model;

namespace ToyStore.ViewModel
{
    public class CustomerViewModel : INotifyPropertyChanged
    {
        private CustomerModel _customer { get; set; }
        private CustomerDAO _customerDAO;

        private ObservableCollection<CustomerModel> _customers { get; set; }

        public CustomerModel Customer
        {
            get { return _customer; }
            set 
            { 
                _customer = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Customer"));
            }
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
            if (Customer.Id == 0)
                Customer.Active = true;
        }

        public bool Save(string cpf)
        {
            bool success;
            _customer.CPF = cpf;
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
            Customer = new CustomerModel { Birthday = DateTime.Today };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string nomePropriedade)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nomePropriedade));
        }
    }
}
