using System.Collections.ObjectModel;
using System.ComponentModel;
using ToyStore.DAL;
using ToyStore.Model;

namespace ToyStore.ViewModel
{
    class EmployeeViewModel : INotifyPropertyChanged
    {
        private EmployeeModel _employee { get; set; }
        private EmployeeDAO _employeeDAO;

        private ObservableCollection<EmployeeModel> _employees { get; set; }

        public EmployeeModel Employee
        {
            get { return _employee; }
            set
            {
                _employee = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Employee"));
            }
        }

        public ObservableCollection<EmployeeModel> Employess
        {
            get { return _employees; }
            set { _employees = value; OnPropertyChanged("Employees"); }
        }

        public EmployeeViewModel()
        {
            _employeeDAO = new EmployeeDAO();
            _employee = new EmployeeModel();
            ClearView();
            UpdateList();
            if (Employee.Id == 0)
                Employee.Active = true;
        }

        public bool Save(string cpf)
        {
            bool success;
            _employee.CPF = cpf;
            if (_employee.Id == 0)
                success = _employeeDAO.Insert(_employee);
            else
                success = _employeeDAO.Update(_employee);
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
            Employess = new ObservableCollection<EmployeeModel>(_employeeDAO.ToList());
        }

        public void Query(string search)
        {
            Employess = new ObservableCollection<EmployeeModel>(_employeeDAO.Query(search));
        }

        public void Select(int index)
        {
            Employee = Employess[index];
        }

        public void ClearView()
        {
            Employee = new EmployeeModel();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string nomePropriedade)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nomePropriedade));
        }
    }
}