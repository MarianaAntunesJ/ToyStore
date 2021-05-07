using System;

namespace ToyStore.Model
{
    class CustomerModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CPF { get; set; }
        public DateTime Birthday { get; set; }
        public string Phone { get; set; }
        public char Gender { get; set; }
        public bool Active { get; set; }

        public CustomerModel()
        {
        }

        public CustomerModel(int id, string name, string cpf, DateTime birthday, string cellphone, char gender, bool active)
        {
            Id = id;
            Name = name;
            CPF = cpf;
            Birthday = birthday;
            Phone = cellphone;
            Gender = gender;
            Active = active;
        }
    }
}
