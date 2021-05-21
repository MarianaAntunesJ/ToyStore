using System.Collections.ObjectModel;

namespace ToyStore.Model
{
    public class PurchaseModel
    {
        public int Id { get; set; }
        public EmployeeModel Employee { get; set; }
        public CustomerModel Customer { get; set; }
        public ObservableCollection<ToyModel> Toys { get; set; }
        public int Quantity { get; set; }
        public double Total { get; set; }
        public bool Active { get; set; }

        public PurchaseModel()
        {
        }

        public PurchaseModel(int id, EmployeeModel employee, CustomerModel customer, int quantity, double total, bool active)
        {
            Id = id;
            Employee = employee;
            Customer = customer;
            Quantity = quantity;
            Total = total;
            Active = active;
        }

        public PurchaseModel(int id, EmployeeModel employee, CustomerModel customer, ObservableCollection<ToyModel> toy, int quantity, double total, bool active)
        {
            Id = id;
            Employee = employee;
            Customer = customer;
            Toys = toy;
            Quantity = quantity;
            Total = total;
            Active = active;
        }
    }
}
