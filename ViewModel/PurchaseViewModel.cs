using System.Collections.ObjectModel;
using System.ComponentModel;
using ToyStore.DAL;
using ToyStore.Model;

namespace ToyStore.ViewModel
{
    public class PurchaseViewModel : INotifyPropertyChanged
    {
        private PurchaseModel _purchase { get; set; }
        private PurchaseDAO _purchaseDAO;

        private ObservableCollection<PurchaseModel> _purchases { get; set; }

        public PurchaseModel Purchase
        {
            get { return _purchase; }
            set
            {
                _purchase = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Purchase"));
            }
        }

        public ObservableCollection<PurchaseModel> Purchases
        {
            get { return _purchases; }
            set { _purchases = value; OnPropertyChanged("Purchases"); }
        }

        public PurchaseViewModel()
        {
            _purchaseDAO = new PurchaseDAO();
            _purchase = new PurchaseModel();
            ClearView();
            UpdateList();
            if (Purchase.Id == 0)
                Purchase.Active = true;
        }

        public bool Save()
        {
            bool success;
            if (_purchase.Id == 0)
                success = _purchaseDAO.Insert(_purchase);
            else
                success = _purchaseDAO.Update(_purchase);
            if (success)
            {
                UpdateList();
                ClearView();
                return true;
            }
            return false;
        }

        public void InsertCustomer(CustomerModel customer)
        {
            _purchase.Customer = customer;
        }

        public void InsertCustomer(ObservableCollection<ToyModel> toys)
        {
            _purchase.Toys = toys;
        }

        public void InsertToys(ObservableCollection<ToyModel> toys)
        {
            _purchase.Toys = toys;
        }

        private void UpdateList()
        {
            Purchases = new ObservableCollection<PurchaseModel>(_purchaseDAO.ToList());
        }

        public void Query(string search)
        {
            Purchases = new ObservableCollection<PurchaseModel>(_purchaseDAO.Query(search));
        }

        public void Select(int index)
        {
            Purchase = Purchases[index];
        }

        public void ClearView()
        {
            Purchase = new PurchaseModel();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string nomePropriedade)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nomePropriedade));
        }
    }
}