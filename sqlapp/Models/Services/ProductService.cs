using System.Data.SqlClient;

namespace sqlapp.Models.Services
{
    public class ProductService
    {
        private static string db_source = "az204-sqlserver-joshua.database.windows.net";
        private static string db_user = "joshuadb";
        private static string db_password = "vertelikniet123!";
        private static string db_database = "joshua-az204-db";

        private SqlConnection GetConnection()
        {
            var _builder = new SqlConnectionStringBuilder();
            _builder.DataSource = db_source;
            _builder.UserID = db_user;
            _builder.Password = db_password;
            _builder.InitialCatalog = db_database;
            return new SqlConnection(_builder.ConnectionString);
        }
        public List<Products> GetProducts()
        {
            SqlConnection conn = GetConnection();

            List<Products> _product_list = new List<Products>();

            string statement = "SELECT ProductID, ProductName, Quantity FROM Products";

            conn.Open();

            SqlCommand cmd = new SqlCommand(statement, conn);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Products product = new Products()
                    {
                        ProductID = reader.GetInt32(0),
                        ProductName = reader.GetString(1),
                        Quantity = reader.GetInt32(2)
                    };
                    _product_list.Add(product);
                }
            }
            conn.Close();
            return _product_list;
        }
    }
}
