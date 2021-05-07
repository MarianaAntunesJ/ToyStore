using System;

namespace ToyStore.Model
{
    class CustomerModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string CPF { get; set; }
        public string Gender { get; set; }
        public DateTime Birthday { get; set; }
        public bool Active { get; set; }

        public CustomerModel()
        {
        }

        public CustomerModel(int id, string name, string phone, string cPF, string gender, DateTime birthday, bool active)
        {
            Id = id;
            Name = name;
            Phone = phone;
            CPF = cPF;
            Gender = gender;
            Birthday = birthday;
            Active = active;
        }
    }
}
