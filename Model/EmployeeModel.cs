namespace ToyStore.Model
{
    public class EmployeeModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string CPF { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool Active { get; set; }

        public EmployeeModel()
        {
        }

        public EmployeeModel(int id, string name, string phone, string cPF, string gender, string email, string username, string password, bool active)
        {
            Id = id;
            Name = name;
            Phone = phone;
            CPF = cPF;
            Gender = gender;
            Email = email;
            Username = username;
            Password = password;
            Active = active;
        }
    }
}