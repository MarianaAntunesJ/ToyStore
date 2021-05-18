using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using ToyStore.Model;

namespace ToyStore.DAL
{
    class CustomerDAO
    {
        private readonly string _table = "Customer";
        private Connection Connection { get; set; }
        private SqlCommand Cmd { get; set; }

        public CustomerDAO()
        {
            Connection = new Connection();
            Cmd = new SqlCommand();
        }

        private void GetConexao()
        {
            Cmd.Connection = Connection.ReturnConnection();
        }

        private bool CustomerData(CustomerModel customer)
        {
            Cmd.Parameters.AddWithValue("@Name", customer.Name);
            Cmd.Parameters.AddWithValue("@Phone", customer.Phone);
            Cmd.Parameters.AddWithValue("@CPF", customer.CPF);
            Cmd.Parameters.AddWithValue("@Gender", customer.Gender);
            Cmd.Parameters.AddWithValue("@Birthday", Convert.ToDateTime(customer.Birthday).ToShortDateString());
            Cmd.Parameters.AddWithValue("@Active", customer.Active);

            if (Cmd.ExecuteNonQuery() == 1)
                return true;
            else
                return false;
        }

        public bool Insert(CustomerModel customer)
        {
            GetConexao();
            Cmd.CommandText = $@"{QueryHelper.GetInsertInto(_table)} (@Name, @Phone, @CPF, @Gender, @Birthday, @Active)";
            Cmd.Parameters.Clear();
            return CustomerData(customer);
        }

        private List<CustomerModel> GetCustomer()
        {
            SqlDataReader rd = Cmd.ExecuteReader();
            List<CustomerModel> customers = new List<CustomerModel>();

            while (rd.Read())
            {
                CustomerModel customer = new CustomerModel(
                        (int)rd[nameof(CustomerModel.Id)],
                        (string)rd[nameof(CustomerModel.Name)],
                        (string)rd[nameof(CustomerModel.Phone)],
                        (string)rd[nameof(CustomerModel.CPF)],
                        (string)rd[nameof(CustomerModel.Gender)],
                        (DateTime)rd[nameof(CustomerModel.Birthday)],
                        (bool)rd[nameof(CustomerModel.Active)]);

                customers.Add(customer);
            }
            rd.Close();
            return customers;
        }

        public List<CustomerModel> ToList()
        {
            GetConexao();
            Cmd.CommandText = $"{QueryHelper.GetSelectFrom(_table)}";

            return GetCustomer();
        }

        public List<CustomerModel> Query(string search)
        {
            GetConexao();
            search = $"%{search}%";
            Cmd.CommandText = $"{QueryHelper.GetSelectFrom(_table)} WHERE Id LIKE @search OR CPF LIKE @search OR Name LIKE @search";

            Cmd.Parameters.Clear();
            Cmd.Parameters.AddWithValue("@search", search);

            return GetCustomer();
        }

        public bool Update(CustomerModel customer)
        {
            GetConexao();
            Cmd.CommandText = $@"{QueryHelper.GetUpdateSet(_table)} Name = @Name, Phone = @Phone, CPF = @CPF, Gender = @Gender, Birthday = @Birthday, Active = @Active  WHERE Id = @Id";

            Cmd.Parameters.Clear();
            Cmd.Parameters.AddWithValue("@Id", customer.Id);
            return CustomerData(customer);
        }
    }
}

