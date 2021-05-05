using System;

namespace ToyStore.Model
{
    class CustomerModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Social Security Number is similar to CPF (Cadastro de pessoa física)
        public string SSN { get; set; }

        public DateTime Birthday { get; set; }
        public string Cellphone { get; set; }
        public char Gender { get; set; }
        public bool Active { get; set; }

        public CustomerModel()
        {
        }

        public CustomerModel(int id, string name, string snn, DateTime birthday, string cellphone, char gender, bool active)
        {
            Id = id;
            Name = name;
            SSN = snn;
            Birthday = birthday;
            Cellphone = cellphone;
            Gender = gender;
            Active = active;
        }
    }
}
