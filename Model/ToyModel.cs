namespace ToyStore.Model
{
    class ToyModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AmountOfStock { get; set; }
        public bool Active { get; set; }

        public ToyModel()
        {
        }

        public ToyModel(int id, string name, int amountOfStock, bool active)
        {
            Id = id;
            Name = name;
            AmountOfStock = amountOfStock;
            Active = active;
        }
    }
}
