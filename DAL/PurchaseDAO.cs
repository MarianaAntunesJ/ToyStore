using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Text;
using ToyStore.Model;

namespace ToyStore.DAL
{
    class PurchaseDAO
    {
        private readonly string _table = "Purchase";
        private Connection Connection { get; set; }
        private SqlCommand Cmd { get; set; }
        private ToyDAO _toyDAO = new ToyDAO();

        public PurchaseDAO()
        {
            Connection = new Connection();
            Cmd = new SqlCommand();
        }

        private void GetConexao()
        {
            Cmd.Connection = Connection.ReturnConnection();
        }

        private bool PurchaseData1(PurchaseModel purchase)
        {
            Cmd.Parameters.AddWithValue("@Employee", purchase.Employee);
            Cmd.Parameters.AddWithValue("@Customer", purchase.Customer);
            Cmd.Parameters.AddWithValue("@Quantity", purchase.Quantity);
            Cmd.Parameters.AddWithValue("@Total", purchase.Total);
            Cmd.Parameters.AddWithValue("@Active", purchase.Active);

            int modified = (int)Cmd.ExecuteScalar();
            if (modified != 0)
                return InsertToys(modified, purchase);
            return false;
        }

        private bool PurchaseData(PurchaseModel purchase)
        {
            Cmd.Parameters.AddWithValue("@Employee", purchase.Employee);
            Cmd.Parameters.AddWithValue("@Customer", purchase.Customer);
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
            Cmd.CommandText = $@"{QueryHelper.GetInsertInto(_table)} (@Employee, @Customer, @Quantity, @Total, @Active)";
            Cmd.Parameters.Clear();
            return PurchaseData1(purchase);
        }

        private bool InsertToys(int idToy, PurchaseModel purchase)
        {
            var sb = new StringBuilder();

            foreach (var toy in purchase.Toys)
                sb.Append($"({toy.Id}, {idToy}),\t");

            string values = sb.ToString();
            values = values.Remove(values.Length - 2);
            return InsertToysQuery(values);
        }

        private bool InsertToysQuery(string values)
        {
            GetConexao();
            Cmd.CommandText = $"INSERT INTO ToysOfPurchase VALUES {values}";

            if (Cmd.ExecuteNonQuery() == 1)
                return true;
            return false;
        }


        private List<PurchaseModel> GetPurchase()
        {
            SqlDataReader rd = Cmd.ExecuteReader();
            List<PurchaseModel> purchases = new List<PurchaseModel>();

            while (rd.Read())
            {
                PurchaseModel purchase = new PurchaseModel(
                        (int)rd[nameof(PurchaseModel.Id)],
                        (EmployeeModel)rd[nameof(PurchaseModel.Employee)],
                        (CustomerModel)rd[nameof(PurchaseModel.Customer)],
                        (int)rd[nameof(PurchaseModel.Quantity)],
                        (double)rd[nameof(PurchaseModel.Total)],
                        (bool)rd[nameof(PurchaseModel.Active)]);

                purchases.Add(purchase);
            }
            rd.Close();
            var ver = new ObservableCollection<ToyModel>();
            foreach (PurchaseModel item in purchases)
            {
                ver = new ObservableCollection<ToyModel>(GetProdutosDeProcedimento(item.Id));
                item.Toys = ver;
            }
            return purchases;
        }

        public List<PurchaseModel> ToList()
        {
            GetConexao();
            Cmd.CommandText = $"{QueryHelper.GetSelectFrom(_table)}";

            return GetPurchase();
        }

        private List<ToyModel> GetProdutosDeProcedimento(int idPurchase)
        {
            GetConexao();
            Cmd.CommandText = $"{QueryHelper.GetSelectFrom("ToysOfPurchase")} WHERE PurchaseId = @idPurchase ";

            Cmd.Parameters.Clear();
            Cmd.Parameters.AddWithValue("@idPurchase", idPurchase);

            SqlDataReader rd = Cmd.ExecuteReader();
            List<int> toys = new List<int>();

            while (rd.Read())
            {
                toys.Add((int)rd["ToyId"]);
            }
            rd.Close();
            return ListaProdutosEscolhidos(toys);
        }

        private List<ToyModel> ListaProdutosEscolhidos(List<int> idToys)
        {
            List<ToyModel> toys = new List<ToyModel>();
            foreach (int item in idToys)
                toys.Add(_toyDAO.GetToy(item));
            return toys;
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
            Cmd.CommandText = $@"{QueryHelper.GetUpdateSet(_table)} Employee = @Employee, Customer = @Customer, Toy = @Toy, Quantity = @Quantity, Total = @Total, Active = @Active  WHERE Id = @Id";

            Cmd.Parameters.Clear();
            Cmd.Parameters.AddWithValue("@Id", purchase.Id);
            return PurchaseData(purchase);
        }
    }
}
