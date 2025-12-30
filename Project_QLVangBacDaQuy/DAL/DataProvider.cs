using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient; 
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;     
using System.Text.RegularExpressions; 
namespace Project_QLVangBacDaQuy.DAL
{
    internal class DataProvider
    {
        // Tạo 1 instance dùng chung 
        private static DataProvider _instance;
        public static DataProvider Instance
        {
            get
            {
                if (_instance == null) _instance = new DataProvider();
                return _instance;
            }
            private set { _instance = value; }
        }

        private DataProvider() { }

        private string connectionSTR = @"Data Source=.;Initial Catalog=QL_VangBacDaQuy;Integrated Security=True";

        public DataTable ExecuteQuery(string query, object[] parameter = null)
        {
            DataTable data = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionSTR))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    AddParameters(command, query, parameter); 
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(data);
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi truy vấn: " + ex.Message);
            }
            return data;
        }

        public int ExecuteNonQuery(string query, object[] parameter = null)
        {
            int data = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionSTR))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    AddParameters(command, query, parameter);
                    data = command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thực thi: " + ex.Message);
            }
            return data;
        }

        public object ExecuteScalar(string query, object[] parameter = null)
        {
            object data = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionSTR))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    AddParameters(command, query, parameter);
                    data = command.ExecuteScalar();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lấy giá trị: " + ex.Message);
            }
            return data;
        }

        private void AddParameters(SqlCommand command, string query, object[] parameter)
        {
            if (parameter != null && parameter.Length > 0)
            {
                MatchCollection matches = Regex.Matches(query, @"@\w+");

                for (int i = 0; i < matches.Count && i < parameter.Length; i++)
                {
                    if (!command.Parameters.Contains(matches[i].Value))
                    {
                        command.Parameters.AddWithValue(matches[i].Value, parameter[i]);
                    }
                }
            }
        }
    }
}
