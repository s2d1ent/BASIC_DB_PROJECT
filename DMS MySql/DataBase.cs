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
        public bool Ssl { get; set; }
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
        private string Charset = "utf8";

        private string Connector = $"";
        private string Query;
        private MySqlConnection Connection;
        private MySqlDataAdapter Client;

        public DataBase() {}
        public DataBase(string host, string port, string username, string password)
        {
            this.Host = host;
            this.Port = port;
            this.Username = username;
            this.Password = password;
            Connector = $"server={Host};user={Username};password={Password};port={Port};charset={Charset};SslMode={ssl}";
        }
        public DataBase(string host, string port, string username, string password, string database)
        {
            this.Host = host;
            this.Port = port;
            this.Username = username;
            this.Password = password;
            this.Database = database;
            Connector = $"server={Host};user={Username};password={Password};port={Port};database={Database};charset={Charset};SslMode={ssl}";
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
        public DataSet GetTables()
        {
            DataSet Result = new DataSet();
            Connection = new MySqlConnection(Connector);
            Query = "show databases";
            Client = new MySqlDataAdapter(Query, Connection);
            Client.Fill(Result);

            return Result;
        }
        public void UseConfig(string path, Window w)
        {
            UseConfig(path);
            w.Close();
        }
        public void UseConfig(string path)
        {
            string Path = path;
            var config = XDocument.Parse(File.ReadAllText(Path)).Element("config");
            Host = (string)config.Element("host");
            Port = (string)config.Element("port");
            Username = (string)config.Element("username");
            Password = (string)config.Element("psswrd");
            Database = (string)config.Element("database");

            Connector = $@"server={Host};user={Username};password={Password};port={Port};database={Database};charset={Charset};SslMode={ssl}";
            bool status = TryConnect();
            if (status)
            {
                //MessageBox.Show("Successful: connection is established", "Successful", MessageBoxButton.OK, MessageBoxImage.Information);
                Workspace wk = new Workspace();
                /*if(Database.Length == 0)
                    wk.db = new DataBase(Host, Port, Username, Password);
                else
                    wk.db = new DataBase(Host, Port, Username, Password, Database);*/
                MessageBox.Show($"Host - {this.Host} : DB - {this.Database} : Password - {this.Password} : User - {this.Username}", "Db", MessageBoxButton.OK);
                wk.db = new DataBase(Host, Port, Username, Password);
                wk.Show();
            }
            else
                MessageBox.Show("Error: There is no connection to the server", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        public bool TryConnect()
        {
            try
            {
                Connection = new MySqlConnection(Connector);
                Query = "show databases";
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
                    //MessageBox.Show($"Ошибка: Соединение было намеренно разорвано {Connection.State}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                else
                {
                    Connection.CloseAsync();
                    //MessageBox.Show($"Ошибка: Соединение закрыто {Connection.State}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
    }
}
