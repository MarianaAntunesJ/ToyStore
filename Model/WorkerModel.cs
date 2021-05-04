namespace ToyStore.Model
{
    class WorkerModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Social Security Number is similar to CPF (Cadastro de pessoa física)
        public string SSN { get; set; }

        public string Email { get; set; }
        public string Userrname { get; set; }
        public string Password { get; set; }
        public bool Active { get; set; }

        public WorkerModel()
        {
        }

        public WorkerModel(int id, string name, string ssn, string email, string userrname, string password, bool active)
        {
            Id = id;
            Name = name;
            SSN = ssn;
            Email = email;
            Userrname = userrname;
            Password = password;
            Active = active;
        }
    }
}