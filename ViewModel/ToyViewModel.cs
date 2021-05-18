using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToyStore.DAL;
using ToyStore.Model;

namespace ToyStore.ViewModel
{
    class ToyViewModel : INotifyPropertyChanged
    {
        private ToyModel _toy { get; set; }
        private ToyDAO _toyDAO;

        private ObservableCollection<ToyModel> _toys { get; set; }

        public ToyModel Toy
        {
            get { return _toy; }
            set
            {
                _toy = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Toy"));
            }
        }

        public ObservableCollection<ToyModel> Toys
        {
            get { return _toys; }
            set { _toys = value; OnPropertyChanged("Toys"); }
        }

        public ToyViewModel()
        {
            _toyDAO = new ToyDAO();
            _toy = new ToyModel();
            ClearView();
            UpdateList();
            if (Toy.Id == 0)
                Toy.Active = true;
        }

        public bool Save()
        {
            bool success;
            if (_toy.Id == 0)
                success = _toyDAO.Insert(_toy);
            else
                success = _toyDAO.Update(_toy);
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
            Toys = new ObservableCollection<ToyModel>(_toyDAO.ToList());
        }

        public void Query(string search)
        {
            Toys = new ObservableCollection<ToyModel>(_toyDAO.Query(search));
        }

        public void Select(int index)
        {
            Toy = Toys[index];
        }

        public void ClearView()
        {
            Toy = new ToyModel();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string nomePropriedade)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nomePropriedade));
        }
    }
}

