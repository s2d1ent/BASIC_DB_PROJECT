using System;
using System.Data;
using System.IO;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using System.Threading.Tasks;
using System.Windows;
using MySql;
using MySql.Data;
using MySql.Data.Types;
using MySql.Data.Common;
using MySql.Data.MySqlClient;
using MySql.Data.EntityFramework;
using System.Windows.Controls;
using System.Windows.Input;

namespace DMS_MySql
{
    public class DataBase
    {
        public string Host { get; set; }
        public string Username { get; set; }
        public string Port { get; set; }
        public string Password { get; set; }
        public string Database { get; set; }
        public string Table { get; set; }
        public bool Ssl = false;
        private string ssl
        {
            get
            {
                if (!Ssl)
                    return "None";
                else
                    return "Required";
            }
            set
            {
                value = value;
            }
        }
        private string DateTimeConvert = "convert zero datetime=True";
        private string Charset = "utf8";

        private string Connector = $"";
        private string Query;
        private MySqlConnection Connection;
        private MySqlDataAdapter Client;
        private MySqlCommand cmd;

        private History history = new History();

        public DataBase() {}
        public DataBase(string host, string port, string username, string password)
        {
            this.Host = host;
            this.Port = port;
            this.Username = username;
            this.Password = password;
            Connector = $"server={Host};user={Username};password={Password};port={Port};charset={Charset};SslMode={ssl};{DateTimeConvert}";
        }
        public DataBase(string host, string port, string username, string password, string database)
        {
            this.Host = host;
            this.Port = port;
            this.Username = username;
            this.Password = password;
            this.Database = database;
            Connector = $"server={Host};user={Username};password={Password};port={Port};database={Database};charset={Charset};SslMode={ssl};{DateTimeConvert}";
        }
        ~DataBase()
        {
            GC.Collect(2, GCCollectionMode.Optimized);
        }
        public void Connect()
        {
            if(Connection.State == System.Data.ConnectionState.Closed)
            {
                Connection.Open();
            }
        }
        public void Disconnect()
        {
            if (Connection.State == System.Data.ConnectionState.Open)
            {
                Connection.Close();
            }
        }
        public System.Data.ConnectionState GetStatus()
        {
            return System.Data.ConnectionState.Open;
        }
        public void GetConfig()
        {
            
        }
        public DataTable GetTable(string table_name)
        {
            DataTable Result = new DataTable();
            Connection = new MySqlConnection(Connector);
            Query = $"SELECT * FROM {Database}.{table_name}";
            Client = new MySqlDataAdapter(Query, Connection);
            Client.Fill(Result);
            return Result;
        }
        async public Task<DataTable> GetTableAsync(string table_name)
        {
            DataTable result = new DataTable();
            await Task.Run(() => { result = GetTable(table_name); });
            return result;
        }
        public List<string> GetTables()
        {
            List<string> Result = new List<string>();
            Connection = new MySqlConnection(Connector);
            Query = "show tables";
            Connection.Open();
            cmd = new MySqlCommand(Query, Connection);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
                Result.Add(reader[0].ToString());
            Connection.Close();

            return Result;
        }
        async public Task<List<string>> GetTablesAsync()
        {
            List<string> result = new List<string>();
            await Task.Run(() => { result = GetTables(); });
            return result;
        }
        public List<string> GetTables(string database)
        {
            List<string> Result = new List<string>();
            try
            {
                Connector = $"server={Host};user={Username};password={Password};port={Port};database={database};charset={Charset};SslMode={ssl};{DateTimeConvert}";
                Connection = new MySqlConnection(Connector);
                Query = "show tables";
                Connection.Open();
                cmd = new MySqlCommand(Query, Connection);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                    Result.Add(reader[0].ToString());
                Connection.Close();
            }
            catch (Exception ex)
            {
                Task.Run(() => {
                    MessageBox.Show($"Message:{ex.Message} \nData: {ex.Data} \nStackTrace{ex.StackTrace} \nHelpLink{ex.HelpLink} \nPlese, copy this message and send on developers email", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                });
            }
            return Result;
        }
        async public Task<List<string>> GetTablesAsync(string database)
        {
            List<string> result = new List<string>();
            await Task.Run(() => { result = GetTables(database); });
            return result;
        }
        public List<string> GetDatabases()
        {
            List<string> Result = new List<string>();
            Connection = new MySqlConnection(Connector);
            Query = "show databases";
            Connection.Open();
            cmd = new MySqlCommand(Query, Connection);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
                Result.Add(reader[0].ToString());
            Connection.Close();

            return Result;
        }
        async public Task<List<string>> GetDatabasesAsync()
        {
            List<string> result = new List<string>();
            await Task.Run(()=> { result = GetDatabases(); });
            return result;
        }
        public bool UseConfig(string path)
        {
            bool result = false;
            try
            {
                string Path = path;
                var config = XDocument.Parse(File.ReadAllText(Path)).Element("config");
                Host = (string)config.Element("host");
                Port = (string)config.Element("port");
                Username = (string)config.Element("username");
                Password = (string)config.Element("psswrd");
                Database = (string)config.Element("database");
                Connector = $@"server={Host};user={Username};password={Password};port={Port};database={Database};charset={Charset};SslMode={ssl};{DateTimeConvert}";
                bool try_connect = TryConnect();
                if (try_connect)
                {
                    //MessageBox.Show("Successful: connection is established", "Successful", MessageBoxButton.OK, MessageBoxImage.Information);
                    Task.Run(() => {
                        history.AddConnection(this);
                    });
                    Workspace wk = new Workspace();
                    wk.db = new DataBase(Host, Port, Username, Password, Database);
                    wk.Show();
                    result = true;
                    return true;
                }
                else
                {
                    //MessageBox.Show("Error: There is no connection to the server", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            catch (Exception ex) 
            {
                Task.Run(() => {
                    MessageBox.Show($"Message:{ex.Message} \nData: {ex.Data} \nStackTrace{ex.StackTrace} \nHelpLink{ex.HelpLink} \nPlese, copy this message and send on developers email", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                });
            }
            return result;
        }
        public async ValueTask<bool> UseConfigAsync(string path)
        {
            return await UseConfigAsync(path);
        }
        public bool TryConnect()
        {
            try
            {
                Connection = new MySqlConnection(Connector);
                Query = "SHOW VARIABLES";
                Client = new MySqlDataAdapter(Query, Connection);
                Connect();
                if (Connection.State == System.Data.ConnectionState.Open)
                {
                    //MessageBox.Show($"Успешно: Соединение было намеренно разорвано {Connection.State}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Information);
                    Connection.CloseAsync();
                    return true;    
                }
                else if (Connection.State == System.Data.ConnectionState.Broken){
                    Connection.CloseAsync();
                    MessageBox.Show($"Error: The connection was deliberately broken", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                else
                {
                    Connection.CloseAsync();
                    MessageBox.Show($"Error: Connection closed", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                    
            }
            catch (Exception ex)
            {
                Task.Run(()=> {
                    MessageBox.Show($"Message:{ex.Message} \nData: {ex.Data} \nStackTrace{ex.StackTrace} \nHelpLink{ex.HelpLink} \nPlese, copy this message and send on developers email", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                });
                return false;
            }
        }
        public async ValueTask<bool> TryConnectAsync()
        {
            bool result = false;
            await Task.Run(() => { result = TryConnect(); });
            return result;
        }
    }
}
