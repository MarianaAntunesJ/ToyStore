using System.Collections.ObjectModel;
using System.ComponentModel;
using ToyStore.DAL;
using ToyStore.Model;

namespace ToyStore.ViewModel
{
    class ToysOfPurchaseViewModel : INotifyPropertyChanged
    {

        private ObservableCollection<ToyModel> _selectedToys { get; set; }
        private ObservableCollection<ToyModel> _allToys { get; set; }
        private ToyDAO _toyDAO;
        private PurchaseViewModel _purchaseViewModel { get; set; }

        public ObservableCollection<ToyModel> SelectedToys
        {
            get { return _selectedToys; }
            set { _selectedToys = value; OnPropertyChanged("SelectedToys"); }
        }

        public ObservableCollection<ToyModel> AllToys
        {
            get { return _allToys; }
            set { _allToys = value; OnPropertyChanged("AllToys"); }
        }

        public ToysOfPurchaseViewModel(PurchaseViewModel purchaseViewModel)
        {
            _purchaseViewModel = purchaseViewModel;
            _toyDAO = new ToyDAO();
            SelectedToys = new ObservableCollection<ToyModel>();
            UpdateAllToysList();
        }

        public void InsertToys()
        {
            _purchaseViewModel.InsertCustomer(SelectedToys);
        }

        public void Select(int index)
        {
            SelectedToys.Add(AllToys[index]);
        }

        public void Remover(int index)
        {
            if (index >= 0)
                SelectedToys.Remove(AllToys[index]);
        }

        public void Query(string search)
        {
            AllToys = new ObservableCollection<ToyModel>(_toyDAO.Query(search));
        }

        private void UpdateAllToysList()
        {
            AllToys = new ObservableCollection<ToyModel>(_toyDAO.ToList());
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string nomePropriedade)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nomePropriedade));
        }

    }
}
