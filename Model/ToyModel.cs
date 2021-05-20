namespace ToyStore.Model
{
    class ToyModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AmountOfStock { get; set; }
        public byte[] Image { get; set; } = new byte[1];
        public bool Active { get; set; }

        public ToyModel()
        {
        }

        public ToyModel(int id, string name, int amountOfStock, byte[] image, bool active)
        {
            Id = id;
            Name = name;
            AmountOfStock = amountOfStock;
            Image = image;
            Active = active;
        }
    }
}
