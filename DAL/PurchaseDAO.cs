using System.Collections.Generic;
using System.Data.SqlClient;
using ToyStore.Model;

namespace ToyStore.DAL
{
    class PurchaseDAO
    {
        private readonly string _table = "Purchase";
        private Connection Connection { get; set; }
        private SqlCommand Cmd { get; set; }

        public PurchaseDAO()
        {
            Connection = new Connection();
            Cmd = new SqlCommand();
        }

        private void GetConexao()
        {
            Cmd.Connection = Connection.ReturnConnection();
        }

        private bool PurchaseData(PurchaseModel purchase)
        {
            Cmd.Parameters.AddWithValue("@Customer", purchase.Customer);
            Cmd.Parameters.AddWithValue("@Toy", purchase.Toy);
            Cmd.Parameters.AddWithValue("@Quantity", purchase.Quantity);
            Cmd.Parameters.AddWithValue("@Total", purchase.Total);
            Cmd.Parameters.AddWithValue("@Active", purchase.Active);

            if (Cmd.ExecuteNonQuery() == 1)
                return true;
            else
                return false;
        }

        public bool Insert(PurchaseModel purchase)
        {
            GetConexao();
            Cmd.CommandText = $@"{QueryHelper.GetInsertInto(_table)} (@Customer, @Toy, @Quantity, @Total, @Active)";
            Cmd.Parameters.Clear();
            return PurchaseData(purchase);
        }

        private List<PurchaseModel> GetPurchase()
        {
            SqlDataReader rd = Cmd.ExecuteReader();
            List<PurchaseModel> purchases = new List<PurchaseModel>();

            while (rd.Read())
            {
                PurchaseModel purchase = new PurchaseModel(
                        (int)rd[nameof(PurchaseModel.Id)],
                        (CustomerModel)rd[nameof(PurchaseModel.Customer)],
                        (ToyModel)rd[nameof(PurchaseModel.Toy)],
                        (int)rd[nameof(PurchaseModel.Quantity)],
                        (double)rd[nameof(PurchaseModel.Total)],
                        (bool)rd[nameof(PurchaseModel.Active)]);

                purchases.Add(purchase);
            }
            rd.Close();
            return purchases;
        }

        public List<PurchaseModel> ToList()
        {
            GetConexao();
            Cmd.CommandText = $"{QueryHelper.GetSelectFrom(_table)}";

            return GetPurchase();
        }

        //ToDo: Search by client name
        public List<PurchaseModel> Query(string search)
        {
            GetConexao();
            search = $"%{search}%";
            Cmd.CommandText = $"{QueryHelper.GetSelectFrom(_table)} WHERE Id LIKE @search";

            Cmd.Parameters.Clear();
            Cmd.Parameters.AddWithValue("@search", search);

            return GetPurchase();
        }

        public bool Update(PurchaseModel purchase)
        {
            GetConexao();
            Cmd.CommandText = $@"{QueryHelper.GetUpdateSet(_table)} Customer = @Customer, Toy = @Toy, Quantity = @Quantity, Total = @Total, Active = @Active  WHERE Id = @Id";

            Cmd.Parameters.Clear();
            Cmd.Parameters.AddWithValue("@Id", purchase.Id);
            return PurchaseData(purchase);
        }
    }
}
