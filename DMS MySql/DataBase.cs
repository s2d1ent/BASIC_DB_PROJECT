using System;
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
    class DataBase
    {
        public string Host { get; set; }
        public string Username { get; set; }
        public string Port { get; set; }
        public string Password { get; set; }
        public string Database { get; set; }
        public string Table { get; set; }

        private string Connector = @"";
        private string Query;
        private MySqlConnection Connection;
        private MySqlDataAdapter Client;

        public DataBase()
        {
            Connection = new MySqlConnection(Connector);
            Client = new MySqlDataAdapter(Query, Connection);
        }
        public DataBase(string host, string port, string username, string password)
        {
            new DataBase();
            this.Host = host;
            this.Port = port;
            this.Username = username;
            this.Password = password;
            this.Connector = $@"server={Host};userid={Username};password={Password}";
        }
        public DataBase(string host, string port, string username, string password, string database)
        {
            new DataBase();
            this.Host = host;
            this.Port = port;
            this.Username = username;
            this.Password = password;
            this.Database = database;
            this.Connector = $@"server={Host};userid={Username};password={Password};database={Database}";
        }
        ~DataBase()
        {
            GC.Collect(2,GCCollectionMode.Optimized);
        }
        public void Connect()
        {
            if(Connection.State == System.Data.ConnectionState.Closed)
            {
                Connection.OpenAsync();
            }
        }
        public void Disconnect()
        {
            if (Connection.State == System.Data.ConnectionState.Open)
            {
                Connection.CloseAsync();
            }
        }
        public System.Data.ConnectionState GetStatus()
        {
            return System.Data.ConnectionState.Open;
        }
        public void GetConnectionData()
        {
            var data = $"Host: {Host}" + "\n" + $"Port: {Port}" + "\n" + $"Username: {Username}" + "\n" + $"Database: {Database}" + "\n";
            MessageBox.Show(data, "Data connection", MessageBoxButton.OK);
        }
        public void UseConfig(string host, string port, string username, string password)
        {
            this.Host = host;
            this.Port = port;
            this.Username = username;
            this.Password = password;
            this.Connector = $@"server={Host};userid={Username};password={Password}";
        }
        // TODO доделать использование конфига
        // разобраться с подключением
        public void UseConfig(string path)
        {
            string Path = path;
            Host = (string)XDocument.Parse(File.ReadAllText(Path)).Element("config").Element("host");
            Port = (string)XDocument.Parse(File.ReadAllText(Path)).Element("config").Element("port");
            Username = (string)XDocument.Parse(File.ReadAllText(Path)).Element("config").Element("username");
            Password = (string)XDocument.Parse(File.ReadAllText(Path)).Element("config").Element("psswrd");
            Database = (string)XDocument.Parse(File.ReadAllText(Path)).Element("config").Element("database");

            Connector = $@"server={Host};user={Username};port={Port};password={Password};database={Database};charset=utf8";
            Connection = new MySqlConnection(Connector);
            bool status = TryConnect();
            if (status)
            {
                MessageBox.Show("Успешно: подключение установленно", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show("Ошибка: Отсутствует соединение с сервером", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        public bool TryConnect()
        {
            try
            {
                Connection.OpenAsync();
                if (Connection.State == System.Data.ConnectionState.Open)
                {
                    //Query = "show tables";
                    //Client = new MySqlDataAdapter(Query, Connection);
                    MessageBox.Show($"Успешно: Соединение было намеренно разорвано {Connection.State}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Information);
                    Connection.CloseAsync();
                    return true;    
                }
                else if (Connection.State == System.Data.ConnectionState.Broken){
                    Connection.CloseAsync();
                    MessageBox.Show($"Ошибка: Соединение было намеренно разорвано {Connection.State}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                else
                {
                    Connection.CloseAsync();
                    MessageBox.Show($"Ошибка: Соединение закрыто {Connection.State}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
