using System;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Text;

namespace Bank.DAL {
    /// <summary>
    /// Main DAL class used to communicate with database via SQL. 
    /// </summary>
    public class Dal {

        public static DataTable GetCustomDataTable(string tableName, string sql) {
            DataSet dataSet = new DataSet();
            FillDataSetCustom(dataSet, tableName, sql);
            return dataSet.Tables[tableName];
        }

        public static DataTable GetDataTable(string tableName, string orderBy) {
            DataSet dataSet = new DataSet();
            FillDataSet(dataSet, tableName, orderBy);
            return dataSet.Tables[tableName];
        }

        public static DataTable GetDataTable(string tableName) => GetDataTable(tableName, "");

        
        public static bool ExecuteSql(string sql) {
            OleDbConnection connection = new OleDbConnection {
                ConnectionString = GetConnectionString()
            };

            OleDbCommand command = new OleDbCommand {
                Connection = connection,
                CommandText = sql
            };

            try {
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            } catch (Exception e) {
                try {

                    Console.WriteLine("Dal ExecuteSql: An Error Occured, Data dump - ");
                    Console.WriteLine("ConnectionString - " + connection.ConnectionString);
                    Console.WriteLine("Error: ");
                    Console.WriteLine(e.ToString());
                    Console.WriteLine("SQL String: ");
                    Console.WriteLine(sql);
                } catch (Exception ex) { }
                return false;
            }
            try {

                Console.WriteLine("Dal ExecuteSql: Success, Data dump - ");
                Console.WriteLine("ConnectionString - " + connection.ConnectionString);

                Console.WriteLine("SQL String: ");
                Console.WriteLine(sql);
            } catch (Exception ex) { }
            return true;
        }


        public static void FillDataSet(DataSet dataSet, string tableName, string orderBy) {
            if (orderBy != "")
                FillDataSet(dataSet, tableName,  "SELECT * FROM " + tableName + " ORDER BY " + orderBy);
            else
                FillDataSet(dataSet, tableName, "SELECT * FROM " + tableName);
        }

        public static void FillDataSetCustom(DataSet dataSet, string tableName, string sql) {
            
            OleDbConnection connection = new OleDbConnection();
            connection.ConnectionString = GetConnectionString();

            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;
            
            command.CommandText = sql;   

            OleDbDataAdapter adapter = new OleDbDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(dataSet, tableName);
        }

        
        private static string GetConnectionString() {
            string connectionString;

            connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;"; 
            connectionString += "Data Source=" + "'" + GetDbLocation() + "'" + ";";
            connectionString += "Persist Security Info=True";

            return connectionString;
        }

        private static string GetDbLocation() {
            // Get Access DB dir
            string path = Directory.GetCurrentDirectory();
            path = path.Replace(@"bin\Debug", "");
            path = path.Replace(@"bin\Release", "");

            path = path + "DB_Project.accdb";

            return path;
        }
    }
}