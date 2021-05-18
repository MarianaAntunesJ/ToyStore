using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using ToyStore.Model;

namespace ToyStore.DAL
{
    class EmployeeDAO
    {
        private readonly string _table = "Employee";
        private Connection Connection { get; set; }
        private SqlCommand Cmd { get; set; }

        public EmployeeDAO()
        {
            Connection = new Connection();
            Cmd = new SqlCommand();
        }

        private void GetConexao()
        {
            Cmd.Connection = Connection.ReturnConnection();
        }

        private bool EmployeeData(EmployeeModel employee)
        {
            Cmd.Parameters.AddWithValue("@Name", employee.Name);
            Cmd.Parameters.AddWithValue("@Phone", employee.Phone);
            Cmd.Parameters.AddWithValue("@CPF", employee.CPF);
            Cmd.Parameters.AddWithValue("@Gender", employee.Gender);
            Cmd.Parameters.AddWithValue("@Email", employee.Email);
            Cmd.Parameters.AddWithValue("@Username", employee.Username);
            Cmd.Parameters.AddWithValue("@Password", employee.Password);
            Cmd.Parameters.AddWithValue("@Active", employee.Active);

            if (Cmd.ExecuteNonQuery() == 1)
                return true;
            else
                return false;
        }

        public bool Insert(EmployeeModel employee)
        {
            GetConexao();
            Cmd.CommandText = $@"{QueryHelper.GetInsertInto(_table)} (@Name, @Phone, @CPF, @Gender, @Email, @Username, @Password, @Active)";
            Cmd.Parameters.Clear();
            return EmployeeData(employee);
        }

        private List<EmployeeModel> GetEmployee()
        {
            SqlDataReader rd = Cmd.ExecuteReader();
            List<EmployeeModel> employees = new List<EmployeeModel>();

            while (rd.Read())
            {
                EmployeeModel employee = new EmployeeModel(
                        (int)rd[nameof(EmployeeModel.Id)],
                        (string)rd[nameof(EmployeeModel.Name)],
                        (string)rd[nameof(EmployeeModel.Phone)],
                        (string)rd[nameof(EmployeeModel.CPF)],
                        (string)rd[nameof(EmployeeModel.Gender)],
                        (string)rd[nameof(EmployeeModel.Email)],
                        (string)rd[nameof(EmployeeModel.Username)],
                        (string)rd[nameof(EmployeeModel.Password)],
                        (bool)rd[nameof(EmployeeModel.Active)]);

                employees.Add(employee);
            }
            rd.Close();
            return employees;
        }

        public List<EmployeeModel> ToList()
        {
            GetConexao();
            Cmd.CommandText = $"{QueryHelper.GetSelectFrom(_table)}";

            return GetEmployee();
        }

        public List<EmployeeModel> Query(string search)
        {
            GetConexao();
            search = $"%{search}%";
            Cmd.CommandText = $"{QueryHelper.GetSelectFrom(_table)} WHERE Id LIKE @search OR CPF LIKE @search OR Name LIKE @search";

            Cmd.Parameters.Clear();
            Cmd.Parameters.AddWithValue("@search", search);

            return GetEmployee();
        }

        public bool Update(EmployeeModel employee)
        {
            GetConexao();
            Cmd.CommandText = $@"{QueryHelper.GetUpdateSet(_table)} Name = @Name, Phone = @Phone, CPF = @CPF, Gender = @Gender Email = @Email, Username = @Username, Password = @Password, Active = @Active  WHERE Id = @Id";

            Cmd.Parameters.Clear();
            Cmd.Parameters.AddWithValue("@Id", employee.Id);
            return EmployeeData(employee);
        }
    }
}
