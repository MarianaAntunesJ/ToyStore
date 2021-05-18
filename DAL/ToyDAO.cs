using System.Collections.Generic;
using System.Data.SqlClient;
using ToyStore.Model;

namespace ToyStore.DAL
{
    class ToyDAO
    {
        private readonly string _table = "Toy";
        private Connection Connection { get; set; }
        private SqlCommand Cmd { get; set; }

        public ToyDAO()
        {
            Connection = new Connection();
            Cmd = new SqlCommand();
        }

        private void GetConexao()
        {
            Cmd.Connection = Connection.ReturnConnection();
        }

        private bool ToyData(ToyModel toy)
        {
            Cmd.Parameters.AddWithValue("@Name", toy.Name);
            Cmd.Parameters.AddWithValue("@AmountOfStock", toy.AmountOfStock);
            Cmd.Parameters.AddWithValue("@Active", toy.Active);

            if (Cmd.ExecuteNonQuery() == 1)
                return true;
            else
                return false;
        }

        public bool Insert(ToyModel toy)
        {
            GetConexao();
            Cmd.CommandText = $@"{QueryHelper.GetInsertInto(_table)} (@Name, @AmountOfStock, @Active)";
            Cmd.Parameters.Clear();
            return ToyData(toy);
        }

        private List<ToyModel> GetToy()
        {
            SqlDataReader rd = Cmd.ExecuteReader();
            List<ToyModel> toys = new List<ToyModel>();

            while (rd.Read())
            {
                ToyModel toy = new ToyModel(
                        (int)rd[nameof(ToyModel.Id)],
                        (string)rd[nameof(ToyModel.Name)],
                        (int)rd[nameof(ToyModel.AmountOfStock)],
                        (bool)rd[nameof(ToyModel.Active)]);

                toys.Add(toy);
            }
            rd.Close();
            return toys;
        }

        public List<ToyModel> ToList()
        {
            GetConexao();
            Cmd.CommandText = $"{QueryHelper.GetSelectFrom(_table)}";

            return GetToy();
        }

        public List<ToyModel> Query(string search)
        {
            GetConexao();
            search = $"%{search}%";
            Cmd.CommandText = $"{QueryHelper.GetSelectFrom(_table)} WHERE Id LIKE @search OR Name LIKE @search";

            Cmd.Parameters.Clear();
            Cmd.Parameters.AddWithValue("@search", search);

            return GetToy();
        }

        public bool Update(ToyModel toy)
        {
            GetConexao();
            Cmd.CommandText = $@"{QueryHelper.GetUpdateSet(_table)} Name = @Name, AmountOfStock = @AmountOfStock, Active = @Active  WHERE Id = @Id";

            Cmd.Parameters.Clear();
            Cmd.Parameters.AddWithValue("@Id", toy.Id);
            return ToyData(toy);
        }
    }
}

