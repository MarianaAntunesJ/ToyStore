namespace ToyStore.Model
{
    class PurchaseModel
    {
        public int Id { get; set; }
        public EmployeeModel Employee { get; set; }
        public CustomerModel Customer { get; set; }
        public ToyModel Toy { get; set; }
        public int Quantity { get; set; }
        public double Total { get; set; }
        public bool Active { get; set; }

        public PurchaseModel()
        {
        }

        public PurchaseModel(int id, EmployeeModel employee, CustomerModel customer, ToyModel toy, int quantity, double total, bool active)
        {
            Id = id;
            Employee = employee;
            Customer = customer;
            Toy = toy;
            Quantity = quantity;
            Total = total;
            Active = active;
        }
    }
}
